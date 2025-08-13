using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

public interface IDoCombat
{
    int speed { get; set; }
    string name { get; set; }
    void StartTurn(Action callback);
}
