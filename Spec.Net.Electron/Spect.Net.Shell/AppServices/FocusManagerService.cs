using Microsoft.AspNetCore.Components.Web;
using System;

namespace Spect.Net.Shell.AppServices
{
    /// <summary>
    /// This service is responsible for managing the focus-related events 
    /// *within* the application
    /// </summary>
    public class FocusManagerService : IFocusManagerService
    {
        /// <summary>
        /// This event signs that the focuse has been changed within the application
        /// </summary>
        public event EventHandler FocusChanged;

        /// <summary>
        /// This event signs that a kay has been pressed down in the app
        /// </summary>
        public event EventHandler<KeyboardEventArgs> AppKeyDown;

        /// <summary>
        /// This event signs that a kay has been released up in the app
        /// </summary>
        public event EventHandler<KeyboardEventArgs> AppKeyUp;

        /// <summary>
        /// This event signs that a kay has been pressed down in the app
        /// </summary>
        public void RaiseAppKeyDown(KeyboardEventArgs args)
        {
            AppKeyDown?.Invoke(this, args);
        }

        /// <summary>
        /// Raises the app key up event
        /// </summary>
        public void RaiseAppKeyUp(KeyboardEventArgs args)
        {
            AppKeyUp?.Invoke(this, args);
        }

        /// <summary>
        /// Raises the focus changed event
        /// </summary>
        public void RaiseFocusChanged()
        {
            FocusChanged?.Invoke(this, EventArgs.Empty);
        }
    }
}
