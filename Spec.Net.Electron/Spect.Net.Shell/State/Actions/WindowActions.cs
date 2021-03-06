﻿using Spect.Net.Shell.State.Redux;

namespace Spect.Net.Shell.State.Actions
{
    /// <summary>
    /// This class is the common base class of actions that belong to window state management.
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
    /// Close the app window
    /// </summary>
    public sealed class CloseWindowAction : WindowActionBase
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