#nullable disable
using Microsoft.VisualBasic.FileIO;
using Spectre.Console;
using System;
using System.Collections.Generic;
using System.ComponentModel.Design;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

public enum Character
{
    RHUN
}

public enum StatTypes
{
    EVASION, DEFIANCE
}

public class CharacterStats: IDoCombat
{
    public Character character;
    public string name { get; set; }
    public int defiance { get; set; }
    public int evasion { get; set; }
    public int speed { get; set; } = 5;
    public int maxHealth;
    int health;
    public int defense;

    public int actionDice;
    public List<int> currentActionDice = new List<int>();
    Action nextTurn;

    public CharacterStats()
    {
        health = maxHealth;
        Data.party.Add(this);

        Dictionary<Character, string> nameDict = new Dictionary<Character, string>() { { Character.RHUN, "Rhun" } };
        name = nameDict[character];
    }

    public void IncrementStat(StatTypes type, int amount)
    {
        switch (type)
        {
            case StatTypes.EVASION:
                evasion += amount;
                break;
            case StatTypes.DEFIANCE:
                defiance += amount;
                break;
        }
    }

    public void RollActionDice()
    {
        currentActionDice.Clear();

        for(int i = 0; i < actionDice; i++)
        {
            currentActionDice.Add(Utils.random.Next(1, 7));
        }
    }

    public void RollActionDice(List<int> rerolls)
    {
        foreach (int dieValue in rerolls)
        {
            currentActionDice.Remove(dieValue);
        }

        for (int i = 0; i < actionDice; i++)
        {
            if (i >= currentActionDice.Count)
            {
                currentActionDice.Add(Utils.random.Next(1, 7));
            }
        }
    }

    public void SpendActionDie(int value)
    {
        currentActionDice.Remove(value);
    }

    public void GetAttacked(int amount)
    {
        Dodge dodge = new Dodge(this, () => GetAttacked(amount), amount);
        Block block = new Block(this, () => GetAttacked(amount), amount);
        IAmPlayerAction option = AnsiConsole.Prompt(
            new SelectionPrompt<IAmPlayerAction>()
            .AddChoices([dodge, block])
            .UseConverter(option => option.display));
        
        option.PerformAction();
    }

    public void LoseHealth(int amount)
    {
        AnsiConsole.WriteLine($"{name} takes {amount} damage");
        health -= amount;
    }

    public void StartTurn(Action nextTurn)
    {
        this.nextTurn = nextTurn;
        AnsiConsole.WriteLine($"It is {name}'s turn");
        RootCombatMenu();
    }

    public void NextTurn()
    {
        nextTurn();
    }

    void RootCombatMenu()
    {
        AnsiConsole.WriteLine("");
        OpenMenu attacksMenu = new OpenMenu("Attack", AttacksMenu);
        OpenMenu rootMenu = new OpenMenu("Back", RootCombatMenu);
        Breathe breathe = new Breathe(RootCombatMenu, this);
        ViewEnemies viewEnemies = new ViewEnemies(RootCombatMenu);

        IAmPlayerAction option = AnsiConsole.Prompt(
            new SelectionPrompt<IAmPlayerAction>()
            .AddChoices([attacksMenu, breathe, viewEnemies])
            .UseConverter(option => option.display));

        option.PerformAction();
    }

    void AttacksMenu()
    {
        IronShortsword ironShortsword = new IronShortsword(AttacksMenu, this);
        OpenMenu rootMenu = new OpenMenu("Back", RootCombatMenu);
        IAmPlayerAction option = AnsiConsole.Prompt(
            new SelectionPrompt<IAmPlayerAction>()
            .AddChoices([ironShortsword, rootMenu])
            .UseConverter(option => option.display));

        option.PerformAction();
    }
}
