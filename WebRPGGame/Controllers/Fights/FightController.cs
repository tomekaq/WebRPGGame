using WebRPGGame.DataAccess;
using FirebirdSql.Data.FirebirdClient;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Monad.Parsec.Token.Numbers;
using System.Threading;
using ModelingObjectTask;
using System.Net;

namespace WebRPGGame.Controllers
{
    public class FightController : Controller
    {
        //
        // GET: /Fight/

        public ActionResult Index()
        {
            return View();
        }

        public FbDataReader GetInfoHero(string refinput)
        {
            int ref1 = int.Parse(refinput);
            var connectionString = DataAccess.DataAccess.CreateConectionString(
                @"C:\Users\user\Documents\Visual Studio 2013\Projects\WebRPGGame\WebRPGGame\DataAccess\RPGDATABASE.fdb",
                "SYSDBA", "masterkey", "WIN1250");

            using (FbConnection conn = new FbConnection(connectionString))
            {
                conn.Open();
                var param1 = new FbParameter();
                param1.ParameterName = "@REFERENSE1";
                param1.Value = ref1;

                var command1 = new FbCommand();
                command1.Connection = conn;
                command1.CommandText = string.Format("select name, agility,strength,type, defense from heroes where ref = @REFERENSE1");
                command1.Parameters.Add(param1);

                FbDataReader reader = command1.ExecuteReader();



                return reader;
            }
            //  return null;
        }
        public Hero CreateHero(string refinput)
        {
            var ReadData = GetInfoHero(refinput);
            if (1==1)//ReadData["type"] == "Warrior")
            {
                Warrior Heroes = new Warrior()
                {
                    Agility = int.Parse(ReadData["agility"].ToString()),
                    DefensePoint = int.Parse(ReadData["defense"].ToString()),
                    Strength = int.Parse(ReadData["strength"].ToString()),
                    Name = ReadData["name"].ToString(),
                    HealthPoints = int.Parse(ReadData["agility"].ToString())
                };
                return Heroes;
            }
            if (1==1)//ReadData["type"] == "Wizzard")
            {
                Mag Heroes = new Mag()
                {
                    Agility = int.Parse(ReadData["agility"].ToString()),
                    DefensePoint = int.Parse(ReadData["defense"].ToString()),
                    Strength = int.Parse(ReadData["strength"].ToString()),
                    Name = ReadData["name"].ToString(),
                    HealthPoints = int.Parse(ReadData["agility"].ToString())
                };
                return Heroes;
            }
            return null;


        }

        public JsonResult ServerMessage(string message)
        {
            return Json(new { success = true, message});

        }

        public ActionResult Fight()
        {

            var HeroFirst = CreateHero("11");
            var HeroSecond = CreateHero("13");



























            int i = 0;
            while (HeroFirst.IsAlive && HeroSecond.IsAlive)
            {
                string message = "";
                Thread.Sleep(50);
                HeroFirst.Attack(HeroSecond);
                message = string.Format("{0} attack {1}!", HeroFirst.Name, HeroSecond.Name);


                Thread.Sleep(50);
                Console.WriteLine("Xardas health: {0}", HeroFirst.HealthPointsNow);
                Thread.Sleep(50);
                Console.WriteLine("Gerlat health: {0}", HeroSecond.HealthPointsNow);
                if ((HeroFirst.IsAlive && HeroSecond.IsAlive))
                {
                    HeroSecond.Attack(HeroFirst);
                    Thread.Sleep(50);
                    Console.WriteLine("Geralt attack Xardas!");
                    message = string.Format("{0} attack {1}!", HeroSecond.Name, HeroFirst.Name);
                    Thread.Sleep(50);
                    Console.WriteLine("Xardas health: {0}", HeroFirst.HealthPointsNow);

                } i++;

            }
            Console.WriteLine("End fight!");
            Console.WriteLine("Who win?");
            if (HeroFirst.IsAlive)
            {
                if (!HeroSecond.head.Alive)
                    Console.WriteLine("Xardas cut Geralt head!");
                Console.WriteLine("Xardas win");

            }
            else
            {
                if (!HeroSecond.head.Alive)
                    Console.WriteLine("Geralt cut Xardas head!");
                Console.WriteLine("Geralt win");
            }



            return null;
        }
    }
}
