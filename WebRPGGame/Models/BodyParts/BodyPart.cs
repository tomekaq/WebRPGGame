using ModelingObjectTask.Items;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;


namespace ModelingObjectTask.BodyParts
{
    public abstract class BodyPart
    {
        protected int health;
        protected List<Item> items = new List<Item>();

        public BodyPart()
        {
            Alive = true;
        }

        public bool Alive
        {
            get;
            private set;
        }

        public IEnumerable<Clothes> Clothes
        {
            get
            {
                return items.Where(i => i is Clothes).Cast<Clothes>();
            }
        }

        public ReadOnlyCollection<Item> Items
        {
            get
            {
                return items.AsReadOnly();
            }
        }

        public int Health
        {
            get
            {
                return health;
            }
            set
            {
                health = (value > 0 ? value : 0);
                Alive = health > 0;
            }
        }

        internal virtual void PutOn<T>(T item) where T : Item
        {
            var t = Items.Where(x => x is T);
            items.Remove(t.FirstOrDefault());
            this.items.Add(item);
        }

        public void ChangeHealth(int AttackValue)
        {
            var Defense = Clothes.Select(x => x.Defense).Sum();
            var Change = Defense - AttackValue;
            if (Change < 0)
                this.Health += Change;
        }
        public override string ToString()
        {
            StringBuilder sb = new StringBuilder();

            sb.AppendFormat("{0}, {1}", this.GetType().Name, this.Health);
            return sb.ToString();
        }
    }
}

