using Spect.Net.Shell.Menus;
using Spect.Net.Shell.State.Redux;

namespace Spect.Net.Shell.State.Actions
{
    /// <summary>
    /// This class is the common base class of actions that belong to app menu state management.
    /// </summary>
    public abstract class MenuActionBase : ReducerAction
    {
    }

    /// <summary>
    /// Represents the action that sets the app menu
    /// </summary>
    public sealed class SetAppMenuAction : MenuActionBase
    {
        public UiMenuItem AppMenu { get; }
        public SetAppMenuAction(UiMenuItem appMenu)
        {
            AppMenu = appMenu;
        }
    }
}
