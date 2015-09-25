using FirebirdSql.Data.FirebirdClient;
using ModelingObjectTask;
using ModelingObjectTask.BodyParts;
using System;
using System.Collections.Generic;
using System.Threading;
using System.Web.Mvc;



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

        public List<string> GetInfoHero(string refinput)
        {
            List<string> readData = new List<string>();
            int ref1 = int.Parse(refinput);
          
            string connectionString = DataAccess.DataAccess.CreateConectionString(
                      @"C:\Users\user\Documents\Visual Studio 2013\Projects\WebRPGGame\WebRPGGame\DataAccess\RPGDatabase.fdb",
                      "SYSDBA", "masterkey", "WIN1250");

            using (FbConnection conn = new FbConnection(connectionString))
            {
                conn.Open();

                var command = new FbCommand();
                command.Connection = conn;
                command.Parameters.AddWithValue("@REFERENSE1", 6);
                command.CommandText = "execute procedure GetHeroInfo(6)";
                command.ExecuteScalar();

                using (var reader = command.ExecuteReader())
                {

                    int i = 0;
                    while (reader.Read())
                    {
                        readData.Add(reader[i].ToString());
                        i++;
                    }
                    return readData;
                }
            }
        }
        public Warrior CreateWarrior(string refinput)
        {

            //    ReadData.Read();
            Warrior Heroes = new Warrior()
               {
                   //Agility = int.Parse(ReadData["agility"].ToString()),
                   //DefensePoint = int.Parse(ReadData["defense"].ToString()),
                   //Strength = int.Parse(ReadData["strength"].ToString()),
                   //Name = ReadData["name"].ToString(),
                   //HealthPoints = int.Parse(ReadData["agility"].ToString())
               };
            return Heroes;
        }

        public Mag CreateWizzard(string refinput)
        {
            var ReadData = GetInfoHero(refinput);

            Mag Heroes = new Mag()
            {
                Name = ReadData[0].ToString(),
                Agility = int.Parse(ReadData[1].ToString()),
                Strength = int.Parse(ReadData[2].ToString()),
                DefensePoint = int.Parse(ReadData[4].ToString()),
                HealthPoints = int.Parse(ReadData[6].ToString()),
                HealthPointsNow = int.Parse(ReadData[6].ToString()),

                Body = new Body
                {
                    Health = int.Parse(ReadData[9].ToString())
                },
                Head = new Head
                {
                    Health = int.Parse(ReadData[13].ToString())
                },
                LeftHand = new LeftHand
                {
                    Health = int.Parse(ReadData[11].ToString())
                },
                RightHand = new RightHand
                {
                    Health = int.Parse(ReadData[12].ToString())
                },
                Legs = new Legs
                {
                    Health = int.Parse(ReadData[10].ToString())
                }

            };
            return Heroes;
        }

        public JsonResult CreateWarrior(int ref1)
        {
            var info = CreateWarrior(ref1);

            return Json(new { success = true, info }, JsonRequestBehavior.AllowGet);
        }

        public JsonResult ServerMessage(string message)
        {
            return Json(new { success = true, message });
        }

        public JsonResult CallToFight(string ID, string MyID)
        {
            var message = "Przekierowuje";//enemy["Name"].ToString();

            var Hero1 = GetInfoHero(ID);

            var Hero2 = GetInfoHero(MyID);

            return Json(new { success = true, Hero1 }, JsonRequestBehavior.AllowGet);
        }

        public ActionResult Fight()
        {

            var HeroFirst = CreateWarrior("11");
            var HeroSecond = CreateWizzard("13");

            int i = 0;
            while (HeroFirst.IsAlive && HeroSecond.IsAlive)
            {
                string message = "";
                Thread.Sleep(50);
                HeroFirst.Attack(HeroSecond);
                message = string.Format("{0} attack {1}!", HeroFirst.Name, HeroSecond.Name);

                Thread.Sleep(50);

                Thread.Sleep(50);

                if ((HeroFirst.IsAlive && HeroSecond.IsAlive))
                {
                    HeroSecond.Attack(HeroFirst);
                    Thread.Sleep(50);
                    Console.WriteLine("Geralt attack Xardas!");
                    message = string.Format("{0} attack {1}!", HeroSecond.Name, HeroFirst.Name);
                    Thread.Sleep(50);
                } i++;
            }


            return null;
        }
    }
}
