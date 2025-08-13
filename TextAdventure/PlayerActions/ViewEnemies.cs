using Spectre.Console.Rendering;
using Spectre.Console;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

public class ViewEnemies: IAmPlayerAction
{
    public string display { get; set; } = "View Foes";
    Action menuAction;

    public ViewEnemies(Action menuAction)
    {
        this.menuAction = menuAction;
    }

    public void PerformAction()
    {
        CombatUtils.DisplayEnemies(CombatData.enemies);
        menuAction();
    }
}
