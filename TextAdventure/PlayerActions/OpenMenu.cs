using Spectre.Console;
using Spectre.Console.Rendering;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

public class OpenMenu: IAmPlayerAction
{
    public string display { get; set; }
    Action menuAction;

    public OpenMenu(string display, Action menuAction)
    {
        this.display = display;
        this.menuAction = menuAction;
    }

    public void PerformAction()
    {
        menuAction();
    }
}
