using System;
using Microsoft.Practices.Unity.InterceptionExtension;

namespace PracticeUnityInterception
{
  internal class ExceptionLoggerCallHandler : ICallHandler
  {
    public IMethodReturn Invoke(
      IMethodInvocation input, GetNextHandlerDelegate getNext)
    {
      IMethodReturn result = getNext()(input, getNext);
      if (result.Exception != null)
      {
        Console.WriteLine("ExceptionLoggerCallHandler:");
        Console.WriteLine("\tParameters:");
        for (int i = 0; i < input.Arguments.Count; i++)
        {
          var parameter = input.Arguments[i];
          Console.WriteLine(
            string.Format("\t\tParam{0} -> {1}", i, parameter.ToString()));
        }
        Console.WriteLine();
        Console.WriteLine("Exception occured: ");
        Console.WriteLine(
          string.Format("\tException -> {0}", result.Exception.Message));

        Console.WriteLine();
        Console.WriteLine("StackTrace:");
        Console.WriteLine(Environment.StackTrace);
      }

      return result;
    }

    public int Order { get; set; }
  }
}
