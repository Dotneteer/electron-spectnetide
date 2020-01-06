using System;
using Spect.Net.Shell.Controls;
using Spect.Net.Shell.State.Redux;

namespace Spect.Net.Shell.State
{
    /// <summary>
    /// This class implements a components that is aware of the current
    /// application state.
    /// </summary>
    public abstract class StateAwareComponentBase: StyleAwareComponentBase
    {
        /// <summary>
        /// Constructs an instance of <see cref="T:Microsoft.AspNetCore.Components.ComponentBase" />.
        /// </summary>
        protected StateAwareComponentBase()
        {
            StateStore.StateChange += HandleStateChanged;
        }

        /// <summary>
        /// Dispatches the specified action.
        /// </summary>
        /// <param name="action">Action to dispatch</param>
        public void Dispatch(IReducerAction action)
        {
            StateStore.Dispatch(action);
        }

        /// <summary>
        /// Gets the application state.
        /// </summary>
        protected AppState AppState => StateStore.GetState();

        /// <summary>
        /// Override this method to handle application state changes.
        /// </summary>
        /// <param name="prevState">Previous application state</param>
        /// <param name="newState">New application state</param>
        protected virtual void OnStateChanged(AppState prevState, AppState newState)
        {
            StateHasChanged();
        }

        /// <summary>
        /// Check if state change should be signed. Override this method to check if
        /// the partial state in which the component is interested has changed.
        /// </summary>
        /// <param name="prevState">Previous application state</param>
        /// <param name="newState">New application state</param>
        /// <returns>True, if state change should be signed; otherwise, false.</returns>
        protected virtual bool HasChanged(AppState prevState, AppState newState) => prevState != newState;

        /// <summary>
        /// This method is assigned to the StateChanged event of the RendererStore
        /// </summary>
        private void HandleStateChanged(AppState prevState, AppState newState)
        {
            if (HasChanged(prevState, newState))
            {
                OnStateChanged(prevState, newState);
            }
        }

        /// <summary>
        /// Performs application-defined tasks associated with freeing, releasing,
        /// or resetting unmanaged resources.
        /// </summary>
        public override void Dispose()
        {
            StateStore.StateChange -= HandleStateChanged;
        }
    }
}