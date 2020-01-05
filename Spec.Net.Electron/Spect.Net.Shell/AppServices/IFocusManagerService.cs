using Microsoft.AspNetCore.Components.Web;
using System;

namespace Spect.Net.Shell.AppServices
{
    /// <summary>
    /// This service is responsible for managing the focus-related events 
    /// *within* the application
    /// </summary>
    interface IFocusManagerService
    {
        /// <summary>
        /// This event signs that the focuse has been changed within the application
        /// </summary>
        event EventHandler FocusChanged;

        /// <summary>
        /// Raises the focus changed event
        /// </summary>
        void RaiseFocusChanged();

        /// <summary>
        /// This event signs that a kay has been pressed down in the app
        /// </summary>
        event EventHandler<KeyboardEventArgs> AppKeyDown;

        /// <summary>
        /// Raises the app key down event
        /// </summary>
        void RaiseAppKeyDown(KeyboardEventArgs args);

        /// <summary>
        /// This event signs that a kay has been released up in the app
        /// </summary>
        event EventHandler<KeyboardEventArgs> AppKeyUp;

        /// <summary>
        /// Raises the app key up event
        /// </summary>
        void RaiseAppKeyUp(KeyboardEventArgs args);
    }
}
