using System.ComponentModel;
using DSharpPlus.Commands;

namespace RocBot.Commands;

public class LoveCommand
{
    private Random _random = new Random();

    private static string[] _openMessages =
    [
        "Ohh it seems that %is is in a good mood, they seem to love %us :o",
        "%is just publicly declared their love for %us 💕 bold move!",
        "Breaking news: %is has feelings for %us ❤️",
        "%us has been chosen… by %is :p",
        "%is is sending all the love to %us right now 💌",
        "A wild affection appeared! %is -> %us 💖",
        "%is thinks %us deserves a hug… or maybe several :D",
        "%is has pressed the 'love %us' button, waw :o",
        "%us, you’ve just been appreciated by %is :)",
        "%is is legally required to love %us now! >:)",
        "%is rolled a 20… %us receives maximum love!",
        "%us has unlocked: 💝 affection from %is",
        "%is saw %us and immediately smiled 😊",
        "%is thinks %us is kinda amazing… just saying ✨",
        "%us has been selected for today’s dose of love by %is 💊❤️",
        "%is sends a wave of love crashing into %us 🌊💖",
        "%is cannot hide it anymore… they love %us 😳"
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
    public async ValueTask Love(
        CommandContext context,
        [Parameter("i_am_shy"), Description("For if you wish to love someone in secret :)")] bool isShy)
    {
        await context.RespondAsync("Love you!");
    }
}