﻿using System;

namespace Spect.Net.Shell.Client.Themes
{
    /// <summary>
    /// This attribute signs that a specific Theme property is not a
    /// CSS variable.
    /// </summary>
    [AttributeUsage(AttributeTargets.Property)]
    public class NonCssAttribute : Attribute
    {
    }
}
