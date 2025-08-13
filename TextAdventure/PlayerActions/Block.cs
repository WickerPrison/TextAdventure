using Spectre.Console;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

class Block: IAmPlayerAction
{
    public string display { get; set; } = "Block";
    int damage;
    CharacterStats thisCharacter;
    IAmPlayerAction noAD;
    IAmPlayerAction cancel;

    public Block(CharacterStats thisCharacter, Action cancel, int damage)
    {
        this.thisCharacter = thisCharacter;
        this.damage = damage;
        int reduction = CalculateBLockAmount(0);
        noAD = new GenericAction($"[[-]]: {reduction} damage reduction", () => BlockDamage(reduction));
        this.cancel = new GenericAction("Cancel", cancel);
    }

    public void PerformAction()
    {
        List<IAmPlayerAction> options = new List<IAmPlayerAction>();
        foreach(int actionDie in thisCharacter.currentActionDice)
        {
            int reduction = CalculateBLockAmount(actionDie);
            options.Add(new GenericAction($"[[{actionDie}]]: {reduction} damage reduction", () => BlockDamage(reduction, actionDie)));
        }

        options.Add(noAD);
        options.Add(cancel);

        IAmPlayerAction confirm = AnsiConsole.Prompt(
            new SelectionPrompt<IAmPlayerAction>()
            .AddChoices(options)
            .UseConverter(option => option.display));

        confirm.PerformAction();
    }

    int CalculateBLockAmount(int dieValue)
    {
        return dieValue;
    }

    void BlockDamage(int reduction, int dieValue = 0)
    {
        if(dieValue != 0)
        {
            thisCharacter.SpendActionDie(dieValue);
        }

        int damageTaken = Math.Max(damage - reduction, 0);
        thisCharacter.LoseHealth(damageTaken);
    }
}
