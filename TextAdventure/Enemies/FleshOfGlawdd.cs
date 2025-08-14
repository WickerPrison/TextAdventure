using Spectre.Console;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

public class FleshOfGlawdd : Enemy
{

    public FleshOfGlawdd()
    {
        maxHp = 20;
        hp = maxHp;
        speed = 1;
        name = "Flesh of Glawdd";
    }

    public override void StartTurn(Action nextTurn)
    {
        AnsiConsole.WriteLine("");
        AnsiConsole.Write(new Rule($"{name}'s Turn"));
        Console.ReadKey();

        PlayerCharacter target = Data.party[0];
        int damage = Utils.random.Next(4, 10);
        AnsiConsole.WriteLine("");
        AnsiConsole.MarkupLine($"{name} shoots a bolt of lightning at {target.name} for [red]{damage}[/] damage. {target.name} has [red]{target.stats.hp}/{target.stats.maxHp}[/] HP.");
        target.GetAttacked(damage);
        Console.ReadKey();
        nextTurn();
    }

    public override void TakeDamage(int amount, PlayerCharacter damageDealer)
    {
        if (hp <= 0) return;

        hp -= amount;
        if(hp > 0)
        {
            AnsiConsole.MarkupLine($"{name} takes [red]{amount}[/] damage! It has [red]{hp}/{maxHp}[/] HP left.");
        }
        else
        {
            CombatData.enemies.Remove(this);
            AnsiConsole.WriteLine("");
            AnsiConsole.MarkupLine($"The {name} lies dead. The storm outside immediately begins to lighten.");
        }
    }
}