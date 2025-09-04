using Spectre.Console.Rendering;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

public interface IAmReward
{
    string optionDisplay { get; set; }
    void ShowReward();
    void GainReward();
}
