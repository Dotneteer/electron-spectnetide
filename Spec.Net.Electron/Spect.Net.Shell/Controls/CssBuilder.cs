﻿using System;

namespace Spect.Net.Shell.Controls
{
    public struct CssBuilder
    {
        private string _stringBuffer;

        /// <summary>
        /// Creates a CssBuilder used to define conditional CSS classes used in a component.
        /// Call Build() to return the completed CSS Classes as a string. 
        /// </summary>
        /// <param name="value"></param>
        public CssBuilder(string value) => _stringBuffer = value;

        /// <summary>
        /// Adds a raw string to the builder that will be concatenated with the next class or value added to the builder.
        /// </summary>
        /// <param name="value"></param>
        /// <returns>CssBuilder</returns>
        public CssBuilder AddValue(string value)
        {
            _stringBuffer += value;
            return this;
        }

        /// <summary>
        /// Adds a CSS Class to the builder with space separator.
        /// </summary>
        /// <param name="value">CSS Class to add</param>
        /// <returns>CssBuilder</returns>
        public CssBuilder AddClass(string value) => AddValue(" " + value);

        /// <summary>
        /// Adds a conditional CSS Class to the builder with space separator.
        /// </summary>
        /// <param name="value">CSS Class to conditionally add.</param>
        /// <param name="when">Condition in which the CSS Class is added.</param>
        /// <returns>CssBuilder</returns>
        public CssBuilder AddClass(string value, bool when = true) => when ? AddClass(value) : this;

        /// <summary>
        /// Adds a conditional CSS Class to the builder with space separator.
        /// </summary>
        /// <param name="value">CSS Class to conditionally add.</param>
        /// <param name="when">Condition in which the CSS Class is added.</param>
        /// <returns>CssBuilder</returns>
        public CssBuilder AddClass(string value, Func<bool> when) => AddClass(value, when());

        /// <summary>
        /// Adds a conditional CSS Class to the builder with space separator.
        /// </summary>
        /// <param name="builder">CSS Class to conditionally add.</param>
        /// <param name="when">Condition in which the CSS Class is added.</param>
        /// <returns>CssBuilder</returns>
        public CssBuilder AddClass(CssBuilder builder, bool when = true) => when ? AddClass(builder.Build()) : this;

        /// <summary>
        /// Adds a conditional CSS Class to the builder with space separator.
        /// </summary>
        /// <param name="builder">CSS Class to conditionally add.</param>
        /// <param name="when">Condition in which the CSS Class is added.</param>
        /// <returns>CssBuilder</returns>
        public CssBuilder AddClass(CssBuilder builder, Func<bool> when) => AddClass(builder, when());

        /// <summary>
        /// Finalize the completed CSS Classes as a string.
        /// </summary>
        /// <returns>string</returns>
        public string Build()
        {
            // String buffer finalization code
            return _stringBuffer != null ? _stringBuffer.Trim() : string.Empty;
        }

        // ToString should only and always call Build to finalize the rendered string.
        public override string ToString() => Build();
    }
}