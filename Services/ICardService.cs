using RapidPayWebAPI.Controllers.Requests;

namespace RapidPayWebAPI.Services;

public interface ICardService
{
    Task<decimal?> GetCardBalanceAsync(string cardNumber);
    Task CreateNewCardAsync(CardRequest cardRequest);
    Task<decimal> MakePaymentAsync(CardRequest cardRequest, decimal amountToPay);
}