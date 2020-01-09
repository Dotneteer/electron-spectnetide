using Spect.Net.Shell.Interop;

namespace Spect.Net.Shell.Controls
{
    /// <summary>
    /// Represents event arguments related to boundary change
    /// </summary>
    public class BoundariesEventArgs
    {
        /// <summary>
        /// The ID of the component
        /// </summary>
        public string ComponentId { get; }
        
        /// <summary>
        /// Current boundaries of the root element
        /// </summary>
        public ElementBoundaries Boundaries { get; }

        /// <summary>
        /// The ID of the parent component (optional)
        /// </summary>
        public string ParentComponentId { get; }

        public BoundariesEventArgs(string componentId, ElementBoundaries boundaries, string parentComponentId = null)
        {
            ComponentId = componentId;
            Boundaries = boundaries;
            ParentComponentId = parentComponentId;
        }
    }
}
