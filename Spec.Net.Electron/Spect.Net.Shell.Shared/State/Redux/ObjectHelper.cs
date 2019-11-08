using System;
using System.Reflection;

namespace Spect.Net.Shell.Shared.State.Redux
{
    /// <summary>
    /// This class provides helpers for state management
    /// </summary>
    public static class ObjectHelper
    {
        /// <summary>
        /// Clones an object while setting new properties on it
        /// </summary>
        /// <typeparam name="TObj">Type of object to clone</typeparam>
        /// <param name="obj">Object instance to clone</param>
        /// <param name="assignAction">Actions for new property assignment</param>
        /// <returns>New cloned object</returns>
        public static TObj Assign<TObj>(this TObj obj, Action<TObj> assignAction = null)
            where TObj: class, new()
        {
            if (obj == null) throw new ArgumentNullException(nameof(obj));
            var result = new TObj();
            foreach (var propInfo in typeof(TObj).GetProperties(BindingFlags.Public | BindingFlags.Instance))
            {
                if (propInfo.CanRead && propInfo.CanWrite)
                {
                    propInfo.SetValue(result, propInfo.GetValue(obj));
                }
            }
            assignAction?.Invoke(result);
            return result;
        }
    }
}