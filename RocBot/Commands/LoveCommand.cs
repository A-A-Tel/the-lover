using System.ComponentModel;
using DSharpPlus.Commands;

namespace RocBot.Commands;

public class LoveCommand
{
    [Command("love")]
    [Description("Love another user (anonymously) :D")]
    public async ValueTask Love(
        CommandContext context,
        [Parameter("i_am_shy"), Description("For if you wish to love someone in secret :)")] bool isShy)
    {
        await context.RespondAsync("Love you!");
    }
}