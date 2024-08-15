namespace RapidPayWebAPI.Services;

public class PaymentFeeService : IPaymentFeeService
{
    private static decimal _previousFee = (decimal)new Random().NextDouble();

    public static decimal PreviousFee
    {
        get => _previousFee;
        private set => _previousFee = value;
    }

    public decimal CalculatePaymentFee()
    {
        var exchangeRate = UniversalFeeExchange.Instance.ExchangeRate;
        var newFee = Math.Round(PreviousFee * exchangeRate, 2);
        PreviousFee = newFee;
        return newFee;
    }
}