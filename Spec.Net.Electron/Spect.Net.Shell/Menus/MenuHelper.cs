using System.Collections.Generic;

namespace Spect.Net.Shell.Menus
{
    public static class MenuHelper
    {
        /// <summary>
        /// Returns the flattened item structure
        /// </summary>
        public static IEnumerable<UiMenuItem> Flatten(this IEnumerable<UiMenuItem> items)
        {
            foreach (var item in items)
            {
                if (item.HasSubitems)
                {
                    foreach (var subItem in item.Items)
                    {
                        yield return subItem;
                    }
                }
                else
                {
                    yield return item;
                }
            }
        }

    }
}
