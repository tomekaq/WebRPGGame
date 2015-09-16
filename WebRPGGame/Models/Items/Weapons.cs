using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ModelingObjectTask.Items
{
    public abstract class Weapons : Item
    {
        public virtual int Attack { get; set; }
        

        public Weapons()
        {
            Attack = 1;
        }

        public override string ToString()
        {
            StringBuilder sb = new StringBuilder();
            sb.AppendFormat(base.ToString());
            sb.AppendFormat("Atak: {0} ", this.Attack);
            return sb.ToString();
        }
    }
}
