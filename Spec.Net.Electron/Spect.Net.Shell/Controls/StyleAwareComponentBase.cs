using Microsoft.AspNetCore.Components;
using Spect.Net.Shell.Helpers;
using System.Linq;
using System.Reflection;

namespace Spect.Net.Shell.Controls
{
    /// <summary>
    /// This class is intended to be the base class of all visual components that
    /// support adding base and extra CSS classes to their HTML markup
    /// </summary>
    public abstract class StyleAwareComponentBase: ComponentBase
    {
        private string _class;
        private object _xclass;
        private string _xclassString;

        [Parameter]
        public string Class
        {
            get => _class;
            set
            {
                if (_class != value)
                {
                    _class = value;
                    BuildClassValue();
                }
            }
        }

        [Parameter]
        public object XClass
        {
            get => _xclass;
            set
            {
                _xclass = value;
                BuildClassValue();
            }
        }

        protected sealed override void OnInitialized()
        {
            var classAttr = GetType().GetCustomAttributes()
                .FirstOrDefault(a => a.GetType() == typeof(ClassAttribute)) as ClassAttribute;
            Class = classAttr?.Value != null
                ? Class = classAttr.Value
                : Class = GetType().Name.ToCssName();
            OnInitializedInternal();
        }

        protected virtual void OnInitializedInternal()
        {
        }

        public string ClassValue => _xclassString ?? string.Empty;

        private void BuildClassValue()
        {
            var oldValue = _xclassString;
            _xclassString = new CssBuilder(_class).AddClass(_xclass).Build();
            if (oldValue != _xclassString)
            {
                StateHasChanged();
            }
        }
    }
}
