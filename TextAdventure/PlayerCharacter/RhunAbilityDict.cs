using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

public static class RhunAbilityDict
{
    public static readonly Dictionary<RhunAction, IAmPlayerAction> abilities = new Dictionary<RhunAction, IAmPlayerAction>()
    {
        { RhunAction.CLEANSING_RAINS, new CleansingRains() }
    };

    public static readonly Dictionary<IAmPlayerAction, RhunAction> reverse = abilities.ToDictionary(i => i.Value, i => i.Key);
}


public enum RhunAction
{
    CLEANSING_RAINS
}
