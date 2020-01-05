using Microsoft.AspNetCore.Components;
using Spect.Net.Shell.Interop;

namespace Spect.Net.Shell.Controls
{
    /// <summary>
    /// Represents event arguments related to boundary change
    /// </summary>
    public class BoundariesEventArgs
    {
        /// <summary>
        /// The component
        /// </summary>
        public ComponentBase Component { get; }
        
        /// <summary>
        /// Current boundaries of the root element
        /// </summary>
        public ElementBoundaries Boundaries { get; }

        public BoundariesEventArgs(ComponentBase component, ElementBoundaries boundaries)
        {
            Component = component;
            Boundaries = boundaries;
        }
    }
}
