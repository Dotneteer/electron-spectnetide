import {ipcRenderer} from "electron";

class SpectNetShellJsInterop {
    public hello(): void {
        console.log("Hello...");
    }

    /**
     * Sets up a channel the renderer process listens to
     * @param channel Channel name
     */
    public setupListener(channel: string): void {
        ipcRenderer.on(channel, (_event: any, response: any) => {
            console.log(JSON.stringify(response, null, 2));
        });
    }

    /**
     * Sends a message to the specified channel
     * @param channel Channel name
     * @param message Message to send
     */
    public sendMessage(channel: string, message: any): void {
        ipcRenderer.send(channel, message);
    }
}

window["SpectNetShell"] = new SpectNetShellJsInterop();