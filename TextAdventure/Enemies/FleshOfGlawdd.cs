using Spectre.Console;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

public class FleshOfGlawdd : IAmEnemy
{
    public string name { get; set; } = "Flesh of Glawdd";
    public int maxHp { get; set; } = 20;
    public int hp { get; set; }
    public int speed { get; set; } = 1;

    public FleshOfGlawdd()
    {
        hp = maxHp;
    }

    public void StartTurn(Action nextTurn)
    {
        CharacterStats target = Data.party[0];
        int damage = Utils.random.Next(4, 10);
        AnsiConsole.WriteLine($"{name} shoots a bolt of lightning at {target.name} for {damage} damage");
        target.GetAttacked(damage);
        nextTurn();
    }

    public void TakeDamage(int amount, CharacterStats damageDealer)
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