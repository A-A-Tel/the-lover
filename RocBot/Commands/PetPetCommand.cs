using System.ComponentModel;
using DSharpPlus;
using DSharpPlus.Commands;
using DSharpPlus.Entities;
using RocBot.Services;

namespace RocBot.Commands;

public class PetPetCommand(
    PetPetGenerator petGenerator,
    HttpClient httpClient
    )
{
    [Command("pet")]
    [Description("Pet a user's avatar!")]
    public async ValueTask Execute(
        CommandContext context,
        [Parameter("user"), Description("The user deserving of pets.")] DiscordUser targetUser
    )
    {
        string avatarUrl = targetUser.GetAvatarUrl(MediaFormat.Png, 512);

        await using var avatarStream = await httpClient.GetStreamAsync(avatarUrl);
        byte[] gif = petGenerator.Generate(avatarStream);
        
        await using var output = new MemoryStream(gif);
        
        DiscordInteractionResponseBuilder response = new DiscordInteractionResponseBuilder()
            .WithContent($"{context.User.Mention} just petted {targetUser.Mention}!")
            .AddFile($"{targetUser.Id}.gif", output);

        await context.RespondAsync(response);
    }
}