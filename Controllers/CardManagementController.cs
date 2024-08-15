using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using RapidPayWebAPI.Controllers.Requests;
using RapidPayWebAPI.Exceptions;
using RapidPayWebAPI.Services;

namespace RapidPayWebAPI.Controllers;

[Route("card")]
[Authorize]
public class CardManagementController : Controller
{
    private readonly ICardService _cardService;

    public CardManagementController(ICardService cardService)
    {
        _cardService = cardService;
    }
    
    [HttpGet("balance/{cardNumber}")]
    public async Task<IActionResult> GetCardBalance(string cardNumber)
    {
        try
        {
            var balance = await _cardService.GetCardBalanceAsync(cardNumber);
            return Ok(balance);
        }
        catch (BadHttpRequestException e)
        {
            return BadRequest(e.Message);
        }
        catch (CardNotFoundException e)
        {
            return NotFound(e.Message);
        }
    }
    
    [HttpPost("create")]
    public async Task<IActionResult> CreateNewCard([FromBody] CardRequest cardRequest)
    {
        try
        {
            await _cardService.CreateNewCardAsync(cardRequest);
            return Ok();
        }
        catch (Exception e)
        {
            return BadRequest(e.Message);
        }
    }
    
    [HttpPatch("payment/{amountToPay}")]
    public async Task<IActionResult> MakePayment([FromBody] CardRequest cardRequest, [FromRoute] decimal amountToPay)
    {
        if (string.IsNullOrWhiteSpace(cardRequest.CardNumber) ||
            string.IsNullOrWhiteSpace(cardRequest.CardHolderName) || 
            string.IsNullOrWhiteSpace(cardRequest.CVV) ||
            cardRequest.ExpirationDate == default ||
            amountToPay <= 0)
        {
            return BadRequest("Invalid request");
        }
        
        try
        {
            var balance = await _cardService.MakePaymentAsync(cardRequest, amountToPay);
            return Ok(balance);
        }
        catch (Exception e)
        {
            return BadRequest(e.Message);
        }
    }
}