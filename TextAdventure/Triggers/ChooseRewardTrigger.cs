using Spectre.Console;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

public class ChooseRewardTrigger: IAmTrigger
{
    List<IAmReward> rewards;

    public ChooseRewardTrigger(List<IAmReward> rewards)
    {
        this.rewards = rewards;
    }

    public void FireTrigger()
    {
        List<IAmPlayerAction> options = new List<IAmPlayerAction>();
        foreach(IAmReward reward in rewards)
        {
            reward.ShowReward();
            options.Add(new GenericAction(reward.optionDisplay, reward.GainReward));
        }

        IAmPlayerAction option = AnsiConsole.Prompt(
            new SelectionPrompt<IAmPlayerAction>()
            .AddChoices(options)
            .UseConverter(option => option.display));

        option.PerformAction();
    }
}
