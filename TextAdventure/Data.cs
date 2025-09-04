#nullable disable
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

public static class Data
{
    public static List<PlayerCharacter> party = new List<PlayerCharacter>();

    public static PlayerCharacter GetPartyMember(Character character)
    {
        return party.Find(member => member.character == character);
    }
}
