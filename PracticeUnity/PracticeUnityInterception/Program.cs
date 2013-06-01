using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Practices.Unity;
using Microsoft.Practices.Unity.InterceptionExtension;

namespace PracticeUnityInterception
{
  class Program
  {
    public static void Main(string[] args)
    {
      // no interception
      IUnityContainer container = new UnityContainer();
      container.RegisterType<ICalculator, Calculator>();

      ResolveFromContainer(container, "No interception");

      Console.WriteLine("==========================================");

      // has interception
      container = new UnityContainer();
      container.AddNewExtension<Interception>();
      container
        .RegisterType<ICalculator, Calculator>()
        .Configure<Interception>()
        .SetInterceptorFor<ICalculator>(new InterfaceInterceptor());

      ResolveFromContainer(container, "Has interface interception");

      Console.ReadKey();
    }

    private static void ResolveFromContainer(IUnityContainer container, string message)
    {
      Console.WriteLine("==== " + message + " ====");

      ICalculator calc = container.Resolve<ICalculator>();

      Console.WriteLine("Outer type: " + calc.GetType().ToString());
      Console.WriteLine("Result: " + calc.Add(1, 2));
      Console.WriteLine();

      try
      {
        calc.Multiply(0, 0);
      }
      catch (Exception) { }

      Console.WriteLine();
    }
  }
}
