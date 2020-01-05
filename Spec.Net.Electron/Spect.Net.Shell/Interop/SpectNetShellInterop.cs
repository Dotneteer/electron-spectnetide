using System.Threading.Tasks;
using Microsoft.AspNetCore.Components;
using Microsoft.JSInterop;

namespace Spect.Net.Shell.Interop
{
    public static class SpectNetShellInterop
    {
        /// <summary>
        /// Creates a new editor with the specified id
        /// </summary>
        /// <param name="jsRuntime">JS runtime object</param>
        public static async Task Hello(this IJSRuntime jsRuntime)
        {
            await jsRuntime.InvokeAsync<object>("SpectNetShell.hello");
        }

        /// <summary>
        /// Gets the element offset dimensions
        /// </summary>
        /// <param name="jsRuntime">JS runtime object</param>
        /// <param name="element">HTML element reference</param>
        /// <returns>Element dimensions</returns>
        public static async Task<ElementBoundaries> GetElementOffset(this IJSRuntime jsRuntime, ElementReference element)
        {
            return await jsRuntime.InvokeAsync<ElementBoundaries>("SpectNetShell.getElementOffset", element);
        }

        /// <summary>
        /// Start focus change checking
        /// </summary>
        /// <param name="jsRuntime">JS runtime object</param>
        /// <returns>Element dimensions</returns>
        public static async Task StartFocusChangeCheck(this IJSRuntime jsRuntime)
        {
            await jsRuntime.InvokeAsync<object>("SpectNetShell.checkFocusChange");
        }
    }
}
