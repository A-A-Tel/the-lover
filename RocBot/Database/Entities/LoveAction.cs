using Microsoft.EntityFrameworkCore;

namespace RocBot.Database.Entities;

[PrimaryKey(nameof(LovingParty))]
public class LoveAction
{
    public ulong LovingParty { get; set; }
    public ulong? LovedParty { get; set; }
}