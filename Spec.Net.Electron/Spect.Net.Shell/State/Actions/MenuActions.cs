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

    /// <summary>
    /// Moves one menu item down in the current pane
    /// </summary>
    public sealed class MenuItemDownAction : MenuActionBase
    {
    }

    /// <summary>
    /// Moves one menu item up in the current pane
    /// </summary>
    public sealed class MenuItemUpAction : MenuActionBase
    {
    }

    /// <summary>
    /// Opens a new menu pane
    /// </summary>
    public sealed class MenuPaneOpenAction : MenuActionBase
    {
        public MenuPaneInfo Pane { get; }

        public bool KeyboardAction { get; }
        public MenuPaneOpenAction(MenuPaneInfo pane, bool keyboardAction)
        {
            Pane = pane;
            KeyboardAction = keyboardAction;
        }
    }

    /// <summary>
    /// Closes all displayed menu panes
    /// </summary>
    public sealed class MenuCloseAllPanesAction : MenuActionBase
    {
    }

    /// <summary>
    /// Selects the specified item
    /// </summary>
    public sealed class MenuItemSelectAction : MenuActionBase
    {
        public MenuItemSelectAction(int index)
        {
            Index = index;
        }

        public int Index { get; }
    }

    /// <summary>
    /// Selects the specified menu button and displays its pane
    /// </summary>
    public sealed class MenuButtonClickAction : MenuActionBase
    {
        public MenuButtonClickAction(MenuPaneInfo pane, int index)
        {
            Pane = pane;
            Index = index;
        }

        public MenuPaneInfo Pane { get; }
        public int Index { get; }
    }

    /// <summary>
    /// Keep the last pane while moving with the mouse
    /// </summary>
    public sealed class MenuKeepPaneAction : MenuActionBase
    {
        public MenuKeepPaneAction(int paneIndex)
        {
            PaneIndex = paneIndex;
        }

        public int PaneIndex { get; }
    }

    /// <summary>
    /// Point to a menu item in a menu pane
    /// </summary>
    public sealed class MenuItemPointAction : MenuActionBase
    {
        public MenuItemPointAction(int paneIndex, int itemIndex)
        {
            PaneIndex = paneIndex;
            ItemIndex = itemIndex;
        }

        public int PaneIndex { get; }
        public int ItemIndex { get; }

    }
}
