using System;
using System.Collections.Generic;

namespace MyITracker {

  public static class EnumerableExtensions {

    public static IEnumerable<T> Safe<T>(this IEnumerable<T> source) {
      if (source is null) {
        yield break;
      }

      foreach (T item in source) {
        yield return item;
      }
    }
  }
}
