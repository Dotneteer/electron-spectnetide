import {ipcRenderer, remote} from "electron";

/**
 * Let's declare the DotNet object
 */
declare const DotNet: any;

class SpectNetShellJsInterop {
    // ========================================================================
    // IPC
    /**
     * Sets up a channel the renderer process listens to
     * @param channel Channel name
     */
    public setupListener(channel: string): void {
        console.log(`Listener set up for ${channel}`)
        ipcRenderer.on(channel, (_event: any, response: any) => {
            console.log("Got it");
            DotNet.invokeMethodAsync("Spect.Net.Shell.Client", "HandleMessage", channel, response);
        });
    }

    /**
     * Sends a message to the specified channel
     * @param channel Channel name
     * @param message Message to send
     */
    public sendMessage(channel: string, message: any): void {
        ipcRenderer.send(channel, message);
        console.log(`Message ${message} sent on ${channel}`)
    }

    // ========================================================================
    // BrowserWindow
    /**
     * Checks if the current browser window is maximized.
     */
    public isBrowserWindowMaximized(): boolean {
        return remote.getCurrentWindow().isMaximized();
    }
}

window["SpectNetShell"] = new SpectNetShellJsInterop();