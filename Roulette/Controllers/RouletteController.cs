using Microsoft.AspNetCore.Mvc;
using Roulette.models;
using Roulette.Services;

namespace Roulette.Controllers;

[ApiController]
[Route("api/[controller]")]
public class RouletteController : ControllerBase
{
    private readonly RouletteService _rouletteService;

    public RouletteController(RouletteService rouletteService) =>
        _rouletteService = rouletteService;

    [HttpGet("showpreviousspins")]
    public async Task<List<Bet>> Get() =>
        await _rouletteService.GetPreviousBetsAsync();

    [HttpPost("placebet")]
    public async Task<IActionResult> Post(Bet newBet)
    {
        await _rouletteService.AddBetAsync(newBet);

        return CreatedAtAction(nameof(Get), new { id = newBet.ID }, newBet);
    }

    [HttpPut("payout/{id:length(24)}")]
    public async Task<IActionResult> Update(string id)
    {
        await _rouletteService.PayoutAsync(int.Parse(id));

        return NoContent();
    }
}