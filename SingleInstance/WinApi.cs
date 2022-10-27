using System.Runtime.InteropServices;

namespace PW.SingleInstance;

internal static class WinApi
{

  public const int HWND_BROADCAST = 0xffff;
  public const int SW_SHOWNORMAL = 1;
  public const int SW_SHOW = 5;


  [DllImport("user32", CharSet = CharSet.Unicode)]
  public static extern int RegisterWindowMessage(string message);


  [DllImport("user32")]
  public static extern bool PostMessage(IntPtr hwnd, int msg, IntPtr wparam, IntPtr lparam);


  [DllImport("user32.dll")]
  public static extern bool ShowWindow(IntPtr hWnd, int nCmdShow);


  [DllImport("user32.dll")]
  public static extern bool SetForegroundWindow(IntPtr hWnd);

}
