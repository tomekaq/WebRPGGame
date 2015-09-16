using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace WebRPGGame.Controllers.Account
{
    public class GamerListController : Controller
    {
        //
        // GET: /GamerList/

        public ActionResult Index()
        {
            return View();
        }
        public ActionResult Download()
        {
          //  connectionString = DataAccess.DataAccess.CreateConectionString();

            return null;
        }
    }
}
