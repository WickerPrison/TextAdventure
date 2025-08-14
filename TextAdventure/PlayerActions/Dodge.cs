#nullable disable
using Spectre.Console;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;

public class Dodge: IAmPlayerAction
{
    public string display { get; set; } = "Dodge";
    PlayerCharacter thisCharacter;
    IAmPlayerAction noAD;
    IAmPlayerAction cancel;
    int damage;

    public Dodge(PlayerCharacter thisCharacter, Action cancel, int damage)
    {
        this.damage = damage;
        this.thisCharacter = thisCharacter;
        int percent = CalculateDodgeChance(0);
        noAD = new GenericAction($"[[-]]: {percent}% dodge chance", () => DodgeChance(percent, damage));
        this.cancel = new OpenMenu("Cancel", cancel);
    }

    public void PerformAction()
    {
        List<IAmPlayerAction> options = new List<IAmPlayerAction>();
        foreach (int actionDie in thisCharacter.stats.currentActionDice)
        {
            int percent = CalculateDodgeChance(actionDie);
            options.Add(new GenericAction($"[[{actionDie}]]: {percent}% dodge chance", () => DodgeChance(percent, damage, actionDie)));
        }

        options.Add(noAD);
        options.Add(cancel);

        IAmPlayerAction confirm = AnsiConsole.Prompt(
            new SelectionPrompt<IAmPlayerAction>()
            .AddChoices(options)
            .UseConverter(option => option.display));

        confirm.PerformAction();
    }

    int CalculateDodgeChance(int dieValue)
    {
        return dieValue * 10 + thisCharacter.stats.evasion;
    }

    void DodgeChance(int percent, int damage, int dieValue = 0)
    {
        if(dieValue != 0)
        {
            thisCharacter.stats.SpendActionDie(dieValue);
        }

        int roll = Utils.random.Next(1, 101);
        if(roll > percent)
        {
            AnsiConsole.MarkupLine($"{thisCharacter.name} fails to dodge");
            thisCharacter.stats.LoseHealth(damage);
        }
        else
        {
            AnsiConsole.MarkupLine($"{thisCharacter.name} dodges to the side, avoiding all damage");
        }
    }
}
