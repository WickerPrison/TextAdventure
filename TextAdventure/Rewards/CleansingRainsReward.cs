#nullable disable
using Spectre.Console.Rendering;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

public class CleansingRainsReward : IAmReward
{
    public string optionDisplay { get; set; } = "Cleansing Rains";
    Rhun rhun;

    public CleansingRainsReward()
    {
        this.rhun = Data.GetPartyMember(Character.RHUN) as Rhun;
    }

    public void ShowReward()
    {
        AbilityDescription abilityDescription = new AbilityDescription();
        abilityDescription.name = "Cleansing Rains";
        abilityDescription.description = $"Restore [green]4 + [[x]][/] hp to one ally and cleanse them of all debuffs. Incur [red]2 Wrath[/].";

        CombatUtils.DisplayAbilityDescription(abilityDescription);
    }

    public void GainReward()
    {
        rhun.rituals.Add(RhunAbilityDict.abilities[RhunAction.CLEANSING_RAINS]);
    }
}
