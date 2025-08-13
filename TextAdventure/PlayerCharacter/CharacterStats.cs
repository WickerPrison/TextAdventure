#nullable disable
using Microsoft.VisualBasic.FileIO;
using Spectre.Console;
using System;
using System.Collections.Generic;
using System.ComponentModel.Design;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

public enum StatTypes
{
    EVASION, DEFIANCE
}

public class CharacterStats
{
    public string name { get; set; }
    public int defiance { get; set; }
    public int evasion { get; set; }
    public int speed { get; set; } = 5;
    public int maxHp;
    public int hp;
    public int defense;

    public int actionDice;
    public List<int> currentActionDice = new List<int>();

    public CharacterStats()
    {
        hp = maxHp;
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

    public void LoseHealth(int amount)
    {
        AnsiConsole.WriteLine($"{name} takes {amount} damage");
        hp -= amount;
    }
}
