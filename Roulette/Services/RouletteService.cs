using Roulette.models;
using Microsoft.Extensions.Options;
using Roulette.Db;
using Microsoft.EntityFrameworkCore;
using Roulette.enums;

namespace Roulette.Services;

public class RouletteService
{
    public RouletteService()
    {
    }

    public async Task<int> AddBetAsync(Bet newBet)
    {
        int betIndex;
        using (AuthDbContext context = new AuthDbContext())
        {
            context.Bets.Add(newBet);
            await context.SaveChangesAsync();
            List<Bet> bets = await context.Bets.ToListAsync();
            betIndex = bets.IndexOf(newBet);
        }
        return betIndex;
    }

    public async Task SpinTable(RouletteTable table)
    {
        Random random = new Random();
        int randomNumber = random.Next(0, 36);

        using (AuthDbContext context = new AuthDbContext())
        {
            var result = await context.Bets.Where(bet => bet.RouletteTableID == table.ID).ToListAsync();
            foreach (Bet aBet in result)
            {
                aBet.SpinNumber = randomNumber;
            }

            await context.SaveChangesAsync();

            var tableRes = await context.RouletteTables.SingleOrDefaultAsync(t => t.ID == table.ID);

            if (tableRes != null)
            {
                tableRes.SpinIteration = tableRes.SpinIteration + 1;
            }

            await context.SaveChangesAsync();
        }
    }

    public async Task PayoutAsync(int tableId)
    {
        using (AuthDbContext context = new AuthDbContext())
        {
            var result = await context.Bets.Where(bet => bet.RouletteTableID == tableId).ToListAsync();
            int payoutMultiplier = 0;
            foreach (Bet aBet in result)
            {
                switch (aBet.BetType)
                {
                    case BetType.Single:
                        if (aBet.BetNumber == aBet.SpinNumber)
                        {
                            payoutMultiplier = 35;
                            aBet.PayoutAmount = aBet.BetAmount * payoutMultiplier;
                        }
                        break;
                    case BetType.First_Column:
                        if (aBet.SpinNumber == 1 || aBet.SpinNumber == 4 || aBet.SpinNumber == 7 || aBet.SpinNumber == 10 || aBet.SpinNumber == 13 || aBet.SpinNumber == 16 || aBet.SpinNumber == 19 || aBet.SpinNumber == 22 || aBet.SpinNumber == 25 || aBet.SpinNumber == 28 || aBet.SpinNumber == 31 || aBet.SpinNumber == 34)
                        {
                            payoutMultiplier = 2;
                            aBet.PayoutAmount = aBet.BetAmount * payoutMultiplier;
                        }
                        break;
                    case BetType.Second_Column:
                        if (aBet.SpinNumber == 2 || aBet.SpinNumber == 5 || aBet.SpinNumber == 8 || aBet.SpinNumber == 11 || aBet.SpinNumber == 14 || aBet.SpinNumber == 17 || aBet.SpinNumber == 20 || aBet.SpinNumber == 23 || aBet.SpinNumber == 26 || aBet.SpinNumber == 29 || aBet.SpinNumber == 32 || aBet.SpinNumber == 35)
                        {
                            payoutMultiplier = 2;
                            aBet.PayoutAmount = aBet.BetAmount * payoutMultiplier;
                        }
                        break;
                    case BetType.Third_Column:
                        if (aBet.SpinNumber == 3 || aBet.SpinNumber == 6 || aBet.SpinNumber == 9 || aBet.SpinNumber == 12 || aBet.SpinNumber == 15 || aBet.SpinNumber == 18 || aBet.SpinNumber == 21 || aBet.SpinNumber == 24 || aBet.SpinNumber == 27 || aBet.SpinNumber == 30 || aBet.SpinNumber == 33 || aBet.SpinNumber == 36)
                        {
                            payoutMultiplier = 2;
                            aBet.PayoutAmount = aBet.BetAmount * payoutMultiplier;
                        }
                        break;
                    case BetType.First_Dozen:
                        if (1 <= aBet.SpinNumber && aBet.SpinNumber <= 12)
                        {
                            payoutMultiplier = 2;
                            aBet.PayoutAmount = aBet.BetAmount * payoutMultiplier;
                        }
                        break;
                    case BetType.Second_Dozen:
                        if (13 <= aBet.SpinNumber && aBet.SpinNumber <= 24)
                        {
                            payoutMultiplier = 2;
                            aBet.PayoutAmount = aBet.BetAmount * payoutMultiplier;
                        }
                        break;
                    case BetType.Third_Dozen:
                        if (25 <= aBet.SpinNumber && aBet.SpinNumber <= 36)
                        {
                            payoutMultiplier = 2;
                            aBet.PayoutAmount = aBet.BetAmount * payoutMultiplier;
                        }
                        break;
                    case BetType.First_Eighteen:
                        if (1 <= aBet.SpinNumber && aBet.SpinNumber <= 18)
                        {
                            payoutMultiplier = 1;
                            aBet.PayoutAmount = aBet.BetAmount * payoutMultiplier;
                        }
                        break;
                    case BetType.Second_Eighteen:
                        if (19 <= aBet.SpinNumber && aBet.SpinNumber <= 36)
                        {
                            payoutMultiplier = 1;
                            aBet.PayoutAmount = aBet.BetAmount * payoutMultiplier;
                        }
                        break;
                    case BetType.Odd:
                        if ((aBet.SpinNumber - 1) % 2 == 0 || aBet.SpinNumber == 1)
                        {
                            payoutMultiplier = 1;
                            aBet.PayoutAmount = aBet.BetAmount * payoutMultiplier;
                        }
                        break;
                    case BetType.Even:
                        if (aBet.SpinNumber % 2 == 0)
                        {
                            payoutMultiplier = 1;
                            aBet.PayoutAmount = aBet.BetAmount * payoutMultiplier;
                        }
                        break;
                    case BetType.Red:
                        if (aBet.SpinNumber % 2 == 0)
                        {
                            payoutMultiplier = 1;
                            aBet.PayoutAmount = aBet.BetAmount * payoutMultiplier;
                        }
                        break;
                    case BetType.Black:
                        if ((aBet.SpinNumber - 1) % 2 == 0 || aBet.SpinNumber == 1)
                        {
                            payoutMultiplier = 1;
                            aBet.PayoutAmount = aBet.BetAmount * payoutMultiplier;
                        }
                        break;
                }
            }

            await context.SaveChangesAsync();
        }
    }

    public async Task<List<Bet>> GetPreviousBetsAsync()
    {
        List<Bet> bets;
        using (AuthDbContext context = new AuthDbContext())
        {
            bets = await context.Bets.ToListAsync();
        }

        return bets;
    }
}