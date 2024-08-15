using System.Text.RegularExpressions;
using RapidPayWebAPI.Controllers.Requests;
using RapidPayWebAPI.Models;
using RapidPayWebAPI.Providers;

namespace RapidPayWebAPI.Services;

public class CardService : ICardService
{
    private readonly ICardsProvider _cardsProvider;
    private readonly IPaymentFeeService _paymentFeeService;

    public CardService(ICardsProvider cardsProvider, IPaymentFeeService paymentFeeService)
    {
        _cardsProvider = cardsProvider;
        _paymentFeeService = paymentFeeService;
    }
    
    public async Task<decimal?> GetCardBalanceAsync(string cardNumber)
    {
        if (string.IsNullOrWhiteSpace(cardNumber) || !Regex.IsMatch(cardNumber, @"^\d{15}$"))
        {
            throw new BadHttpRequestException("Invalid card number");
        }

        return await _cardsProvider.GetCardBalanceAsync(cardNumber);
    }
    
    public async Task CreateNewCardAsync(CardRequest cardRequest)
    {
        if (string.IsNullOrWhiteSpace(cardRequest.CardNumber) || !Regex.IsMatch(cardRequest.CardNumber, @"^\d{15}$"))
        {
            throw new Exception("Invalid card number");
        }
        
        if (string.IsNullOrWhiteSpace(cardRequest.CardHolderName))
        {
            throw new Exception("Invalid card holder name");
        }
        
        if (cardRequest.ExpirationDate <= DateTime.UtcNow)
        {
            throw new Exception("Invalid expiration date");
        }
        
        if (string.IsNullOrWhiteSpace(cardRequest.CVV) || !Regex.IsMatch(cardRequest.CVV, @"^\d{3}$"))
        {
            throw new Exception("Invalid CVV");
        }

        var newCard = new Card
        {
            CardNumber = cardRequest.CardNumber,
            Balance = cardRequest.Balance,
            CardHolderName = cardRequest.CardHolderName,
            ExpirationDate = cardRequest.ExpirationDate,
            CVV = cardRequest.CVV
        };
        
        await _cardsProvider.CreateNewCardAsync(newCard);
    }
    
    public async Task<decimal> MakePaymentAsync(CardRequest cardRequest, decimal amountToPay)
    {
        if (string.IsNullOrWhiteSpace(cardRequest.CardNumber) || !Regex.IsMatch(cardRequest.CardNumber, @"^\d{15}$"))
        {
            throw new Exception("Invalid card number");
        }
        
        if (string.IsNullOrWhiteSpace(cardRequest.CardHolderName))
        {
            throw new Exception("Invalid card holder name");
        }
        
        // this validation could be better, but for time constraints, I leave it as is
        if (cardRequest.ExpirationDate <= DateTime.UtcNow)
        {
            throw new Exception("Invalid expiration date");
        }
        
        if (string.IsNullOrWhiteSpace(cardRequest.CVV) || !Regex.IsMatch(cardRequest.CVV, @"^\d{3}$"))
        {
            throw new Exception("Invalid CVV");
        }

        return await _cardsProvider.MakePaymentAsync(new Card
        {
            CardNumber = cardRequest.CardNumber,
            CardHolderName = cardRequest.CardHolderName,
            ExpirationDate = cardRequest.ExpirationDate,
            CVV = cardRequest.CVV
        }, amountToPay, _paymentFeeService.CalculatePaymentFee());
    }
}