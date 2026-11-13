using DSharpPlus;
using DSharpPlus.EventArgs;
using DSharpPlus.Extensions;

namespace RocBot.Handlers;

public static class InitializationExtensions
{
    public static void ConfigureDiscordHandlers(this IServiceCollection services)
    {
        // services.ConfigureEventHandlers(b => b
        //     .HandleMessageCreated(HandleMessageCreated)
        //     .HandleMessageDeleted(HandleMessageDeleted)
        // );
    }

    // private static async Task HandleMessageCreated(DiscordClient client, MessageCreatedEventArgs args)
    // {
    // }
    //
    // private static async Task HandleMessageDeleted(DiscordClient client, MessageDeletedEventArgs args)
    // {
    // }
}