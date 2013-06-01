using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;

namespace PracticeUnityLifetimeManagement
{
  public class Example : IExample
  {
    private bool _disposed = false;
    private readonly Guid _key = Guid.NewGuid();

    public void SayHello()
    {
      if (_disposed)
      {
        throw new ObjectDisposedException("Example",
            string.Format("{0} is already disposed!", _key));
      }

      Console.WriteLine("{0} says hello in thread {1}!", _key,
          Thread.CurrentThread.ManagedThreadId);
    }

    public void Dispose()
    {
      if (!_disposed)
      {
        _disposed = true;
      }
    }
  }
}
