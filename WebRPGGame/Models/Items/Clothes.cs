
using System.Globalization;
using System.Text;
namespace ModelingObjectTask.Items
{
    public abstract class Clothes:Item
    {
        public int Defense { get; set; }

        public Clothes()
        {
            Defense = 1;
        }

        public override string ToString()
        {
            StringBuilder sb = new StringBuilder();
            sb.Append(base.ToString());
            sb.AppendFormat("Obrona: {0}",this.Defense);
            return sb.ToString();
        }
    }
}
