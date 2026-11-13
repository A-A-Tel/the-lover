using Microsoft.EntityFrameworkCore;

namespace RocBot.Database.Entities;

public class LoveAction
{
    public ulong Id { get; set; }
    public ulong LovingParty { get; set; }
    public ulong? LovedParty { get; set; }
}