using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace Code_Base.Utilities
{
  public static class EnumerableUtilities
  {
    public static bool IsNullOrEmpty<T>(this IEnumerable<T> enumerable)
    {
      if (enumerable == null)
        return true;

      if (enumerable is ICollection<T> collection)
        return collection.Count == 0;

      return !enumerable.Any();
    }
    
    public static T PickRandom<T>(this IEnumerable<T> collection)
    {
      T[] enumerable = collection as T[] ?? collection.ToArray();
      return enumerable[Random.Range(0, enumerable.Length)];
    }
  }
}