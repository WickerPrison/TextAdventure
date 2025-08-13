#nullable disable
using Spectre.Console;
using Spectre.Console.Rendering;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


public class Breathe: IAmPlayerAction
{
    public string display { get; set; } = "Breathe";
    IAmPlayerAction menuAction;
    IAmPlayerAction breatheAction;
    CharacterStats thisCharacter;

    public Breathe(Action menuAction, CharacterStats thisCharacter)
    {
        this.thisCharacter = thisCharacter;
        this.menuAction = new GenericAction("Cancel", menuAction);
        breatheAction = new GenericAction("Breathe", PerformBreathe);
    }

    public void PerformAction()
    {
        AbilityDescription abilityDescription = new AbilityDescription();
        abilityDescription.name = "Breathe";
        abilityDescription.description = "Gain new Action Dice up to your limit and selectively reroll current dice";
        CombatUtils.DisplayAbilityDescription(abilityDescription);

        IAmPlayerAction confirm = AnsiConsole.Prompt(
            new SelectionPrompt<IAmPlayerAction>()
            .AddChoices([breatheAction, menuAction])
            .UseConverter(option => option.display));

        confirm.PerformAction();
    }

    void PerformBreathe()
    {
        List<int> rerolls = AnsiConsole.Prompt(
            new MultiSelectionPrompt<int>()
            .AddChoices(thisCharacter.currentActionDice)
            .UseConverter(option => $"[[{option.ToString()}]]")
            .NotRequired()
            .InstructionsText(
            "[grey](Press [blue]<space>[/] to toggle a fruit, " +
            "[green]<enter>[/] to accept)[/]"));

        AnsiConsole.WriteLine("You take a moment to catch your breath.");

        thisCharacter.RollActionDice(rerolls);
        AnsiConsole.WriteLine(CombatUtils.ActionDiceDisplay(thisCharacter.currentActionDice));

        thisCharacter.NextTurn();
    }
}
