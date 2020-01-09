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
        public string ComponentId { get; }
        
        /// <summary>
        /// Current boundaries of the root element
        /// </summary>
        public ElementBoundaries Boundaries { get; }

        public BoundariesEventArgs(string componentId, ElementBoundaries boundaries)
        {
            ComponentId = componentId;
            Boundaries = boundaries;
        }
    }
}
