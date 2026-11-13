using System.ComponentModel;
using DSharpPlus.Commands;

namespace RocBot.Commands;

public class LoveCommand
{
    [Command("love")]
    [Description("Love another user (anonymously) :D")]
    public async ValueTask Love(CommandContext context)
    {
        await context.RespondAsync("Love you!");
    }
}