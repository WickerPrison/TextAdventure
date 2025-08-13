using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

public class ViewParty: IAmPlayerAction
{
    public string display { get; set; } = "View Party";
    Action menuAction;

    public ViewParty(Action menuAction)
    {
        this.menuAction = menuAction;
    }

    public void PerformAction()
    {
        CombatUtils.DisplayParty(Data.party);
        menuAction();
    }
}
