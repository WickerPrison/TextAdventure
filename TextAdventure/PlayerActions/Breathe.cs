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
    PlayerCharacter thisCharacter;

    public Breathe(Action menuAction, PlayerCharacter thisCharacter)
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
        List<int> rerolls;
        if(thisCharacter.stats.currentActionDice.Count > 0)
        {
            AnsiConsole.WriteLine("Which dice do you want to reroll?");
            rerolls = AnsiConsole.Prompt(
                new MultiSelectionPrompt<int>()
                .AddChoices(thisCharacter.stats.currentActionDice)
                .UseConverter(option => $"[[{option.ToString()}]]")
                .NotRequired()
                .InstructionsText(
                "[grey](Press [blue]<space>[/] to select a die to reroll, " +
                "[green]<enter>[/] to accept)[/]"));
        }
        else
        {
            rerolls = new List<int>();
        }

        AnsiConsole.WriteLine($"{thisCharacter.name} takes a moment to catch his breath.");

        thisCharacter.stats.RollActionDice(rerolls);
        AnsiConsole.MarkupLine($"[green]{CombatUtils.ActionDiceDisplay(thisCharacter)}[/]");

        Console.ReadKey();
        thisCharacter.NextTurn();
    }
}
