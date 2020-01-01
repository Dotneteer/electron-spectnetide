namespace Spect.Net.Shell.Interop
{
    /// <summary>
    /// This class represents the dimensions of en element
    /// </summary>
    public class ElementOffset
    {
        /// <summary>
        /// An integer representing the offset to the left in pixels from the 
        /// closest relatively positioned parent element.
        /// </summary>
        public int OffsetLeft { get; set; }

        /// <summary>
        /// An integer representing the offset to the top in pixels from the 
        /// closest relatively positioned parent element.
        /// </summary>
        public int OffsetTop { get; set; }

        /// <summary>
        /// An integer corresponding to the offsetWidth pixel value of the element.
        /// </summary>
        public int OffsetWidth { get; set; }

        /// <summary>
        /// An integer corresponding to the offsetHeight pixel value of the element.
        /// </summary>
        public int OffsetHeight { get; set; }
    }
}
