using System;
using System.Collections.Generic;
using System.Linq;

public static class CollectionExtensions
{
    public static bool None<T>(this T[] items) => !items.AnyNonAlloc();
    public static bool None<T>(this HashSet<T> items) => !items.AnyNonAlloc();
    public static bool None<T>(this List<T> items) => !items.AnyNonAlloc();
    public static bool NoneNonAlloc<T>(this T[] items) => !items.AnyNonAlloc();
    public static bool NoneNonAlloc<T>(this HashSet<T> items) => !items.AnyNonAlloc();
    public static bool NoneNonAlloc<T>(this List<T> items) => !items.AnyNonAlloc();
    public static bool NoneNonAlloc<T>(this IEnumerable<T> items, Func<T, bool> condition)
    {
        foreach (var i in items)
            if (condition(i))
                return false;

        return true;
    }
    
    public static bool AnyNonAlloc<T>(this T[] items) => items.Length > 0;
    public static bool AnyNonAlloc<T>(this List<T> items) => items.Count > 0;
    public static bool AnyNonAlloc<T>(this Queue<T> items) => items.Count > 0;
    public static bool AnyNonAlloc<T>(this HashSet<T> items) => items.Count > 0;
    public static bool AnyNonAlloc<T>(this IEnumerable<T> items, Func<T, bool> condition)
    {
        foreach (var i in items)
            if (condition(i))
                return true;

        return false;
    }
    
    public static bool AllNonAlloc<T>(this IEnumerable<T> items, Func<T, bool> condition)
    {
        foreach (var i in items)
            if (!condition(i))
                return false;

        return true;
    }
    
    public static T[] AsArray<T>(this T item) => new [] {item};
    public static TValue ValueOrDefault<TKey, TValue>(this IDictionary<TKey, TValue> d, TKey key, Func<TValue> getDefault) => d.TryGetValue(key, out var value) ? value : getDefault();

    public static void ForEach<T>(this IEnumerable<T> arr, Action<T> action)
    {
        foreach (var t in arr)
            action(t);
    }
    
    public static void ForEach<T>(this HashSet<T> set, Action<T> action)
    {
        foreach (var t in set)
            action(t);
    }
    
    public static int FirstIndexOf<T>(this List<T> array, Func<T, bool> condition)
    {
        for (var i = 0; i < array.Count; i++)
            if (condition(array[i]))
                return i;
        return -1;
    }

    public static Maybe<T> FirstOrMaybe<T>(this IEnumerable<T> items) where T : class
        => items.FirstOrDefault();
    
    public static Maybe<T> FirstOrMaybe<T>(this IEnumerable<T> items, Func<T, bool> condition) where T : class
        => items.FirstOrDefault(condition);
    
    public static Maybe<T> LastOrMaybe<T>(this IEnumerable<T> items, Func<T, bool> condition) where T : class
        => items.LastOrDefault(condition);
    
    public static Maybe<T> LastOrMaybe<T>(this IEnumerable<T> items) where T : class
        => items.LastOrDefault();
}
