using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ModelingObjectTask.Items
{
    public class MagicRing : Clothes
    {
        public override void Apply(Hero hero)
        {
            hero.Strength += 1;

        }
        public void Unapply(Hero hero)
        {
            hero.Strength -= 1;
        }
    }
}
