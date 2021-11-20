using System;
using System.Threading.Tasks;

namespace MyITracker {

  public static class TaskExtensions {

    // NOTE: Async void is intentional here. This provides a way
    // to call an async method from the constructor while
    // communicating intent to fire and forget, and allow
    // handling of exceptions
    public static async void SafeFireAndForget(this Task task, bool returnToCallingContext, Action<Exception>? onException = null) {
      try {
        if (task is object) {
          await task.ConfigureAwait(returnToCallingContext);
        }
        else {
          throw new ArgumentNullException(nameof(task), "This is an extension method. How is this even null?");
        }
      }

      // if the provided action is not null, catch and
      // pass the thrown exception
      catch (Exception ex) when (onException is object) {
        onException(ex);
      }
    }
  }
}
