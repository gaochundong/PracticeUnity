using System;
using Microsoft.Practices.Unity.InterceptionExtension;

namespace PracticeUnityInterception
{
  internal class LoggerCallHandler : ICallHandler
  {
    public IMethodReturn Invoke(IMethodInvocation input, GetNextHandlerDelegate getNext)
    {
      IMethodReturn result = getNext()(input, getNext);

      Console.WriteLine("LoggerCallHandler:");
      Console.WriteLine("\tParameters:");
      for (int i = 0; i < input.Arguments.Count; i++)
      {
        var parameter = input.Arguments[i];
        Console.WriteLine(string.Format("\t\tParam{0} -> {1}", i, parameter.ToString()));
      }

      Console.WriteLine();

      return result;
    }

    public int Order { get; set; }
  }
}
