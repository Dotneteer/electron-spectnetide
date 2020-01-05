/**
 * Let's declare the DotNet object
 */
declare const DotNet: any;

const ASM_NAME = "Spect.Net.Shell";

/**
 * This class encapsulates the JavaScript interop methods
 */
class SpectNetShellJsInterop {
    // --- The element that had the focus last time
    private _lastFocus: HTMLElement | null = null;
    private _currentFocus: HTMLElement | null = null;

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

    // ========================================================================
    // Focus handling

    /**
     * This function starts checking the focus changes
     */
    checkFocusChange(): void {
        const service = this;
        requestAnimationFrame(() => {
            // --- Do check
            const newFocus = document.activeElement as HTMLElement;
            if (newFocus !== service._lastFocus) {
                DotNet.invokeMethodAsync(ASM_NAME, "HandleFocusChangeAsync")
                    .then(() => service._lastFocus = newFocus)
            }

            // --- Next request
            service.checkFocusChange();
        });
    }

    /**
     * Checks if the currently focused element is within the specified element
     * @param parent
     */
    isFocusedWithinElement(parent: HTMLElement): boolean {
        return this._currentFocus !== null
            && this.isDescendant(parent, this._currentFocus);
    }

    // ========================================================================
    // Helpers

    /**
     * Tests if the parent element contains the child element
     * @param parent Parent HTML element
     * @param child Child HTML element
     */
    isDescendant(parent: HTMLElement, child: HTMLElement) {
        let node = child.parentNode;
        while (node !== null) {
            if (node === parent) {
                return true;
            }
            node = node.parentNode;
        }
        return false;
    }
}

window["SpectNetShell"] = new SpectNetShellJsInterop();