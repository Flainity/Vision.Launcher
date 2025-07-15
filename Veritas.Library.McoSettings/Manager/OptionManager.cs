using Veritas.Library.McoSettings.Options;

namespace Veritas.Library.McoSettings.Manager;

public class OptionManager
{
    public static IEnumerable<IGameOption> Load()
    {
        var videoOptions = new VideoOptions();
        var soundOptions = new SoundOptions();
        var list = new List<IGameOption> { videoOptions, soundOptions };
        var test = OptionFiles.OptionFilePath;

        if (File.Exists(OptionFiles.OptionFilePath))
        {
            var position = 0;
            using var reader = new BinaryReader(File.Open(OptionFiles.OptionFilePath, FileMode.Open, FileAccess.Read));

            while (reader.BaseStream.Position != reader.BaseStream.Length)
            {
                reader.BaseStream.Seek(position, SeekOrigin.Begin);
                switch (position)
                {
                    case 39:
                        videoOptions.X = reader.ReadInt16();
                        break;
                    case 43:
                        videoOptions.Y = reader.ReadInt16();
                        break;
                    case 30:
                        videoOptions.CharacterShadow = reader.ReadInt16();
                        break;
                    case 34:
                        videoOptions.CharacterOutline = reader.ReadInt16();
                        break;
                    case 47:
                        videoOptions.WindowMode = reader.ReadBoolean();
                        break;
                    case 48:
                        videoOptions.Antialiasing = reader.ReadInt16();
                        break;
                    case 52:
                        videoOptions.GlowEffect = reader.ReadInt16();
                        break;
                    case 54:
                        videoOptions.ScreenVibrationEffect = reader.ReadBoolean();
                        break;
                    case 55:
                        videoOptions.CharacterEffect = reader.ReadBoolean();
                        break;
                    case 56:
                        videoOptions.ShowInterface = reader.ReadInt16();
                        break;
                }
                
                position++;
            }
        }
        else
        {
            videoOptions.X = 800;
            videoOptions.Y = 600;
            videoOptions.CharacterShadow = 1;
            videoOptions.CharacterOutline = 1;
            videoOptions.WindowMode = true;
            videoOptions.Antialiasing = 1;
            videoOptions.GlowEffect = 1;
            videoOptions.ScreenVibrationEffect = true;
            videoOptions.CharacterEffect = true;
            videoOptions.ShowInterface = 1;
        }

        if (File.Exists(OptionFiles.OptionSoundFilePath))
        {
            var position = 0;
            using var reader = new BinaryReader(File.Open(OptionFiles.OptionSoundFilePath, FileMode.Open, FileAccess.Read));

            while (reader.BaseStream.Position != reader.BaseStream.Length)
            {
                reader.BaseStream.Seek(position, SeekOrigin.Begin);
                switch (position)
                {
                    case 8:
                        soundOptions.MasterVolume = reader.ReadInt16();
                        break;
                    case 12:
                        soundOptions.BackgroundMusicVolume = reader.ReadInt16();
                        break;
                    case 16:
                        soundOptions.SoundEffectsVolume = reader.ReadInt16();
                        break;
                    case 20:
                        soundOptions.EnvironmentVolume = reader.ReadInt16();
                        break;
                }
                
                position++;
            }
        }
        else
        {
            soundOptions.MasterVolume = 100;
            soundOptions.BackgroundMusicVolume = 100;
            soundOptions.SoundEffectsVolume = 100;
            soundOptions.EnvironmentVolume = 100;
        }

        return list;
    }
}