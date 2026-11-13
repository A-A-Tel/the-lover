namespace RocBot.Database.Entities;

public class LoveAction
{
    public ulong LovingParty { get; set; }
    public ulong? LovedParty { get; set; }
}