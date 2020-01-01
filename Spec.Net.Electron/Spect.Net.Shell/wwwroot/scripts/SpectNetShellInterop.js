/**
 * This class encapsulates the JavaScript interop methods
 */
var SpectNetShellJsInterop = /** @class */ (function () {
    function SpectNetShellJsInterop() {
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
    return SpectNetShellJsInterop;
}());
window["SpectNetShell"] = new SpectNetShellJsInterop();
//# sourceMappingURL=SpectNetShellInterop.js.map