#nullable disable
using Spectre.Console;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

class Rhun: PlayerCharacter
{
    public Rhun() : base()
    {
        name = "Rhun";
        speed = 5;
        character = Character.RHUN;
        stats.evasion = 0;
        stats.defiance = 2;
        stats.actionDice = 3;
        stats.maxHp = 20;
        stats.hp = stats.maxHp;
    }

    public override void RootCombatMenu()
    {
        AnsiConsole.WriteLine("");
        OpenMenu attacksMenu = new OpenMenu("Attack", AttacksMenu);
        OpenMenu rootMenu = new OpenMenu("Back", RootCombatMenu);
        Breathe breathe = new Breathe(RootCombatMenu, this);
        ViewEnemies viewEnemies = new ViewEnemies(RootCombatMenu);
        ViewParty viewParty = new ViewParty(RootCombatMenu);

        IAmPlayerAction option = AnsiConsole.Prompt(
            new SelectionPrompt<IAmPlayerAction>()
            .AddChoices([attacksMenu, breathe, viewEnemies, viewParty])
            .UseConverter(option => option.display));

        option.PerformAction();
    }

    void AttacksMenu()
    {
        IronShortsword ironShortsword = new IronShortsword(AttacksMenu, this);
        OpenMenu rootMenu = new OpenMenu("Back", RootCombatMenu);
        IAmPlayerAction option = AnsiConsole.Prompt(
            new SelectionPrompt<IAmPlayerAction>()
            .AddChoices([ironShortsword, rootMenu])
            .UseConverter(option => option.display));

        option.PerformAction();
    }

    public override void GetAttacked(int damage)
    {
        Dodge dodge = new Dodge(this, () => GetAttacked(damage), damage);
        Block block = new Block(this, () => GetAttacked(damage), damage);
        IAmPlayerAction option = AnsiConsole.Prompt(
            new SelectionPrompt<IAmPlayerAction>()
            .AddChoices([dodge, block])
            .UseConverter(option => option.display));

        option.PerformAction();
    }

    public override void Death()
    {
        Data.party.Remove(this);
        AnsiConsole.MarkupLine($"{name} has fallen!");
    }
}
