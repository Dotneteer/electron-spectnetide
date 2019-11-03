"use strict";
Object.defineProperty(exports, "__esModule", { value: true });
var electron_1 = require("electron");
var SpectNetShellJsInterop = /** @class */ (function () {
    function SpectNetShellJsInterop() {
    }
    // ========================================================================
    // IPC
    /**
     * Sets up a channel the renderer process listens to
     * @param channel Channel name
     */
    SpectNetShellJsInterop.prototype.setupListener = function (channel) {
        console.log("Listener set up for " + channel);
        electron_1.ipcRenderer.on(channel, function (_event, response) {
            console.log("Got it");
            DotNet.invokeMethodAsync("Spect.Net.Shell.Client", "HandleMessage", response);
        });
    };
    /**
     * Sends a message to the specified channel
     * @param channel Channel name
     * @param message Message to send
     */
    SpectNetShellJsInterop.prototype.sendMessage = function (channel, message) {
        electron_1.ipcRenderer.send(channel, message);
        console.log("Message " + message + " sent on " + channel);
    };
    // ========================================================================
    // BrowserWindow
    /**
     * Checks if the current browser window is maximized.
     */
    SpectNetShellJsInterop.prototype.isBrowserWindowMaximized = function () {
        return electron_1.remote.getCurrentWindow().isMaximized();
    };
    /**
     * Maximizes the current browser window
     */
    SpectNetShellJsInterop.prototype.maximize = function () {
        electron_1.remote.getCurrentWindow().maximize();
    };
    return SpectNetShellJsInterop;
}());
window["SpectNetShell"] = new SpectNetShellJsInterop();
//# sourceMappingURL=SpectNetShellInterop.js.map