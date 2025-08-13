using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

class Rhun: IAmPlayerCharacter
{
    public CharacterStats stats { get; set; }

    public Rhun()
    {
        this.stats = new CharacterStats();
    }
}
