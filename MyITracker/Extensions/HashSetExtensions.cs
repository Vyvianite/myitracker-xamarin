using System;
using System.Collections.Generic;
using System.Linq;

namespace MyITracker {
  public static class HashSetExtensions {

    public static bool NullCheck<T>(this HashSet<T> items) {
      return items.Any(x => x is null || (x is string && string.IsNullOrWhiteSpace(x as string)));
    }
  }
}
