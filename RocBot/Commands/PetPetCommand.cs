using System.ComponentModel;
using DSharpPlus;
using DSharpPlus.Commands;
using DSharpPlus.Entities;
using RocBot.Services;

namespace RocBot.Commands;

public class PetPetCommand(
    PetPetGenerator petGenerator,
    HttpClient httpClient,
    LoveActionService loveActionService
    )
{
    [Command("pet")]
    [Description("Pet a user's avatar!")]
    public async ValueTask Execute(
        CommandContext context,
        [Parameter("user"), Description("The user deserving of pets.")] DiscordUser targetUser
    )
    {
        if (targetUser.IsBot)
        {
            DiscordInteractionResponseBuilder clankerResponse = new DiscordInteractionResponseBuilder()
                .WithContent("Clankers do not deserve pets.")
                .AsEphemeral();
            
            await context.RespondAsync(clankerResponse);
            return;
        }
        
        if (targetUser.Id == context.User.Id)
        {
            DiscordInteractionResponseBuilder selfResponse = new DiscordInteractionResponseBuilder()
                .WithContent("Find someone else to pet you!")
                .AsEphemeral();
            
            await context.RespondAsync(selfResponse);
            return;
        }
        
        string avatarUrl = targetUser.GetAvatarUrl(MediaFormat.Png, 512);

        await using var avatarStream = await httpClient.GetStreamAsync(avatarUrl);
        byte[] gif = petGenerator.Generate(avatarStream);
        
        await using var output = new MemoryStream(gif);
        
        DiscordInteractionResponseBuilder response = new DiscordInteractionResponseBuilder()
            .WithContent($"{context.User.Mention} just petted {targetUser.Mention}!")
            .AddFile($"{targetUser.Id}.gif", output);

        await context.RespondAsync(response);

        
        await loveActionService.AddLoveAction(context.User.Id, targetUser.Id);
    }
}