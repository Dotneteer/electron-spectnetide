using System.Collections.Generic;

namespace Spect.Net.Shell.Menus
{
    /// <summary>
    /// This class describes information about menu panes opened in the main menu
    /// </summary>
    public class MenuPaneInfo
    {
        /// <summary>
        /// The index of the item in the parent, if this is a submenu pane
        /// </summary>
        public int ParentIndex { get; set; }

        /// <summary>
        /// Menu items in the pane
        /// </summary>
        public List<UiMenuItem> Items { get; set; }

        /// <summary>
        /// Left position in pixels
        /// </summary>
        public int LeftPos { get; set; }

        /// <summary>
        /// Top position in pixels
        /// </summary>
        public int TopPos { get; set; }

        /// <summary>
        /// The index of the selected item
        /// </summary>
        public int SelectedIndex { get; set; }

        /// <summary>
        /// Returns the flattened item structure
        /// </summary>
        public IEnumerable<UiMenuItem> ItemsFlattened
        {
            get
            {
                foreach (var item in Items)
                {
                    if (item.HasSubitems)
                    {
                        foreach (var subItem in item.Items)
                        {
                            yield return subItem;
                        }
                    }
                    else
                    {
                        yield return item;
                    }
                }
            }
        }
    }
}
