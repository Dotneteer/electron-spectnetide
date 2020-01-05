using ElectronNET.API;
using ElectronNET.API.Entities;
using Spect.Net.Shell.Interop;
using System;
using System.Collections.Generic;
using System.Text.RegularExpressions;

namespace Spect.Net.Shell.Menus
{
    /// <summary>
    /// This class represents an executable menu item with its UI-state information
    /// </summary>
    public class UiMenuItem : IMenuItemState, IDisposable
    {
        private string _label;

        /// <summary>
        /// Creates a new menu item with the specified attributes
        /// </summary>
        /// <param name="id">Item identifier</param>
        /// <param name="label">Initial label</param>
        /// <param name="role">Optional role</param>
        public UiMenuItem(string id = null, string label = null, MenuRole? role = null)
        {
            Id = id ?? Guid.NewGuid().ToString();
            Label = label;
            Role = role;
        }

        /// <summary>
        /// Optional Electron menu item
        /// </summary>
        public MenuItem MenuItem { get; set; }

        /// <summary>
        /// Item identifier
        /// </summary>
        public string Id { get; }

        /// <summary>
        /// Item label
        /// </summary>
        public string Label
        {
            get => _label;
            set
            {
                _label = value;
                if (string.IsNullOrWhiteSpace(_label)) return;
                var match = Regex.Match(_label, "&([^&])");
                AccessKey = match.Success ? match.Groups[1].Value : null;
            }
        }

        /// <summary>
        /// Item role
        /// </summary>
        public MenuRole? Role { get; }

        /// <summary>
        /// The accelerator key of the menu item
        /// </summary>
        public string Accelerator { get; private set; }

        /// <summary>
        /// The access key of the menu item
        /// </summary>
        public string AccessKey { get; private set; }

        /// <summary>
        /// Instructs the menu item to execute its associated action
        /// </summary>
        /// <param name="window">Host browser window</param>
        public virtual void OnExecute(BrowserWindow window) { }

        /// <summary>
        /// Instructs the menu item to update its status
        /// </summary>
        /// <param name="window">Host browser window</param>
        public virtual void OnQueryStatus(BrowserWindow window) { }

        /// <summary>
        /// Is the item enabled?
        /// </summary>
        public bool Enabled { get; set; } = true;

        /// <summary>
        /// Is the item visible?
        /// </summary>
        public bool Visible { get; set; } = true;

        /// <summary>
        /// Is the item checked?
        /// </summary>
        public bool Checked { get; set; } = false;

        /// <summary>
        /// Sets the Enabled flag of the item
        /// </summary>
        /// <param name="enabled">Enabled flag</param>
        /// <returns>This instance</returns>
        public UiMenuItem Enable(bool enabled)
        {
            Enabled = enabled;
            return this;
        }

        /// <summary>
        /// Sets the accelerator of the command
        /// </summary>
        /// <param name="accelerator">Accelerator string</param>
        /// <returns>This instance</returns>
        public UiMenuItem WithAccelerator(string accelerator)
        {
            Accelerator = accelerator;
            return this;
        }
        /// <summary>
        /// Subitems within this menu item
        /// </summary>
        public List<UiMenuItem> Items { get; } = new List<UiMenuItem>();

        /// <summary>
        /// Indicates that this item has subitems
        /// </summary>
        public bool HasSubitems => Items.Count > 0;

        /// <summary>
        /// Inserts a subitem to the specified position
        /// </summary>
        /// <param name="item">Item to insert</param>
        /// <param name="position">Zero-based position</param>
        /// <returns>This instance</returns>
        public UiMenuItem Insert(UiMenuItem item, int position)
        {
            if (item == null)
            {
                throw new ArgumentNullException(nameof(item));
            }
            if (position < 0)
            {
                Items.Insert(0, item);
            }
            else if (position >= Items.Count)
            {
                Items.Add(item);
            }
            else
            {
                Items.Insert(position, item);
            }
            return this;
        }

        /// <summary>
        /// Appends a new item to the end of the existing ones
        /// </summary>
        /// <param name="item">Item to append</param>
        /// <returns>This group instance</returns>
        public UiMenuItem Append(UiMenuItem item)
        {
            if (item == null)
            {
                throw new ArgumentNullException(nameof(item));
            }
            Items.Add(item);
            return this;
        }

        /// <summary>
        /// Updates the status of the item in the specified browser window
        /// </summary>
        /// <param name="window"></param>
        public void UpdateStatus(BrowserWindow window)
        {
            var oldLabel = Label;
            var oldEnabled = Enabled;
            var oldVisible = Visible;
            var oldChecked = Checked;

            OnQueryStatus(window);

            if (oldLabel != Label
                || oldEnabled != Enabled
                || oldVisible != Visible
                || oldChecked != Checked)
            {
                StatusUpdated?.Invoke(this, this);
            }
        }

        /// <summary>
        /// This event is fired when the status of a command has been updated.
        /// </summary>
        public event EventHandler<UiMenuItem> StatusUpdated;

        /// <summary>
        /// Override this method to dispose resources held by the command
        /// </summary>
        public virtual void Dispose() { }
    }
}
