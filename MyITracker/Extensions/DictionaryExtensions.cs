using System;
using System.Collections.Generic;
using System.Linq;

namespace MyITracker {

  public static class DictionaryExtensions {

    public static string? NullCheck(this Dictionary<string, string> items) {
      KeyValuePair<string, string>[] nullCheck = null;

      nullCheck = (from x in items where string.IsNullOrWhiteSpace(x.Value) select x).ToArray(); /* if (strings.Any(x => string.IsNullOrWhiteSpace(x))) */

      if (nullCheck is object && nullCheck.Length > 0) {
        string? errorMessage = null;

        foreach (KeyValuePair<string, string> i in nullCheck) {
          errorMessage += $"{i.Key}, ";
        }

        return errorMessage;
      }

      return null;
    }
  }
}
