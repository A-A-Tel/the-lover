using DotNetEnv;
using DSharpPlus;
using DSharpPlus.Commands;
using DSharpPlus.Commands.Processors.SlashCommands;
using DSharpPlus.Extensions;
using RocBot;
using RocBot.Commands;
using RocBot.Database;
using RocBot.Handlers;
using RocBot.Services;

Env.Load();
HostApplicationBuilder builder = Host.CreateApplicationBuilder(args);


#region Bot config

builder.Services.AddDiscordClient(Env.GetString("BOT_TOKEN"), DiscordIntents.All);
builder.Services.ConfigureDiscordHandlers();

builder.Services.AddCommandsExtension((sp, cx) =>
{
    cx.AddCommands([
        typeof(LoveCommand),
        typeof(PetPetCommand),
        typeof(ScoreCommand)
    ]);

    cx.AddProcessor(sp.GetRequiredService<SlashCommandProcessor>());
});
builder.Services.AddSingleton<SlashCommandProcessor>();
builder.Services.AddHostedService<Worker>();

#endregion

builder.Services.AddSingleton(_ => PetPetGenerator.FromDirectory("Assets/PetPet"));
builder.Services.AddSingleton<HttpClient>();
builder.Services.AddDbContext<AppDbContext>();
builder.Services.AddScoped<LoveActionService>();

IHost host = builder.Build();

using (IServiceScope scope = host.Services.CreateScope())
{
    scope.ServiceProvider.GetRequiredService<AppDbContext>().Database.EnsureCreated();
}

await host.RunAsync();