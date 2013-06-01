using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace PracticeUnityLifetimeManagement
{
  class Program
  {
    static void Main(string[] args)
    {
      TestHelper.TestExternallyControlledLifetimeManager();

      Console.ReadKey();
    }
  }
}
