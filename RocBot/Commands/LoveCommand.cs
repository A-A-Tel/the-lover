using System.ComponentModel;
using System.Runtime.CompilerServices;
using DSharpPlus.Commands;
using DSharpPlus.Entities;

namespace RocBot.Commands;

public class LoveCommand
{
    private Random _random = new Random();

    private static string[] _openMessages =
    [
        "Ohh it seems that %is is in a good mood, they seem to love %ts :o",
        "%is just publicly declared their love for %ts 💕 bold move!",
        "Breaking news: %is has feelings for %ts ❤️",
        "%ts has been chosen… by %is :p",
        "%is is sending all the love to %ts right now 💌",
        "A wild affection appeared! %is -> %ts 💖",
        "%is thinks %ts deserves a hug… or maybe several :D",
        "%is has pressed the 'love %ts' button, waw :o",
        "%ts, you’ve just been appreciated by %is :)",
        "%is is legally required to love %ts now! >:)",
        "%is rolled a 20… %ts receives maximum love!",
        "%ts has unlocked: 💝 affection from %is",
        "%is saw %ts and immediately smiled 😊",
        "%is thinks %ts is kinda amazing… just saying ✨",
        "%ts has been selected for today’s dose of love by %is 💊❤️",
        "%is sends a wave of love crashing into %ts 🌊💖",
        "%is cannot hide it anymore… they love %ts 😳"
    ];

    private static string[] _shyMessages =
    [
        "Heyyy %s, someone is feeling a little shy, but they still wanted to let you know that you are loved :D",
        "%s received an anonymous message 💌: You are loved!",
        "You are indeed appreciated, %s!",
        "Hello there lovely %s, you're very sweet!",
        "Psst… %s… someone out there really likes you 👀",
        "%s, an anonymous admirer thinks you're amazing :D",
        "A secret message for %s: you mean a lot to someone 💕",
        "%s, you didn’t hear it from me… but someone cares about you a lot 🤫",
        "Incoming anonymous affection for %s 💘",
        "%s, someone just smiled thinking about you 😊",
        "A shy soul wanted you to know this, %s: you're appreciated!",
        "%s, you’ve been quietly loved from afar 💞",
        "An unknown sender wants %s to have a wonderful day ☀️",
        "%s, someone believes in you more than you think 💫",
        "This is your reminder, %s: you are valued :)",
        "%s, someone out there is rooting for you 🎉",
        "%s, someone thinks you're pretty great, just saying :p",
        "Secret transmission received: %s is loved 💖",
        "%s, you’ve been the reason for someone’s smile today :D"
    ];
    
    [Command("love")]
    [Description("Love another user (anonymously) :D")]
    public async ValueTask Execute(
        CommandContext context,
        [Parameter("user"), Description("The one receiving the love")] DiscordUser targetUser,
        [Parameter("i_am_shy"), Description("For if you wish to love someone in secret :)")] bool isShy)
    {
        if (targetUser.IsBot)
        {
            DiscordInteractionResponseBuilder clankerResponse = new DiscordInteractionResponseBuilder()
                .WithContent("Clankers do not deserve love.")
                .AsEphemeral();

            await context.RespondAsync(clankerResponse);
            return;
        }
        
        if (targetUser.Id == context.User.Id)
        {
            DiscordInteractionResponseBuilder selfLoveResponse = new DiscordInteractionResponseBuilder()
                .WithContent("Love yourself first ❤️")
                .AsEphemeral();

            await context.RespondAsync(selfLoveResponse);
            return;
        }
        
        DiscordInteractionResponseBuilder ephemeral = new DiscordInteractionResponseBuilder()
            .WithContent("Your message has been sent!")
            .AsEphemeral();
        
        string[] messages = isShy ? _shyMessages : _openMessages;
        string message = messages[Random.Shared.Next(messages.Length)];

        string response = isShy
            ? FormatShyMessageMentions(message, targetUser)
            : FormatOpenMessageMentions(message, targetUser, context.User);
        
        await context.Channel.SendMessageAsync(response);
        await context.RespondAsync(ephemeral);
    }

    private static string FormatShyMessageMentions(string message, DiscordUser targetUser)
    {
        return message
            .Replace("%s", targetUser.Mention);
    }

    private static string FormatOpenMessageMentions(string message, DiscordUser targetUser, DiscordUser initiatingUser)
    {
        return message
            .Replace("%ts", targetUser.Mention)
            .Replace("%is", initiatingUser.Mention);
    }
}