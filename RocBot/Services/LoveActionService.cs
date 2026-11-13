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
    public void AddLoveAction(ulong loving, ulong? loved = null)
    {
        database.LoveActions.Add(new LoveAction
        {
            LovingParty = loving,
            LovedParty = loved
        });
    }

    public IEnumerable<LeaderboardEntry> GetLeaderboard()
    {
        return database.LoveActions
            .AsNoTracking()
            .GroupBy(action => action.LovingParty)
            .Select(group => new LeaderboardEntry(
                group.Key,
                group.Count()
            ))
            .OrderByDescending(entry => entry.Amount)
            .Take(10);
    }
    
    public IEnumerable<UserLoveEntry> GetUserLoveEntries(ulong loving)
    {
        return database.LoveActions
            .AsNoTracking()
            .Where(action => action.LovingParty == loving)
            .GroupBy(action => action.LovedParty)
            .Select(group => new UserLoveEntry(
                loving,
                group.Key,
                group.Count()
            ))
            .OrderByDescending(entry => entry.Amount);
    }
}