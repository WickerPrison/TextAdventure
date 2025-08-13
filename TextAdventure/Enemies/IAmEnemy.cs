using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

public interface IAmEnemy: IDoCombat
{
    int maxHp { get; set; }
    int hp { get; set; }
    void TakeDamage(int amount, PlayerCharacter damageDealer);
}
