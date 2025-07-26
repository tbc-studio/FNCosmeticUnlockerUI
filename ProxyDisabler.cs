using System;
using System.Runtime.InteropServices;
using FNCosmeticUnlockerUI;

class ProxyDisabler
{
    [DllImport("wininet.dll", SetLastError = true)]
    public static extern bool InternetSetOption(IntPtr hInternet, int dwOption, IntPtr lpBuffer, int dwBufferLength);

    const int INTERNET_OPTION_SETTINGS_CHANGED = 39;
    const int INTERNET_OPTION_REFRESH = 37;

    public static void DisableSystemProxy(object sender, EventArgs e)
    {
        Microsoft.Win32.RegistryKey registry = Microsoft.Win32.Registry.CurrentUser.OpenSubKey(
            @"Software\Microsoft\Windows\CurrentVersion\Internet Settings", true);

        if (registry != null)
        {
            registry.SetValue("ProxyEnable", 0); // Disable proxy
            registry.Close();
        }

        // Notify the system of the change
        InternetSetOption(IntPtr.Zero, INTERNET_OPTION_SETTINGS_CHANGED, IntPtr.Zero, 0);
        InternetSetOption(IntPtr.Zero, INTERNET_OPTION_REFRESH, IntPtr.Zero, 0);

        Form1.AppendLog("System proxy disabled successfully.");
    }
}
