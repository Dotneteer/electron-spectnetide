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

    /// <summary>
    /// The ALT key has been pressed
    /// </summary>
    public sealed class MenuAltPressedAction : MenuActionBase 
    {
    }

    /// <summary>
    /// The ALT key has been released
    /// </summary>
    public sealed class MenuAltReleasedAction : MenuActionBase
    {
    }

    /// <summary>
    /// Close the last open menu pane
    /// </summary>
    public sealed class MenuPaneClosedAction : MenuActionBase
    {
    }

    /// <summary>
    /// Set a menu button
    /// </summary>
    public sealed class MenuButtonSetAction : MenuActionBase
    {
        public int ItemIndex { get; }
        public MenuPaneInfo Pane { get; }
        public bool KeyboardAction { get; }

        public MenuButtonSetAction(int itemIndex, MenuPaneInfo pane, bool keyboardAction)
        {
            ItemIndex = itemIndex;
            Pane = pane;
            KeyboardAction = keyboardAction;
        }
    }
}
