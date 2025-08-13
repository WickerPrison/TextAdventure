using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

public interface IAmPlayerAction
{
    string display { get; set; }
    void PerformAction();
}
