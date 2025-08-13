#nullable disable
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

public class IncrementStatTrigger: IAmTrigger
{
    StatTypes stat;
    int amount;
    Character character;

    public IncrementStatTrigger(StatTypes stat, Character character, int amountToIncrement)
    {
        this.stat = stat;
        amount = amountToIncrement;
        this.character = character;
    }

    public void FireTrigger()
    {
        PlayerCharacter playerCharacter = Data.party.Find(partyMember => partyMember.character == character);
        playerCharacter.stats.IncrementStat(stat, amount);
    }
}
