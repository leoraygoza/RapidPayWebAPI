namespace RapidPayWebAPI.Services;

public class UniversalFeeExchange
{
    private static readonly Lazy<UniversalFeeExchange> _instance = new(() => new UniversalFeeExchange());
    private decimal _exchangeRate = (decimal)new Random().NextDouble();
    private Timer _timer;

    private UniversalFeeExchange()
    {
        // I leave this timer to execute each hour due to the requirement, but I recomend
        // to change it to a more reasonable time, like 1 minute, for testing purposes
        _timer = new Timer(UpdateExchangeRate, null, TimeSpan.Zero, TimeSpan.FromHours(1));
    }

    public static UniversalFeeExchange Instance => _instance.Value;

    public decimal ExchangeRate
    {
        get => _exchangeRate;
        private set => _exchangeRate = value;
    }

    private void UpdateExchangeRate(object? state)
    {
        var random = new Random();
        ExchangeRate = (decimal)(random.NextDouble() * 2);
    }
    
}