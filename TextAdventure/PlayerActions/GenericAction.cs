using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

class GenericAction: IAmPlayerAction
{
    public string display { get; set; }
    Action action;

    public GenericAction(string display, Action action)
    {
        this.display = display;
        this.action = action;
    }

    public void PerformAction()
    {
        action();
    }
}
