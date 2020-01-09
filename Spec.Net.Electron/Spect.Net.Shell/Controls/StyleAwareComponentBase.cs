using Microsoft.AspNetCore.Components;
using Microsoft.JSInterop;
using Spect.Net.Shell.Helpers;
using Spect.Net.Shell.Interop;
using System;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;

namespace Spect.Net.Shell.Controls
{
    /// <summary>
    /// This class is intended to be the base class of all visual components that
    /// support adding base and extra CSS classes to their HTML markup
    /// </summary>
    public abstract class StyleAwareComponentBase: ComponentBase, IDisposable
    {
        [Inject]
        protected IJSRuntime JsRuntime { get; set; }

        private string _class;
        private object _xclass;
        private string _xclassString;
        private ElementReference _rootElement;
        private bool _rootElementSet;
        private ElementBoundaries _elementBoundaries;

        /// <summary>
        /// Use this field to assig it to the root element of the component
        /// </summary>
        protected ElementReference RootElement
        {
            get => _rootElement;
            set
            {
                _rootElement = value;
                _rootElementSet = true;
            }
        }

        /// <summary>
        /// The identifier of this component that makes it unique for its parent component
        /// </summary>
        [Parameter]
        public string ComponentInstanceID { get; set; } = Guid.NewGuid().ToString();

        /// <summary>
        /// Use this parameter to set the extra classes to be used
        /// </summary>
        [Parameter]
        public string Class
        {
            get => _xclass.ToString();
            set
            {
                _xclass = value;
                BuildClassValue();
            }
        }

        /// <summary>
        /// Use this parameter to set the extra classes to be used
        /// </summary>
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

        /// <summary>
        /// This event is raised when element boundaries has been changed
        /// </summary>
        [Parameter]
        public EventCallback<BoundariesEventArgs> BoundariesChanged { get; set; }

        /// <summary>
        /// Use the value of this attribute to set the class attribute
        /// within the component markup
        /// </summary>
        public string ClassValue => _xclassString ?? string.Empty;

        /// <summary>
        /// Takes care to set up the ClassValue property
        /// </summary>
        protected override void OnInitialized()
        {
            var classAttr = GetType().GetCustomAttributes()
                .FirstOrDefault(a => a.GetType() == typeof(ComponentClassAttribute)) as ComponentClassAttribute;
            _class = classAttr?.Value != null
                ? classAttr.Value
                : GetType().Name.ToCssName();
            BuildClassValue();
        }

        /// <summary>
        /// Queries element boundaries after the last render cycle has been completed.
        /// </summary>
        protected override async Task OnAfterRenderAsync(bool firstRender)
        {
            if (_rootElementSet)
            {
                var oldElementBoundaries = _elementBoundaries;
                _elementBoundaries = await JsRuntime.GetElementOffset(_rootElement);
                if (!_elementBoundaries.Equals(oldElementBoundaries))
                {
                    await BoundariesChanged.InvokeAsync(
                        new BoundariesEventArgs(ComponentInstanceID, _elementBoundaries));
                }
            }
        }

        /// <summary>
        /// Override this method to handle component disposal
        /// </summary>
        public virtual void Dispose()
        {
        }

        /// <summary>
        /// Builds the value of class property.
        /// </summary>
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
