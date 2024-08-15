using Microsoft.EntityFrameworkCore;
using RapidPayWebAPI.Data;
using RapidPayWebAPI.Exceptions;
using RapidPayWebAPI.Models;

namespace RapidPayWebAPI.Providers;

public class CardsProvider : ICardsProvider
{
    private readonly RapidPayDbContext _db;

    public CardsProvider(RapidPayDbContext db)
    {
        _db = db;
    }

    public async Task<decimal> GetCardBalanceAsync(string cardNumber)
    {
        var card = await _db.Cards.SingleOrDefaultAsync(c => c.CardNumber == cardNumber);
        if (card == null)
        {
            throw new CardNotFoundException("Card number not found");
        }
        
        return card.Balance;
    }

    public async Task CreateNewCardAsync(Card card)
    {
        if (_db.Cards.Any(c => c.CardNumber == card.CardNumber))
        {
            throw new Exception("Card already exists");
        }
        
        await _db.Cards.AddAsync(card);
        await _db.SaveChangesAsync();
    }

    public async Task<decimal> MakePaymentAsync(Card card, decimal amountToPay, decimal paymentFee)
    {
        var existingCard = await _db.Cards.SingleOrDefaultAsync(c =>
            c.CardNumber == card.CardNumber && 
            c.CardHolderName == card.CardHolderName &&
            c.ExpirationDate.Year == card.ExpirationDate.Year &&
            c.ExpirationDate.Month == card.ExpirationDate.Month &&
            c.CVV == card.CVV);

        if (existingCard == null)
        {
            throw new Exception("Card not found");
        }
        
        if (existingCard.Balance < amountToPay + paymentFee)
        {
            throw new Exception($"Insufficient funds, consider the ${paymentFee} payment fee");
        }
        
        existingCard.Balance -= amountToPay + paymentFee;
        await _db.SaveChangesAsync();

        return existingCard.Balance;
    }
}