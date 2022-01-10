using System.ComponentModel.DataAnnotations;
using Roulette.enums;

namespace Roulette.models;

public class Bet
{
    [Key]
    public int ID { get; set; }

    public BetType BetType { get; set; }

    public int? BetNumber { get; set; } = null;

    public int BetAmount { get; set; }

    public int? SpinNumber { get; set; } = null;

    public int RouletteTableID { get; set; }

    public int? PayoutAmount { get; set; } = null;
}