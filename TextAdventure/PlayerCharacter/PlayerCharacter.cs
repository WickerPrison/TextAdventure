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
        stats = new CharacterStats(Death);
        Data.party.Add(this);
    }

    public virtual void StartTurn(Action nextTurn)
    {
        this.nextTurn = nextTurn;
        AnsiConsole.WriteLine("");
        AnsiConsole.Write(new Rule($"{name}'s Turn"));
        RootCombatMenu();
    }

    public void NextTurn()
    {
        nextTurn();
    }

    public void DisplayCharacter()
    {
        Panel panel = new Panel($"[bold]{name}[/]" +
            $"\nHP: [red]{stats.hp}/{stats.maxHp}[/]" +
            $"\nAction Dice:[green]{CombatUtils.ActionDiceDisplay(this)}[/]" +
            $"\nDefiance: {stats.defiance}" +
            $"\nEvasion: {stats.evasion}" +
            $"\nDefense: {stats.defense}" +
            $"\nSpeed: {stats.speed}");

        panel.Border = BoxBorder.Double;

        AnsiConsole.Write(panel);
    }

    public abstract void RootCombatMenu();
    public abstract void GetAttacked(int damage);
    public abstract void Death();
}
