using System;
using System.Collections.Generic;

namespace Spect.Net.Shell.Shared.State.Redux
{
    /// <summary>
    /// This class implements the store
    /// </summary>
    /// <typeparam name="TState"></typeparam>
    public class Store<TState> : IStore<TState>
    {
        private readonly object _syncRoot = new object();
        private readonly IList<Reducer<TState>> _reducers;
        private readonly Middleware<TState>[] _middlewares;
        private TState _lastState;

        public Store(IList<Reducer<TState>> reducers,
            TState initialState = default,
            params Middleware<TState>[] middlewares)
        {
            _reducers = reducers;
            _middlewares = middlewares;
            _lastState = initialState;
        }

        public event Action<TState> StateChanged;

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
            var shouldFire = typeof(TState) is IEqualityComparer<TState>
                ? (oldState as IEqualityComparer<TState>).Equals(_lastState as IEqualityComparer<TState>)
                : (object)oldState == (object)_lastState;
            if (shouldFire)
            {
                StateChanged?.Invoke(_lastState);
            }
            return _lastState;
        }

        public TState GetState()
        {
            return _lastState;
        }
    }
}