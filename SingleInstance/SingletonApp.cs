using static PW.SingleInstance.WinApi;


namespace PW.SingleInstance; 

public class SingletonApp : IDisposable {

  /// <summary>
  /// Creates a new instance, scoped to the current user's session only.
  /// </summary>
  public SingletonApp() : this(InstanceScope.Local) { }

  /// <summary>
  /// Creates a new instance.
  /// </summary>
  /// <param name="scope> <see cref="InstanceScope"/></param>
  public SingletonApp(InstanceScope scope) {

    string guid = EntryAssemblyCustomAttributes.AssemblyGuid();

    RegisteredWindowMessage = RegisterWindowMessage($"WM_SHOWFIRSTINSTANCE|{guid}");

    AppMutex = new Mutex(true, $"{scope}\\{guid}", out bool mutexCreated);
    AlreadyRunning = !mutexCreated;
  }

  public int RegisteredWindowMessage { get; }


  ///// <summary>
  ///// Registers a unique window message for the app. Do this first.
  ///// </summary>
  ///// <param name="uniqueAppId">A globally unique id for your app. A Guid is ideal.</param>
  ///// <returns></returns>
  //public static int RegisteryAppId(string uniqueAppId) => RegisterWindowMessage($"WM_SHOWFIRSTINSTANCE|{uniqueAppId}");


  private Mutex? AppMutex { get; }

  public bool AlreadyRunning { get; }



  /// <summary>
  /// Call from a non-first-instance app in order to activate the first instance.
  /// </summary>
  /// <param name="appRegistration"></param>
  public void ShowFirstInstance() {
    WinApi.PostMessage(
      (IntPtr)HWND_BROADCAST,
      RegisteredWindowMessage,
      IntPtr.Zero,
      IntPtr.Zero);
  }

  /// <summary>
  /// When a 'non-first-instance' app is launched it can call ShowFirstInstance(), to signal the 'first-instance' app to activate, via a windows broadcast message.
  /// When this windows message is received by the WndProc of the main form of the 'first-instance' app, call this method in order to become the active window.
  /// <example> 
  /// <code>
  /// Example:
  /// protected override void WndProc(ref Message message) {
  ///   if (message.Msg == id) SingletonApp.Activate(Handle); // Where id is the value returned by RegisteryAppId()
  ///   base.WndProc(ref message);
  /// }
  /// 
  /// </code>
  /// </example>
  /// </summary>
  /// <param name="hWnd">Window handle of the form to be activated.</param>
  public static void Activate(IntPtr hWnd) {
    ShowWindow(hWnd, SW_SHOW);
    SetForegroundWindow(hWnd);
  }

  public void Dispose() {
    AppMutex?.ReleaseMutex();
    GC.SuppressFinalize(this);
  }
}
