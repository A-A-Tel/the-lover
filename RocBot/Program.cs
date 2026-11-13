using DotNetEnv;
using DSharpPlus;
using DSharpPlus.Commands;
using DSharpPlus.Commands.Processors.SlashCommands;
using DSharpPlus.Extensions;
using RocBot;
using RocBot.Commands;
using RocBot.Handlers;

Env.Load();
HostApplicationBuilder builder = Host.CreateApplicationBuilder(args);

builder.Services.AddDiscordClient(Env.GetString("BOT_TOKEN"), DiscordIntents.All);
builder.Services.ConfigureDiscordHandlers();

builder.Services.AddCommandsExtension((sp, cx) =>
{
    cx.AddCommands([
        typeof(LoveCommand)
        // typeof(PetPetCommand),
        // typeof(ScoreCommand)
    ]);

    cx.AddProcessor(sp.GetRequiredService<SlashCommandProcessor>());
});
builder.Services.AddSingleton<SlashCommandProcessor>();

builder.Services.AddHostedService<Worker>();

IHost host = builder.Build();
await host.RunAsync();