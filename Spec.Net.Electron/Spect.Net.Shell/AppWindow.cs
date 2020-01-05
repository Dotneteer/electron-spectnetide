using ElectronNET.API;
using ElectronNET.API.Entities;
using Spect.Net.Shell.Commands;
using Spect.Net.Shell.Helpers;
using Spect.Net.Shell.Menus;
using Spect.Net.Shell.State;
using Spect.Net.Shell.State.Actions;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Spect.Net.Shell
{
    /// <summary>
    /// This class represents the main application window
    /// </summary>
    public class AppWindow
    {
        // --- The application menu
        private MenuItem[] _appMenu;

        // --- Map of commands (addressed by command IDs)
        private Dictionary<string, UiMenuItem> _commandMap;

        /// <summary>
        /// Gets the singleton instance of this AppWindow
        /// </summary>
        public static AppWindow Instance { get; private set; }

        /// <summary>
        /// Factory method to create the singleton instance
        /// </summary>
        /// <returns></returns>
        public static async Task Create()
        {
            if (Instance != null) return;
            var browserWindow = await Electron.WindowManager.CreateWindowAsync(new BrowserWindowOptions
            {
                Title = "ZX Spectrum IDE",
                Width = 1152,
                Height = 864,
                Show = true,
                Frame = true
            });
            Instance = new AppWindow(browserWindow);
            browserWindow.OnReadyToShow += () =>
            {
                Console.WriteLine("OnReadyToShow invoked");
                browserWindow.Show();
            };
            browserWindow.OnFocus += () => StateStore.Dispatch(new AppGotFocusAction());
            browserWindow.OnBlur += () => StateStore.Dispatch(new AppLostFocusAction());
            browserWindow.OnRestore += () => StateStore.Dispatch(new RestoreWindowAction());
            browserWindow.OnMaximize += () => StateStore.Dispatch(new MaximizeWindowAction());
            browserWindow.OnMinimize += () => StateStore.Dispatch(new MinimizeWindowAction());
            browserWindow.OnUnmaximize += () => StateStore.Dispatch(new RestoreWindowAction());
        }

        /// <summary>
        /// Gets the BrowserWindows that displays this app
        /// </summary>
        public BrowserWindow Window { get; }

        /// <summary>
        /// Represents the application's menu
        /// </summary>
        public UiMenuItem AppMenu { get; private set; }

        /// <summary>
        /// The private constructor that initializes the singleton
        /// class instance
        /// </summary>
        /// <param name="window">BrowserWindow instance wrapped in</param>
        private AppWindow(BrowserWindow window)
        {
            Window = window;
            //SetupMenu();
        }

        public void SetupMenu()
        {
            // --- Create the command structure that represents the main menu
            var aboutGroup = new UiMenuItem()
                .Append(new AboutCommand());

            var preferencesGroup = new UiMenuItem()
                .Append(new OptionsCommand());

            var servicesGroup = new UiMenuItem()
                .Append(new ElectronShellMenuItem(MenuRole.services));

            var appWindowGroup = new UiMenuItem()
                .Append(new ElectronShellMenuItem(MenuRole.hide))
                .Append(new ElectronShellMenuItem(MenuRole.hideothers))
                .Append(new ElectronShellMenuItem(MenuRole.unhide));

            var quitGroup = new UiMenuItem()
                .Append(new ElectronShellMenuItem(MenuRole.quit, "E&xit"));

            var darwinMenu = new UiMenuItem("darwin", "ZX Spectrum IDE")
                .Append(aboutGroup)
                .Append(preferencesGroup)
                .Append(servicesGroup)
                .Append(appWindowGroup)
                .Append(quitGroup);

            var createGroup = new UiMenuItem()
                .Append(new NewProjectCommand())
                .Append(new OpenProjectCommand());

            var closeGroup = new UiMenuItem()
                .Append(new CloseProjectCommand());

            var fileGroup = new UiMenuItem("file", PlatformHelper.IsDarwin ? "File" : "&File")
                .Append(createGroup)
                .Append(closeGroup);
            if (!PlatformHelper.IsDarwin)
            {
                fileGroup.Append(preferencesGroup);
            }
            fileGroup.Append(quitGroup);

            var explorerGroup = new UiMenuItem()
                .Append(new ShowExplorerCommand())
                .Append(new ShowSpectrumEmulatorCommand());

            var spectrumWindowsGroup = new UiMenuItem()
                .Append(new ShowRegistersCommand())
                .Append(new ShowDisassemblyCommand())
                .Append(new ShowMemoryCommand());

            var devToolsGroup = new UiMenuItem()
                .Append(new ShowDevToolsCommand());

            var viewGroup = new UiMenuItem("view", PlatformHelper.IsDarwin ? "View" : "&View")
                .Append(explorerGroup)
                .Append(spectrumWindowsGroup)
                .Append(devToolsGroup);

            var help1Group = new UiMenuItem()
                .Append(new UiMenuItem("help-topic-1", "Help topic #1").Enable(false))
                .Append(new UiMenuItem("help-topic-2", "Help topic #2").Enable(false));

            var help3SubGroup = new UiMenuItem("help-topic-3", "Help topic #&3")
                .Append(new UiMenuItem("help-topic-31", "Help topic #31"))
                .Append(new UiMenuItem("help-topic-32", "Help topic #32"))
                .Append(new UiMenuItem("help-topic-33", "Help topic #33"));

            var help4SubGroup = new UiMenuItem("help-topic-4", "Help topic #4").Enable(false)
                .Append(new UiMenuItem("help-topic-41", "Help topic #41"))
                .Append(new UiMenuItem("help-topic-42", "Help topic #42"))
                .Append(new UiMenuItem("help-topic-43", "Help topic #43"));

            var help2Group = new UiMenuItem()
                .Append(help3SubGroup)
                .Append(help4SubGroup);

            var helpMenu = new UiMenuItem("help", "H&elp")
                .Append(help1Group)
                .Append(help2Group);

            AppMenu = new UiMenuItem();
            if (PlatformHelper.IsDarwin)
            {
                AppMenu.Append(darwinMenu);
            }
            AppMenu
                .Append(fileGroup)
                .Append(viewGroup)
                .Append(helpMenu);

            // --- Convert the command structure into Electron menu
            BuildDefaultMenuFromCommands();

            // --- Now, set the menu
            try
            {
                Electron.Menu.SetApplicationMenu(_appMenu);
                Console.WriteLine("Electron menu set");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"App Menu Error: {ex}");
            }

            // --- Set the menu state
            Console.WriteLine("Setting app menu");
            StateStore.Dispatch(new SetAppMenuAction(AppMenu));
        }

        /// <summary>
        /// Builds the Electron menu from the command structure
        /// </summary>
        /// <returns></returns>
        private void BuildDefaultMenuFromCommands()
        {
            var topItems = new List<MenuItem>();
            _commandMap = new Dictionary<string, UiMenuItem>();
            foreach (var paneItem in AppMenu.Items)
            {
                if (string.IsNullOrWhiteSpace(paneItem.Label))
                {
                    throw new InvalidOperationException("Top level menu item must have a label");
                }
                if (paneItem.Items.Count == 0)
                {
                    throw new InvalidOperationException("Top level menu item must have at least one subcommand");
                }
                var menuPane = BuildMenuPaneFromCommands(paneItem);
                topItems.Add(menuPane);
            }
            _appMenu = topItems.ToArray();
        }

        /// <summary>
        /// Builds a menu pane from the specified command group
        /// </summary>
        /// <param name="menuGroup"></param>
        /// <returns></returns>
        private MenuItem BuildMenuPaneFromCommands(UiMenuItem menuGroup)
        {
            var separator = new MenuItem { Type = MenuType.separator };
            var pane = new List<MenuItem>();
            var lastItemWasGroup = false;
            var groupJustEnded = false;
            for (var i = 0; i < menuGroup.Items.Count; i++)
            {
                var subitem = menuGroup.Items[i];

                // --- Provide separator between groups
                if (i > 0 && subitem.HasSubitems != lastItemWasGroup || groupJustEnded)
                {
                    pane.Add(separator);
                }
                lastItemWasGroup = subitem.HasSubitems;
                groupJustEnded = false;
                if (subitem.HasSubitems)
                {
                    // --- We are about to process a command group
                    foreach (var item in subitem.Items)
                    {
                        if (item.HasSubitems)
                        {
                            // --- This is a submenu to render
                            var submenu = BuildMenuPaneFromCommands(item);
                            pane.Add(submenu);
                        }
                        else if (item.Role != null)
                        {
                            // --- An Electron Shell predefined role
                            pane.Add(new MenuItem
                            {
                                Type = MenuType.normal,
                                Role = item.Role.Value
                            });
                        }
                        else
                        {
                            // --- Normal menu item
                            pane.Add(new MenuItem
                            {
                                Label = item.Label,
                                Accelerator = item.Accelerator,
                                Click = () => item.OnExecute(Window)
                            });

                            // --- Let's map this item by its ID
                            _commandMap[item.Id] = item;
                        }
                    }
                    groupJustEnded = true;
                }
                else {
                    // --- Normal menu item
                    pane.Add(new MenuItem
                    {
                        Label = subitem.Label,
                        Accelerator = subitem.Accelerator,
                        Click = () => subitem.OnExecute(Window)
                    });

                    // --- Let's map this item by its ID
                    _commandMap[subitem.Id] = subitem;
                }
            }

            // --- Done
            return new MenuItem
            {
                Label = menuGroup.Label,
                Submenu = pane.ToArray()
            };
        }
    }
}