using System.ComponentModel;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using CommunityToolkit.Mvvm.Messaging;
using Microsoft.Extensions.DependencyInjection;
using Veritas.Library.Configuration.Services;
using Veritas.Library.McoSettings.Manager;
using Veritas.Services.Launcher.Message;
using Veritas.Services.Launcher.Service.Client;
using Veritas.Services.Launcher.Service.IO;

namespace Veritas.Services.Launcher.ViewModel;

public partial class PatchViewModel : ObservableObject, IRecipient<LoginProcessStartedMessage>, IRecipient<LoginProcessFinishedMessage>
{
    [ObservableProperty] private int _downloadProgress;
    [ObservableProperty] private int _extractProgress;
    [ObservableProperty] private string _updateStatus = "Checking for updates...";
    [ObservableProperty] private bool _isPlayable = false;
    [ObservableProperty] private bool _isLoginProcess = false;
    
    private readonly BackgroundWorker _patchWorker = new();

    private IHttpClient _httpClient;
    private IVersionSystem _versionSystem;
    private IExtractService _extractService;

    public PatchViewModel(IHttpClient httpClient, IVersionSystem versionSystem, IExtractService extractService)
    {
        WeakReferenceMessenger.Default.RegisterAll(this);

        _httpClient = httpClient;
        _versionSystem = versionSystem;
        _extractService = extractService;

        _patchWorker.DoWork += PatchWorker_OnDoWork;
        _patchWorker.RunWorkerCompleted += PatchWorker_OnRunWorkerCompleted;
        _patchWorker.WorkerReportsProgress = true;
        _patchWorker.RunWorkerAsync();
    }

    public void Receive(LoginProcessStartedMessage message)
    {
        IsLoginProcess = message.Value;
    }

    private void PatchWorker_OnDoWork(object? sender, DoWorkEventArgs e)
    {
        PatchWorkerAsync().GetAwaiter().GetResult();
    }

    private async Task PatchWorkerAsync()
    {
        var patches = await _httpClient.LoadPatches();
        
        UpdateStatus = $"Found {patches.Count} patches. Downloading...";
        
        foreach (var patch in patches)
        {
            if (patch.Key.CompareTo(_versionSystem.GetVersion()) <= 0)
                continue;

            var progress = new Progress<double>(value =>
            {
                DownloadProgress = (int)value;
            });

            UpdateStatus = $"Downloading patch version {patch.Key.ToString()}...";

            await _httpClient.DownloadPatch(patch.Value, progress);

            var extractProgress = new Progress<double>(value =>
            {
                ExtractProgress = (int)value;
            });

            UpdateStatus = $"Extracting patch version {patch.Key.ToString()}...";

            await _extractService.ExtractFile(patch.Value, extractProgress);

            _versionSystem.SetVersion(patch.Key);

            DownloadProgress = 0;
            ExtractProgress = 0;
        }
    }
    
    private async void PatchWorker_OnRunWorkerCompleted(object? sender, RunWorkerCompletedEventArgs e)
    {
        UpdateStatus = $"Your client is updated";
        IsPlayable = true;

        WeakReferenceMessenger.Default.Send(new PatchProcessFinishedMessage(true));
    }

    [RelayCommand]
    private void ClickStart()
    {
        var settings = OptionManager.Load();
        DownloadProgress = new Random().Next(0, 100);
        ExtractProgress = new Random().Next(0, 100);
    }

    public void Receive(LoginProcessFinishedMessage message)
    {
        IsLoginProcess = false;
    }
}