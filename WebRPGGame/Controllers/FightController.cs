using WebRPGGame.DataAccess;
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

        public ActionResult GetInfoHero()
        {
            var connectionString = DataAccess.DataAccess.CreateConectionString(
                @"C:\Users\user\Documents\visual studio 2013\Projects\WpfRandomValue\WPFDatabase.fdb",
                "SYSDBA", "masterkey", "WIN1250");
            
            return null;
        }
   
    }
}
