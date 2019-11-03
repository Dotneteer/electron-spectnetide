"use strict";
Object.defineProperty(exports, "__esModule", { value: true });
var electron_1 = require("electron");
var SpectNetShellJsInterop = /** @class */ (function () {
    function SpectNetShellJsInterop() {
    }
    SpectNetShellJsInterop.prototype.hello = function () {
        console.log("Hello...");
    };
    /**
     * Sets up a channel the renderer process listens to
     * @param channel Channel name
     */
    SpectNetShellJsInterop.prototype.setupListener = function (channel) {
        electron_1.ipcRenderer.on(channel, function (_event, response) {
            console.log(JSON.stringify(response, null, 2));
        });
    };
    /**
     * Sends a message to the specified channel
     * @param channel Channel name
     * @param message Message to send
     */
    SpectNetShellJsInterop.prototype.sendMessage = function (channel, message) {
        electron_1.ipcRenderer.send(channel, message);
    };
    return SpectNetShellJsInterop;
}());
window["SpectNetShell"] = new SpectNetShellJsInterop();
//# sourceMappingURL=SpectNetShellInterop.js.map