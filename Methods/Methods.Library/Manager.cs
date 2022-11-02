namespace Methods.Library;

public class Manager
{
    private readonly IOperationService _operationService;

    public Manager(IOperationService operationService)
    {
        _operationService = operationService;
    }

    public int Sum(int a, int b) => _operationService.Sum(a, b);

    public int SumAbs(int a, int b) => _operationService.SumAbs(a, b);

    public bool IsOdd(int value) => _operationService.IsOdd(value);
    public double Divide(int a, int b)
    {
        if (b == 0) throw new ArgumentException("The divisor cannot be zero.");
        return a / b;
    }
}
