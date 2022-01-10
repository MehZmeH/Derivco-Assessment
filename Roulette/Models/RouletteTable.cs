using System.ComponentModel.DataAnnotations;

namespace Roulette.models;

public class RouletteTable
{
    [Key]
    public int ID { get; set; }

    public int SpinIteration { get; set; } = 0;
}