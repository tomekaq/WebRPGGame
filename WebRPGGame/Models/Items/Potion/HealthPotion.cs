using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ModelingObjectTask.Items
{
    class HealthPotion : Potion
    { 
        public int Health{get;set;}
        public override void Apply(Hero hero)
        {
            hero.ChangeHealth(-Health);
        }
    }
}
