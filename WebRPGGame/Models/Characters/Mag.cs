using ModelingObjectTask.BodyParts;
using ModelingObjectTask.Items;
using System;
using System.Linq;
using System.Text;

namespace ModelingObjectTask
{
    public class Mag : Hero
    {
        protected int mana;
        public int Mana
        {
            get
            {
                return mana;
            }
            set
            {
                mana = value;
            }
        }

        public Mag()
        {
            Name = "Xardas";
            Strength = DiceProvider.Instance.Throw(1, 6);
            Mana = DiceProvider.Instance.Throw(2, 12);
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

        public override int AttackValue()
        {
            var sum = base.AttackValue();

            return (Mana + Strength + sum) + Agility + DiceProvider.Instance.Throw(1, 6);
        }

        public override void AddItem(Item item)
        {
            Type typ = item.GetType();
            if (!typ.IsSubclassOf(typeof(Weapons)))

                base.AddItem(item);
            else
            {
                if (typ == typeof(MagicWeapon))
                    base.AddItem(item);
            }
        }

        public override string ToString()
        {
            var standard = new StringBuilder();
            var Attack = AttackValue();
            standard.Append(base.ToString());
            standard.AppendFormat("Punkty Many: {0} " , Mana);
            standard.AppendFormat("Wartość Ataku: {0} ", Attack);
            return standard.ToString();
        }
    }
}
