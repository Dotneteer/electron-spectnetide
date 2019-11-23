using System;
using System.Collections.Generic;

namespace Spect.Net.Shell.State.Redux
{
    /// <summary>
    /// This class implements the application state store (redux).
    /// </summary>
    /// <typeparam name="TState">
    /// Type of object that represents the application state.
    /// </typeparam>
    /// <remarks>
    /// Use a separate instances for the *renderer* process (or processes)
    /// and the *main* process.
    /// </remarks>
    public class Store<TState> : IStore<TState>
    {
        private TState _lastState;
        private readonly IList<Reducer<TState>> _reducers;
        private readonly Middleware<TState>[] _middlewares;
        private readonly object _syncRoot = new object();

        /// <summary>
        /// Creates a new store with the specified list of reducers,
        /// optional initial state, and list of chained middleware
        /// objects.
        /// </summary>
        /// <param name="reducers">Reducer objects</param>
        /// <param name="initialState">Initial state of the store</param>
        /// <param name="middlewares">
        /// List of middlewares, chained from the firts to the last.
        /// </param>
        public Store(IList<Reducer<TState>> reducers,
            TState initialState = default,
            params Middleware<TState>[] middlewares)
        {
            _reducers = reducers;
            _middlewares = middlewares;
            _lastState = initialState;
        }

        /// <summary>
        /// Retrieves the current state of the store.
        /// </summary>
        public TState GetState() => _lastState;

        /// <summary>
        /// Dispatches the specified actions, and sets
        /// the new state of the store accordingly.
        /// </summary>
        /// <param name="action">Action to dispatch</param>
        /// <returns>
        /// The new state of the store.
        /// </returns>
        /// <remarks>
        /// The action forst goes through the chain of middleware objects,
        /// starting from the first one. Middleware objects can vote to abandone
        /// the dispatch process. Though they can, they should not change the
        /// app state.
        /// 
        /// If a middleware votes for abandon the dispatch process, the subsequent
        /// middleware objects are skipped; the reducer would not be dispatched.
        /// After going through the middlewares, all reducers are invoked, starting
        /// with the first.
        /// 
        /// Whenever the new state is different from the previous one, the StateChanged
        /// event is raised.
        /// </remarks>
        public TState Dispatch(IReducerAction action)
        {
            var oldState = _lastState;

            // --- Go through middlewares
            var goOn = true;
            foreach (var middleware in _middlewares)
            {
                if (!(goOn = middleware(this, action)))
                {
                    break;
                }
            }

            // --- Call the reducer, provided the middleware does
            // --- not abandon
            if (goOn)
            {
                lock (_syncRoot)
                {
                    foreach (var reducer in _reducers)
                    {
                        _lastState = reducer(_lastState, action);
                    }
                }
            }

            // --- Check for change
            var stateHasChanged = typeof(TState) is IEqualityComparer<TState>
                ? !(oldState as IEqualityComparer<TState>).Equals(_lastState as IEqualityComparer<TState>)
                : (object)oldState != (object)_lastState;
            if (stateHasChanged)
            {
                StateChanged?.Invoke(oldState, _lastState);
            }
            return _lastState;
        }

        /// <summary>
        /// This event is raised when the store's state has changed.
        /// The first argument of the event method is the previous app state;
        /// the second argument is the new state.
        /// </summary>
        public event Action<TState, TState> StateChanged;
    }
}