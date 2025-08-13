using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

class StartCombatTrigger: IAmTrigger
{
    List<Enemy> enemies;

    public StartCombatTrigger(List<Enemy> enemies)
    {
        this.enemies = enemies;
    }

    public void FireTrigger()
    {
        new CombatManager().StartCombat(enemies);
    }
}
