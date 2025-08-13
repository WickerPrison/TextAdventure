#nullable disable
using Spectre.Console;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

public enum Character
{
    RHUN
}

public abstract class PlayerCharacter: IDoCombat
{
    public Character character;
    public string name { get { return stats.name; } set { stats.name = value; } }
    public CharacterStats stats;
    public int speed { get { return stats.speed; } set { stats.speed = value; } }
    Action nextTurn;

    public PlayerCharacter()
    {
        stats = new CharacterStats();
        Data.party.Add(this);
    }

    public virtual void StartTurn(Action nextTurn)
    {
        this.nextTurn = nextTurn;
        AnsiConsole.WriteLine($"It is {name}'s turn");
        RootCombatMenu();
    }

    public void NextTurn()
    {
        nextTurn();
    }

    public abstract void RootCombatMenu();
    public abstract void GetAttacked(int damage);
}
