using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WebRPGGame.DataAccess
{
    public class ModelView
    {
        public string Name { get; set; }
        public int Level { get; set; }
        public int Strength { get; set; }
        public int Agility { get; set; }
        public int Defense { get; set; }
        public int Mana { get; set; }
    }
}