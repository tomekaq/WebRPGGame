using FirebirdSql.Data.FirebirdClient;
using ModelingObjectTask;
using ModelingObjectTask.BodyParts;
using System;
using System.Collections.Generic;
using System.Data;
using System.Threading;
using System.Threading.Tasks;
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

        public Hero GetInfoHero(string refinput)
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

                command.CommandText = "GetHeroInfo";
                command.CommandType = CommandType.StoredProcedure;
                command.Parameters.AddWithValue("ID", FbDbType.Integer).Value = ref1;
                command.Connection = conn;
                var reader = command.ExecuteReader();

                reader.Read();

                var f = reader.GetString(3);

                if (f == "WARRIOR")
                {
                    var hero = CreateWarrior(reader);
                    return hero;
                }
                else
                {
                    var hero = CreateWizzard(reader);
                    return hero;
                }
            }
        }
        public Warrior CreateWarrior(FbDataReader ReadData)
        {
            Warrior Heroes = new Warrior()
             {
                 Agility = int.Parse(ReadData["agility"].ToString()),
                 DefensePoint = int.Parse(ReadData["defense"].ToString()),
                 Strength = int.Parse(ReadData["strength"].ToString()),
                 Name = ReadData["name"].ToString(),
                 HealthPoints = int.Parse(ReadData["healthpointmax"].ToString()),
                 HealthPointsNow = int.Parse(ReadData["healthpointnow"].ToString()),
                 Body = new Body
                 {
                     Health = int.Parse(ReadData["BODYHEALTH"].ToString())
                 },
                 Head = new Head
                 {
                     Health = int.Parse(ReadData["HEADHEALTH"].ToString())
                 },
                 LeftHand = new LeftHand
                 {
                     Health = int.Parse(ReadData["LEFTHANDHEALTH"].ToString())
                 },
                 RightHand = new RightHand
                 {
                     Health = int.Parse(ReadData["RIGHTHANDHEALTH"].ToString())
                 },
                 Legs = new Legs
                 {
                     Health = int.Parse(ReadData["LEGSHEALTH"].ToString())
                 }
             };
            return Heroes;
        }


        public Mag CreateWizzard(FbDataReader ReadData)
        {
            Mag Heroes = new Mag()
            {
                Name = ReadData[0].ToString(),
                Agility = int.Parse(ReadData[1].ToString()),
                Strength = int.Parse(ReadData[2].ToString()),
                DefensePoint = int.Parse(ReadData[4].ToString()),
                HealthPoints = int.Parse(ReadData[6].ToString()),
                HealthPointsNow = int.Parse(ReadData[7].ToString()),
                Mana = int.Parse(ReadData[8].ToString()),

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
            var message = "Przekierowuje";

            return Json(new { success = true, message }, JsonRequestBehavior.AllowGet);
        }

        public JsonResult Fight(string ID, string MyID)
        {
     
            var HeroFirst = GetInfoHero(ID);
            var HeroSecond = GetInfoHero(MyID);
            Thread.Sleep(100);

            string message = "nic";
            Thread.Sleep(100);
            //    HeroFirst.Attack(HeroSecond);
            message = string.Format("{0} attack {1}!", HeroFirst.Name, HeroSecond.Name);
          
            var los = new Random();
            if (los.Next(2) == 1)
            {
                var heroTemp = HeroFirst;
                HeroFirst = HeroSecond;
                HeroSecond = heroTemp;
            }  

            Thread.Sleep(1000);
           
            while ((HeroFirst.IsAlive && HeroSecond.IsAlive))
            {
                HeroFirst.Attack(HeroSecond);
                message = string.Format("{0} attack {1}!", HeroFirst.Name, HeroSecond.Name);
                Json(new { success = true, message });

                if ((HeroFirst.IsAlive && HeroSecond.IsAlive))
                {
                    HeroSecond.Attack(HeroFirst);
                    
                    
                    //message = string.Format("{0} attack {1}!", HeroSecond.Name, HeroFirst.Name);
                    Json(new {success = true, message});  
                }
            }
            //}

            if (HeroFirst.IsAlive)
            {
                message = HeroFirst.Name + " win!";
            }
            else
            {
                message = HeroFirst.Name + " win!";
            }
            return Json(new {success = true,message}, JsonRequestBehavior.AllowGet);
        }
        public JsonResult TurnResult(string message)
        {
            return Json(new { success = true, message });
        }
    }
}
