using System;
using System.Collections.Generic;
using System.Linq;

namespace Terminal.Helpers;

public static class EnumerableExtensions
{
    public static bool HasDuplicates<TSource, TKey>(
        this IEnumerable<TSource> source,
        Func<TSource, TKey> keySelector
    )
    {
        return source.GroupBy(keySelector).Any(g => g.Count() > 1);
    }
}