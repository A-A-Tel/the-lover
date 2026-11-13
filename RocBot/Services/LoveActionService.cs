using Microsoft.EntityFrameworkCore;
using RocBot.Database;
using RocBot.Database.Entities;

namespace RocBot.Services;

public record LeaderboardEntry(ulong Loving, int Amount);

public record UserLoveEntry(ulong Loving, ulong? Loved, int Amount);

public class LoveActionService(
    AppDbContext database
    )
{
    public async Task AddLoveAction(ulong loving, ulong? loved = null)
    {
        database.LoveActions.Add(new LoveAction
        {
            LovingParty = loving,
            LovedParty = loved
        });

        await database.SaveChangesAsync();
    }

    public async Task<IEnumerable<LeaderboardEntry>> GetLeaderboardAsync()
    {
        var results = await database.LoveActions
            .AsNoTracking()
            .GroupBy(action => action.LovingParty)
            .Select(group => new
            {
                Loving = group.Key,
                Amount = group.Count()
            })
            .OrderByDescending(entry => entry.Amount)
            .Take(10)
            .ToListAsync();

        return results.Select(r => new LeaderboardEntry(r.Loving, r.Amount));
    }

    public async Task<IEnumerable<UserLoveEntry>> GetUserLoveEntriesAsync(ulong loving)
    {
        var results = await database.LoveActions
            .AsNoTracking()
            .Where(action => action.LovingParty == loving)
            .GroupBy(action => action.LovedParty)
            .Select(group => new
            {
                Loved = group.Key,
                Amount = group.Count()
            })
            .OrderByDescending(entry => entry.Amount)
            .ToListAsync();

        return results.Select(r => new UserLoveEntry(loving, r.Loved, r.Amount));
    }
}