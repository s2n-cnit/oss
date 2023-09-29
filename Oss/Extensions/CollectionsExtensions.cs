
namespace Extensions
{
    public static class CollectionsExtensions
    {
        public static bool IsEmpty<TObject>(this IEnumerable<TObject> collection)
        {
            return collection.Count() == 0;
        }

        public static bool IsEmpty<TObject>(this TObject[] collection)
        {
            return collection.Length == 0;
        }

        public static bool ContainsAny<TObject>(this IEnumerable<TObject> collection, IEnumerable<TObject> values)
        {
            if (values is null || values.IsEmpty())
            {
                return false;
            }

            foreach (var value in values)
            {
                if (collection.Contains(value))
                {
                    return true;
                }
            }

            return false;
        }

        public static bool ContainsAll<TObject>(this IEnumerable<TObject> collection, IEnumerable<TObject> values)
        {
            if (values is null || values.IsEmpty())
            {
                return true;
            }

            foreach (var value in values)
            {
                if (!collection.Contains(value))
                {
                    return false;
                }
            }

            return true;
        }

        public static bool IsIn<TObject>(this TObject value, IEnumerable<TObject> collection)
        {
            return collection.Contains(value);
        }

        public static bool IsNotIn<TObject>(this TObject value, IEnumerable<TObject> collection)
        {
            return !value.IsIn(collection);
        }

        public static void ForEach<TObject>(this IEnumerable<TObject> collection, Action<TObject> action)
        {
            foreach (var item in collection)
            {
                action(item);
            }
        }

        public static IList<TObject> AddFluent<TObject>(this IList<TObject> collection, TObject item)
        {
            collection.Add(item);

            return collection;
        }

        public static IList<TObject> RemoveFluent<TObject>(this IList<TObject> collection, TObject item)
        {
            _ = collection.Remove(item);

            return collection;
        }

        public static IList<TObject> InsertFluent<TObject>(this IList<TObject> collection, int index, TObject item)
        {
            collection.Insert(index, item);

            return collection;
        }

        public static IEnumerable<TObject> ForEachFluent<TObject>(this IEnumerable<TObject> collection, Action<TObject> action)
        {
            collection.ForEach(action);

            return collection;
        }

        public static TResult Map<TObject, TResult>(this TObject obj, Func<TObject, TResult> func)
        {
            return func(obj);
        }

        public static List<TObject> SortFluent<TObject>(this List<TObject> collection)
        {
            collection.Sort();

            return collection;
        }

        public static List<TObject> SortFluent<TObject>(this List<TObject> collection, Comparison<TObject> comparison)
        {
            collection.Sort(comparison);

            return collection;
        }

        public static List<TObject> SortFluent<TObject>(this List<TObject> collection, int index, int count, IComparer<TObject> comparer)
        {
            collection.Sort(index, count, comparer);

            return collection;
        }

        public static List<TObject> SortFluent<TObject>(this List<TObject> collection, IComparer<TObject> comparer)
        {
            collection.Sort(comparer);

            return collection;
        }
    }
}
