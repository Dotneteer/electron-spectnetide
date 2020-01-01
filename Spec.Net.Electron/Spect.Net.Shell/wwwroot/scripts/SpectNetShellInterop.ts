/**
 * Let's declare the DotNet object
 */
declare const DotNet: any;

/**
 * This class encapsulates the JavaScript interop methods
 */
class SpectNetShellJsInterop {
    /**
     * Test interop method
     */
    hello() {
        console.log("Hello!");
    }

    /**
     * Gets the dimensions of the specified element
     */
    getElementOffset(element: HTMLElement) {
        return {
            offsetLeft: element.offsetLeft,
            offsetTop: element.offsetTop,
            offsetWidth: element.offsetWidth,
            offsetHeight: element.offsetHeight
        }
    }
}

window["SpectNetShell"] = new SpectNetShellJsInterop();