namespace Veritas.Library.McoSettings;

using System;
using System.Collections.Generic;
using System.Runtime.InteropServices;

public class Resolutions
{
    private const int ENUM_CURRENT_SETTINGS = -1;

    [StructLayout(LayoutKind.Sequential)]
    private struct DEVMODE
    {
        private const int CCHDEVICENAME = 32;
        private const int CCHFORMNAME = 32;

        [MarshalAs(UnmanagedType.ByValTStr, SizeConst = CCHDEVICENAME)]
        public string dmDeviceName;
        public short dmSpecVersion;
        public short dmDriverVersion;
        public short dmSize;
        public short dmDriverExtra;
        public int dmFields;

        public int dmPositionX;
        public int dmPositionY;
        public int dmDisplayOrientation;
        public int dmColor;
        public int dmDuplex;
        public int dmYResolution;
        public int dmTTOption;
        public int dmCollate;
        [MarshalAs(UnmanagedType.ByValTStr, SizeConst = CCHFORMNAME)]
        public string dmFormName;
        public short dmLogPixels;
        public int dmBitsPerPel;
        public int dmPelsWidth;
        public int dmPelsHeight;
        public int dmDisplayFlags;
        public int dmDisplayFrequency;
        public int dmICMMethod;
        public int dmICMIntent;
        public int dmMediaType;
        public int dmDitherType;
        public int dmReserved1;
        public int dmReserved2;
        public int dmPanningWidth;
        public int dmPanningHeight;
    }

    [DllImport("user32.dll")]
    private static extern bool EnumDisplaySettings(string lpszDeviceName, int iModeNum, ref DEVMODE lpDevMode);

    public static List<(int width, int height, int refreshRate)> GetSupportedResolutions()
    {
        List<(int width, int height, int refreshRate)> resolutions = new();
        var devMode = new DEVMODE();
        devMode.dmSize = (short)Marshal.SizeOf(devMode);

        var i = 0;
        while (EnumDisplaySettings(null, i, ref devMode))
        {
            if (devMode is {dmPelsWidth: > 0, dmPelsHeight: > 0})
            {
                resolutions.Add((devMode.dmPelsWidth, devMode.dmPelsHeight, devMode.dmDisplayFrequency));
            }
            i++;
        }

        return resolutions;
    }
}