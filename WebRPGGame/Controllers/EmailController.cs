using FirebirdSql.Data.FirebirdClient;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Web;
using System.Web.Mail;
using System.Web.Mvc;

namespace WebRPGGame.Controllers
{
    public class EmailController : Controller
    {
        //
        // GET: /Email/

        public ActionResult Index()
        {
            return View();
        }

        public void CallToChallenge(int ref1,int ref2)
        {

            string connectionString = DataAccess.DataAccess.CreateConectionString(
               @"C:\Users\user\Documents\Visual Studio 2013\Projects\WebRPGGame\WebRPGGame\DataAccess\RPGDatabase.fdb",
               "SYSDBA", "masterkey", "WIN1250");
            using (FbConnection conn = new FbConnection(connectionString))
            {
                conn.Open();
              
                var command1 = new FbCommand();
                command1.Connection = conn;
                command1.CommandText = "select u.email from users u where ref = @ref1;";
                command1.Parameters.AddWithValue("@ref1", ref1);
                
                var command2 = new FbCommand();
                command2.Connection = conn;
                command2.CommandText = "select u.email from users u where ref = @ref2;";
                command2.Parameters.AddWithValue("@ref2", ref2);

                FbDataReader readCommand1 = command1.ExecuteReader();
                FbDataReader readCommand2 = command2.ExecuteReader();
                readCommand1.Read();
                readCommand2.Read();

                var email1 = readCommand2[0].ToString();
                var email2 = readCommand1[0].ToString();

                MailAddress to = new MailAddress(email2);
                MailAddress from = new MailAddress(email1);
                System.Net.Mail.MailMessage mail = new System.Net.Mail.MailMessage(from, to);

                mail.Subject = "cos wyslac";
                mail.Body = "cos wyslij";

                SmtpClient smtp = new SmtpClient();
                smtp.Host = "smtp.wp.pl";
                smtp.Port = 465;

                try
                {
                   
               
                smtp.EnableSsl = true;
                Console.WriteLine("Sending email...");
                smtp.Send(mail);
                }
                catch (Exception e)
                {
                    var v = e.Message;
                }
                Response.Write("E-mail sent!");

            }
        }
    }
}
