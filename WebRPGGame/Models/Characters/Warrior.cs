
using ModelingObjectTask.BodyParts;
using ModelingObjectTask.Items;
using System;
using System.Linq;
using System.Text;

namespace ModelingObjectTask
{
    public class Warrior : Hero
    {
        public Warrior()
        {
            Name = "Geralt";
            Strength = DiceProvider.Instance.Throw(3, 18);
            Agility = DiceProvider.Instance.Throw(2, 12);
            HealthPointsNow = HealthPoints;
        
        }

        public override string Name
        {
            get
            {
                return name;
            }
            set
            {
                name = value;
            }
        }

        public override int Strength
        {
            get
            {
                return strength;
            }
            set
            {
                strength = value;
            }
        }

        public override void AddItem(Item item)
        {
            Type typ = item.GetType(); 
            if (!typ.IsSubclassOf(typeof(Weapons)))
                base.AddItem(item);
            else
            {
                if (typ == typeof(Weapon))
                    base.AddItem(item);
            }
        }

        public override int AttackValue()
        {
            var sum = base.AttackValue();
            return (Strength + sum) + Agility + DiceProvider.Instance.Throw(1, 6);
        }

        public override string ToString()
        {
            var standard = new StringBuilder();
            var Attack = AttackValue();
            standard.Append(base.ToString());
            standard.AppendFormat("Wartość Ataku: {0} ", Attack);
            return standard.ToString();
        }
    }
}
