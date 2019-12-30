namespace Spect.Net.Shell.State.Redux
{
    /// <summary>
    /// Represents a method that is used as middleware.
    /// </summary>
    /// <typeparam name="TState">
    /// The state tree type.
    /// </typeparam>
    /// <param name="store">
    /// The IStore this middleware is to be used on.
    /// </param>
    /// <param name="action">Action to be handled</param>
    /// <returns>
    /// A boolean that represents if the middleware chain should be processed (true),
    /// or abandoned (false)
    /// </returns>
    public delegate bool Middleware<in TState>(IStore<TState> store, IReducerAction action);
}