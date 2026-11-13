using System.ComponentModel;
using DSharpPlus.Commands;
using DSharpPlus.Entities;

namespace RocBot.Commands;

public class PetPetCommand
{
    [Command("pet")]
    [Description("Pet a user's avatar!")]
    public async ValueTask Execute(
        CommandContext context,
        [Parameter("user"), Description("The user deserving of pets.")] DiscordUser targetUser
    )
    {
        await context.RespondAsync("Test");
    }
}