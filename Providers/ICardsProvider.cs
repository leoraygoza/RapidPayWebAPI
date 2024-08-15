using RapidPayWebAPI.Models;

namespace RapidPayWebAPI.Providers;

public interface ICardsProvider
{
    Task<decimal> GetCardBalanceAsync(string cardNumber);
    Task CreateNewCardAsync(Card card);
    Task<decimal> MakePaymentAsync(Card card, decimal amountToPay, decimal paymentFee);
}