using System.ComponentModel.DataAnnotations;

namespace RapidPayWebAPI.Models;

public class Card
{
    [Key]
    public string CardNumber { get; set; }
    public string CardHolderName { get; set; }
    public DateTime ExpirationDate { get; set; }
    public decimal Balance { get; set; }
    public string CVV { get; set; }
}