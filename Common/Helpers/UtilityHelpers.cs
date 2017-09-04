using System;
using System.Collections.Generic;

namespace FreeParkingSystem.Common.Helpers
{
    public static class UtilityHelpers
    {

        /// <summary>
        /// Gets the index of the element that fits the <paramref name="predicate"/>
        /// </summary>
        /// <typeparam name="TEntity"></typeparam>
        /// <param name="list"></param>
        /// <param name="predicate"></param>
        /// <returns></returns>
        public static int GetIndex<TEntity>(this IList<TEntity> list, Func<TEntity, bool> predicate)
        {
            if (list == null || list.Count == 0) return -1;

            for (int index = 0; index < list.Count; index++)
            {
                if (predicate(list[index]))
                {
                    return index;
                }
            }

            return -1;
        }
    }
}
