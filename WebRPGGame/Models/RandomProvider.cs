using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ModelingObjectTask
{
    public class DiceProvider
    {
        protected static DiceProvider instance;
        public static DiceProvider Instance
        {
            get
            {
                if(instance==null)
                    instance = new DiceProvider();
                return instance;
            }
        }

        Random rand = new Random();

        public virtual int Throw(int n, int k)
        {
            var val = 0;
            for (int i = 0; i < n; i++)
                val = rand.Next(1, k);
            return val;
        }
    }
}
