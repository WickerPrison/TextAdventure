#nullable disable
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

public class IncrementStatTrigger: IAmTrigger
{
    public StatTypes stat;
    public Character character;
    public int amount;

    public IncrementStatTrigger(StatTypes statType, Character characterToIncrement, int amountToIncrement)
    {
        stat = statType;
        character = characterToIncrement;
        amount = amountToIncrement;
    }

    public void FireTrigger()
    {
        CharacterStats stats = Data.party.Find(characterStat => characterStat.character == character);
        stats.IncrementStat(stat, amount);
    }
}
