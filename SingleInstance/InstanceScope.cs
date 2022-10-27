namespace PW.SingleInstance;

/// <summary>
/// Scope to which the application will be single-instance. 
/// </summary>
public enum InstanceScope
{
  /// <summary>
  /// Single-instance within this user's session.
  /// </summary>
  Local,
  /// <summary>
  /// Single-instance across all user's sessions, including terminal services.
  /// </summary>
  Global
}

