
namespace PracticeUnityInterception
{
  public interface ICalculator
  {
    [Logger]
    int Add(int first, int second);

    [ExceptionLogger]
    int Multiply(int first, int second);
  }
}
