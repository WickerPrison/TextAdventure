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
        PlayerCharacter target = Data.party[0];
        int damage = Utils.random.Next(4, 10);
        AnsiConsole.WriteLine($"{name} shoots a bolt of lightning at {target.name} for {damage} damage");
        target.GetAttacked(damage);
        nextTurn();
    }

    public override void TakeDamage(int amount, PlayerCharacter damageDealer)
    {
        if (hp <= 0) return;

        hp -= amount;
        if(hp > 0)
        {
            AnsiConsole.WriteLine($"{name} takes {amount} damage! It has {hp}/{maxHp} HP left.");
        }
        else
        {
            CombatData.enemies.Remove(this);
            AnsiConsole.WriteLine($"The {name} lies dead. The storm outside immediately begins to lighten.");
        }
    }
}