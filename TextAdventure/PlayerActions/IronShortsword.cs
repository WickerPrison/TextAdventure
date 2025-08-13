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
    CharacterStats thisCharacter;
    int baseDamage = 4;

    public IronShortsword(Action menuAction, CharacterStats characterStats)
    {
        this.menuAction = new GenericAction("Cancel", menuAction);
        noAD = new GenericAction($"[[-]]: Deal {baseDamage} damage to one enemy", () => CombatUtils.SelectEnemy(IronShortswordNoAD, this.menuAction));
        thisCharacter = characterStats;
    }

    public void PerformAction()
    {
        AbilityDescription abilityDescription = new AbilityDescription();
        abilityDescription.name = "Iron Shortsword";
        abilityDescription.description = $"Deal {baseDamage} + [[x]] damage to one enemy";
        CombatUtils.DisplayAbilityDescription(abilityDescription);

        List<IAmPlayerAction> attacks = new List<IAmPlayerAction>();
        foreach(int actionDie in thisCharacter.currentActionDice)
        {
            attacks.Add(new GenericAction($"Deal {baseDamage} + [[{actionDie}]] damage to one enemy", () => CombatUtils.SelectEnemy(IronShortswordWithAD, menuAction, actionDie)));
        }

        attacks.Add(noAD);
        attacks.Add(menuAction);

        IAmPlayerAction confirm = AnsiConsole.Prompt(
            new SelectionPrompt<IAmPlayerAction>()
            .AddChoices(attacks)
            .UseConverter(option => option.display));

        confirm.PerformAction();
    }

    void IronShortswordNoAD(IAmEnemy target)
    {
        AnsiConsole.WriteLine($"{thisCharacter.name} strikes at {target.name} with his iron shortsword");
        target.TakeDamage(baseDamage, thisCharacter);
        this.thisCharacter.NextTurn();
    }

    void IronShortswordWithAD(IAmEnemy target, int dieValue)
    {
        AnsiConsole.WriteLine($"{thisCharacter.name} strikes at {target.name} with his iron shortsword");
        thisCharacter.SpendActionDie(dieValue);
        target.TakeDamage(baseDamage + dieValue, thisCharacter);
        this.thisCharacter.NextTurn();
    }
}
