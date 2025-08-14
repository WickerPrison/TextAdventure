#nullable disable
using Spectre.Console;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

class IronShortsword: IAmPlayerAction
{
    public string display { get; set; }
    IAmPlayerAction menuAction;
    IAmPlayerAction noAD;
    PlayerCharacter thisCharacter;
    int baseDamage = 4;

    public IronShortsword(Action menuAction, PlayerCharacter characterStats)
    {
        this.menuAction = new GenericAction("Cancel", menuAction);
        noAD = new GenericAction($"[[-]]: Deal [red]{baseDamage}[/] damage to one enemy", () => CombatUtils.SelectEnemy(IronShortswordNoAD, this.menuAction));
        thisCharacter = characterStats;
    }

    public void PerformAction()
    {
        AbilityDescription abilityDescription = new AbilityDescription();
        abilityDescription.name = "Iron Shortsword";
        abilityDescription.description = $"Deal [red]{baseDamage} + [[x]][/] damage to one enemy";
        CombatUtils.DisplayAbilityDescription(abilityDescription);

        List<IAmPlayerAction> attacks = new List<IAmPlayerAction>();
        foreach(int actionDie in thisCharacter.stats.currentActionDice)
        {
            attacks.Add(new GenericAction($"Deal [red]{baseDamage} + [[{actionDie}]][/] damage to one enemy", () => CombatUtils.SelectEnemy(IronShortswordWithAD, menuAction, actionDie)));
        }

        attacks.Add(noAD);
        attacks.Add(menuAction);

        IAmPlayerAction confirm = AnsiConsole.Prompt(
            new SelectionPrompt<IAmPlayerAction>()
            .AddChoices(attacks)
            .UseConverter(option => option.display));

        confirm.PerformAction();
    }

    void IronShortswordNoAD(Enemy target)
    {
        AnsiConsole.WriteLine("");
        AnsiConsole.MarkupLine($"{thisCharacter.name} strikes at {target.name} with his iron shortsword");
        target.TakeDamage(baseDamage, thisCharacter);
        Console.ReadKey();
        this.thisCharacter.NextTurn();
    }

    void IronShortswordWithAD(Enemy target, int dieValue)
    {
        AnsiConsole.WriteLine("");
        AnsiConsole.MarkupLine($"{thisCharacter.name} strikes at {target.name} with his iron shortsword");
        thisCharacter.stats.SpendActionDie(dieValue);
        target.TakeDamage(baseDamage + dieValue, thisCharacter);
        Console.ReadKey();
        this.thisCharacter.NextTurn();
    }
}
