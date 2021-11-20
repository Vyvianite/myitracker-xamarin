using System;
using Microsoft.FSharp.Core;

namespace MyITracker {

  public static class FOpt {
    public static FSharpOption<T> New<T>(T value) {
      return new FSharpOption<T>(value);
    }

    public static bool IsSome<T>(this FSharpOption<T> opt) {
      return FSharpOption<T>.get_IsSome(opt);
    }

    public static bool IsNone<T>(this FSharpOption<T> opt) {
      return FSharpOption<T>.get_IsNone(opt);
    }
  }
}
