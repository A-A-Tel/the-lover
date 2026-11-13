using System.ComponentModel;
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
        await context.RespondAsync("Test");
    }
    
    
}