#nullable disable
using Spectre.Console;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

public struct AbilityDescription()
{
    public string name;
    public string description;
}

public static class CombatUtils
{
    public static string ActionDiceDisplay(List<int> diceResults)
    {
        string output = "";
        for(int i = 0; i < diceResults.Count; i++)
        {
            output += "[";
            output += diceResults[i].ToString();
            output += "] ";
        }
        return output;
    }

    public static void DisplayEnemies(List<Enemy> enemies)
    {
        foreach(Enemy enemy in enemies)
        {
            enemy.DisplayEnemy();
        }
    }

    public static void DisplayAbilityDescription(AbilityDescription ability)
    {
        Panel panel = new Panel($"{ability.name}\n {ability.description}");

        // Sets the border
        panel.Border = BoxBorder.Ascii;
        panel.Border = BoxBorder.Square;
        panel.Border = BoxBorder.Rounded;
        panel.Border = BoxBorder.Heavy;
        panel.Border = BoxBorder.Double;

        AnsiConsole.Write(panel);
    }

    public static void SelectEnemy(Action<Enemy> attack, IAmPlayerAction backAction)
    {
        AnsiConsole.WriteLine("Choose your target");
        CombatUtils.DisplayEnemies(CombatData.enemies);

        List<IAmPlayerAction> enemyOptions = new List<IAmPlayerAction>();
        foreach (Enemy enemy in CombatData.enemies)
        {
            enemyOptions.Add(new GenericAction(enemy.name, () => attack(enemy)));
        }
        enemyOptions.Add(backAction);

        IAmPlayerAction selectTarget = AnsiConsole.Prompt(
            new SelectionPrompt<IAmPlayerAction>()
            .AddChoices(enemyOptions)
            .UseConverter(option => option.display));

        selectTarget.PerformAction();
    }

    public static void SelectEnemy(Action<Enemy, int> attack, IAmPlayerAction backAction, int dieValue)
    {
        Action<Enemy> newAttack = (target) => attack(target, dieValue);
        SelectEnemy(newAttack, backAction);
    }
}
