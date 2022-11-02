namespace Methods.Library;

public class OperationService : IOperationService
{
    public int Sum(int a, int b) => a + b;

    public int SumAbs(int a, int b) => Math.Abs(a) + Math.Abs(b);

    public bool IsOdd(int value) => value % 2 == 1;

    public double Divide(int a, int b) => a / b;
}