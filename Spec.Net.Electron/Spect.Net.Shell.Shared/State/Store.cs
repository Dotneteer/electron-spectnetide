using System;

namespace Spect.Net.Shell.Shared.State
{
    public class Store<TState> : IStore<TState>
    {
        private readonly object _syncRoot = new object();
        private readonly Dispatcher _dispatcher;
        private readonly Reducer<TState> _reducer;
        private TState _lastState;
        private Action _stateChanged;

        public Store(Reducer<TState> reducer, 
            TState initialState = default, 
            params Middleware<TState>[] middlewares)
        {
            _reducer = reducer;
            _dispatcher = ApplyMiddlewares(middlewares);

            _lastState = initialState;
        }

        public event Action StateChanged
        {
            add
            {
                value();
                _stateChanged += value;
            }
            // ReSharper disable once DelegateSubtraction
            remove => _stateChanged -= value;
        }

        public object Dispatch(object action)
        {
            return _dispatcher(action);
        }

        public TState GetState()
        {
            return _lastState;
        }

        private Dispatcher ApplyMiddlewares(params Middleware<TState>[] middlewares)
        {
            Dispatcher dispatcher = InnerDispatch;
            foreach (var middleware in middlewares)
            {
                dispatcher = middleware(this)(dispatcher);
            }
            return dispatcher;
        }

        private object InnerDispatch(object action)
        {
            lock (_syncRoot)
            {
                _lastState = _reducer(_lastState, action);
            }

            _stateChanged?.Invoke();

            return action;
        }
    }
}