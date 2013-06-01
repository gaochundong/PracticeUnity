using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace PracticeUnityLifetimeManagement
{
  public interface IExample : IDisposable
  {
    void SayHello();
  }
}
