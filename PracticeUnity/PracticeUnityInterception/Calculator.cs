using System;

namespace PracticeUnityInterception
{
  internal class Calculator : ICalculator
  {
    public int Add(int first, int second)
    {
      Console.WriteLine("Inner type: " + this.GetType().ToString());

      return first + second;
    }

    public int Multiply(int first, int second)
    {
      throw new InvalidOperationException("Dummy exception");
    }
  }
}
