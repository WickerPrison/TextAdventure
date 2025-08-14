#nullable disable
using Spectre.Console;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

public enum CombatState
{
    START, PLAYER_TURN, ENEMY_TURN
}

public class CombatManager
{
    CombatState combatState;
    List<IDoCombat> combatants;

    public void StartCombat(List<Enemy> enemies)
    {
        CombatData.enemies = enemies;
        combatState = CombatState.START;

        AnsiConsole.WriteLine("");
        AnsiConsole.Write(new Rule("Begin Combat"));
        AnsiConsole.WriteLine("");
        Console.ReadKey();

        CombatUtils.DisplayEnemies(enemies);
        foreach(PlayerCharacter character in Data.party)
        {
            character.stats.RollActionDice();
        }
        CombatUtils.DisplayParty(Data.party);
        AnsiConsole.WriteLine("");
        Console.ReadKey();

        combatants = new List<IDoCombat>(enemies);
        combatants.AddRange(Data.party);
        combatants = combatants.OrderByDescending(combatant => combatant.speed).ToList();

        NextTurn(0);
    }

    void NextTurn(int index)
    {
        if(CombatData.enemies.Count == 0)
        {
            AnsiConsole.WriteLine("You win!");
            return;
        }

        Action callback = () => NextTurn(index + 1 >= combatants.Count ? 0 : index + 1);
        combatants[index].StartTurn(callback);
    }
}
