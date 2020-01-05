using System;

namespace Spect.Net.Shell.Controls
{
    public sealed class ComponentClassAttribute: Attribute
    {
        public string Value { get; }

        public ComponentClassAttribute(string value = null)
        {
            Value = value;
        }
    }
}
