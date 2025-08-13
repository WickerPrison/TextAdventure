using Spectre.Console;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

public abstract class Enemy: IDoCombat
{
    public string name { get; set; }
    public int speed { get; set; }
    public int maxHp { get; set; }
    public int hp { get; set; }

    public abstract void StartTurn(Action nextTurn);
    public abstract void TakeDamage(int amount, PlayerCharacter damageDealer);

    public void DisplayEnemy()
    {
        Panel panel = new Panel($"{name}\n HP:{hp}/{maxHp}");

        // Sets the border
        panel.Border = BoxBorder.Ascii;
        panel.Border = BoxBorder.Square;
        panel.Border = BoxBorder.Rounded;
        panel.Border = BoxBorder.Heavy;
        panel.Border = BoxBorder.Double;

        AnsiConsole.Write(panel);
    }
}
