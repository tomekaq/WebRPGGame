using WebRPGGame.DataAccess;
using FirebirdSql.Data.FirebirdClient;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
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

        public ActionResult GetInfoHero(int ref1, int ref2)
        {
            var connectionString = DataAccess.DataAccess.CreateConectionString(
                @"C:\Users\user\Documents\visual studio 2013\Projects\WpfRandomValue\WPFDatabase.fdb",
                "SYSDBA", "masterkey", "WIN1250");

            using (FbConnection conn = new FbConnection())
            {
             
                var param1 = new FbParameter();
                param1.ParameterName = "@REFETENDSE1";
                param1.Value = ref1;

                var command1 = new FbCommand();
                command1.CommandText = string.Format("select name, agility,strength, defense from heroes where ref = @REFERENSE1");
                command1.Parameters.Add(param1);

                var param2 = new FbParameter();
                param2.ParameterName = "@REFETENDSE2";
                param2.Value = ref2;

                var command2 = new FbCommand();
                command2.CommandText = string.Format("select name, agility,strength, defense from heroes where ref = @REFERENSE1");
                command2.Parameters.Add(param2);
            }
            return null;
        }
   
    }
}
