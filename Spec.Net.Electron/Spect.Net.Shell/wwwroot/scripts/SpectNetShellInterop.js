var ASM_NAME = "Spect.Net.Shell";
/**
 * This class encapsulates the JavaScript interop methods
 */
var SpectNetShellJsInterop = /** @class */ (function () {
    function SpectNetShellJsInterop() {
        // --- The element that had the focus last time
        this._lastFocus = null;
        this._currentFocus = null;
    }
    /**
     * Test interop method
     */
    SpectNetShellJsInterop.prototype.hello = function () {
        console.log("Hello!");
    };
    /**
     * Gets the dimensions of the specified element
     */
    SpectNetShellJsInterop.prototype.getElementOffset = function (element) {
        return {
            offsetLeft: element.offsetLeft,
            offsetTop: element.offsetTop,
            offsetWidth: element.offsetWidth,
            offsetHeight: element.offsetHeight
        };
    };
    // ========================================================================
    // Focus handling
    /**
     * This function starts checking the focus changes
     */
    SpectNetShellJsInterop.prototype.checkFocusChange = function () {
        var service = this;
        requestAnimationFrame(function () {
            // --- Do check
            var newFocus = document.activeElement;
            if (newFocus !== service._lastFocus) {
                DotNet.invokeMethodAsync(ASM_NAME, "HandleFocusChangeAsync")
                    .then(function () { return service._lastFocus = newFocus; });
            }
            // --- Next request
            service.checkFocusChange();
        });
    };
    /**
     * Checks if the currently focused element is within the specified element
     * @param parent
     */
    SpectNetShellJsInterop.prototype.isFocusedWithinElement = function (parent) {
        return this._currentFocus !== null
            && this.isDescendant(parent, this._currentFocus);
    };
    // ========================================================================
    // Helpers
    /**
     * Tests if the parent element contains the child element
     * @param parent Parent HTML element
     * @param child Child HTML element
     */
    SpectNetShellJsInterop.prototype.isDescendant = function (parent, child) {
        var node = child.parentNode;
        while (node !== null) {
            if (node === parent) {
                return true;
            }
            node = node.parentNode;
        }
        return false;
    };
    return SpectNetShellJsInterop;
}());
window["SpectNetShell"] = new SpectNetShellJsInterop();
//# sourceMappingURL=SpectNetShellInterop.js.map