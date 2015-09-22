using FirebirdSql.Data.FirebirdClient;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace WebRPGGame.Controllers.Account
{
    public class LoginController : Controller
    {
        //
        // GET: /Login/

        public ActionResult Index()
        {
            return View();
        }
        public JsonResult Login(string login,string password)
        {
           string connectionString = DataAccess.DataAccess.CreateConectionString(
                @"C:\Users\user\Documents\Visual Studio 2013\Projects\WebRPGGame\WebRPGGame\DataAccess\RPGDatabase",
                "SYSDBA", "masterkey", "WIN1250");
           using (FbConnection conn = new FbConnection(connectionString))
           {
               conn.Open();
               var command = new FbCommand();
               command.Connection = conn;
               command.CommandText = "select u.ref from users u where email = @email and haslo = @password;";
               command.Parameters.AddWithValue("@email",login);
               command.Parameters.AddWithValue("@password", password);
               FbDataReader readCommand = command.ExecuteReader();
               readCommand.Read();

               if (readCommand[0] != null)
               {
                   var command1 = new FbCommand();
                   command1.Connection = conn;
                   command1.CommandText = "select * from heroes where ref = @refCommand;";
                   command1.Parameters.AddWithValue("@refCommand", readCommand[0].ToString());
                   FbDataReader readCommand1 = command.ExecuteReader();
                   readCommand1.Read();
               }
               return null;
           }
        }
    }
}
