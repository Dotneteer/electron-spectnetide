using Spect.Net.Shell.Shared.State.Redux;

namespace Spect.Net.Shell.Shared.State.Actions
{
    /// <summary>
    /// This class represents the actions that belong to window state management.
    /// </summary>
    public abstract class WindowActionBase: ReducerAction
    {
    }

    /// <summary>
    /// Maximizes the app window
    /// </summary>
    public sealed class MaximizeWindowAction: WindowActionBase
    {
    }

    /// <summary>
    /// Minimizes the app window
    /// </summary>
    public sealed class MinimizeWindowAction : WindowActionBase
    {
    }

    /// <summary>
    /// Restore the app window to its last size
    /// </summary>
    public sealed class RestoreWindowAction : WindowActionBase
    {
    }

    /// <summary>
    /// Signs that the app got the focus
    /// </summary>
    public sealed class AppGotFocusAction : WindowActionBase
    {
    }

    /// <summary>
    /// Signs that the app lost the focus
    /// </summary>
    public sealed class AppLostFocusAction : WindowActionBase
    {
    }
}