using System;
using System.Linq;

namespace EPharma.Shared.Constants.Application
{
    public static class GeneralClass
    {
        public static bool In<T>(this T item, params T[] items)
        {
            if (items == null)
                throw new ArgumentNullException("items");

            return items.Contains(item);
        }
    }
}
