using System;

namespace Spect.Net.Shell.Controls
{
    public sealed class ClassAttribute: Attribute
    {
        public string Value { get; }

        public ClassAttribute(string value = null)
        {
            Value = value;
        }
    }
}
