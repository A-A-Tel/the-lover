using System.ComponentModel;
using System.Text;
using DSharpPlus.Commands;
using DSharpPlus.Entities;
using RocBot.Services;

namespace RocBot.Commands;

public class ScoreCommand(
    LoveActionService loveActionService
    )
{
    [Command("score")]
    [Description("The global love scores")]
    public async ValueTask Execute(
        CommandContext context,
        [Parameter("loving_user"), Description("The user that has loved.")] DiscordUser? user = null
        )
    {
        string message = user == null ? await GetFullScoreboardMessage() : await GetUserScoreMessage(user.Id);
        
        await context.RespondAsync(message);
    }

    private async Task<string> GetFullScoreboardMessage()
    {
        IEnumerable<LeaderboardEntry> entries = await loveActionService.GetLeaderboardAsync();

        StringBuilder builder = new("Top lovers of all time: \n\n");

        int i = 0;
        foreach (LeaderboardEntry entry in entries)
        {
            i++;
            builder.Append($"{i}. <@{entry.Loving}> ({entry.Amount})");
        }

        return builder.ToString();
    }

    private async Task<string> GetUserScoreMessage(ulong userId)
    {
        IEnumerable<UserLoveEntry> entries = await loveActionService.GetUserLoveEntriesAsync(userId);

        StringBuilder builder = new($"Love stats for <@{userId}>:\n\n");

        int i = 0;
        foreach (UserLoveEntry entry in entries)
        {
            i++;
            string target = entry.Loved.HasValue ? $"<@{entry.Loved.Value}>" : "(Anonymous loving)";
            builder.AppendLine($"{i}. {target} ({entry.Amount})");
        }

        return i == 0 
            ? $"<@{userId}> has not spread any love yet!" 
            : builder.ToString();
    }

}