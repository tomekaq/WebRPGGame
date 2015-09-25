using FirebirdSql.Data.FirebirdClient;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using WebRPGGame.DataAccess;

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

        public JsonResult GetGamerList(int ref1)
        {
            ObservableCollection<ModelView> data = new ObservableCollection<ModelView>();
            data = GetGamerListFromDatabase(ref1);

            return Json(new { success = true, data }, JsonRequestBehavior.AllowGet);
        }
        public ObservableCollection<string> GetUsers()
        {
            string connectionString = DataAccess.DataAccess.CreateConectionString(
                @"C:\Users\user\Documents\Visual Studio 2013\Projects\WebRPGGame\WebRPGGame\DataAccess\RPGDatabase.fdb",
                "SYSDBA", "masterkey", "WIN1250");
            using (FbConnection conn = new FbConnection(connectionString))
            {
                conn.Open();
                ObservableCollection<string> observableCollection = new ObservableCollection<string>();
                var command = new FbCommand();
                command.Connection = conn;
                command.CommandText = "select u.ref from users u;";
                command.ExecuteScalar();

                using (var dataRead = command.ExecuteReader())
                {
                    while (dataRead.Read())
                    {
                        observableCollection.Add(
                            dataRead[0].ToString());
                    }
                }
                return observableCollection;
            }
        }
        public ObservableCollection<ModelView> GetGamerListFromDatabase(int ref1)
        {

            string connectionString = DataAccess.DataAccess.CreateConectionString(
                @"C:\Users\user\Documents\Visual Studio 2013\Projects\WebRPGGame\WebRPGGame\DataAccess\RPGDatabase.fdb",
                "SYSDBA", "masterkey", "WIN1250");
            using (FbConnection conn = new FbConnection(connectionString))
            {
                conn.Open();
                ObservableCollection<ModelView> observableCollection = new ObservableCollection<ModelView>();
                var command = new FbCommand();
                command.Connection = conn;
                //string sqlCommand = @"select u.ref,u.login,h.name,h.level,h.strength,h.agility,h.defense from users u join heroes h on u.ref = h.""USER"" where u.ref is distinct from @ref";
                string sqlCommand = "execute procedure GetHeroInfo(6)";
                
                //command.Parameters.AddWithValue("@ref", ref1);
                command.CommandText = sqlCommand;
                command.ExecuteScalar();

                using (var dataRead = command.ExecuteReader())
                {
                    while (dataRead.Read())
                    {
                        observableCollection.Add(
                            new ModelView()
                            {
                                REF = dataRead.GetString(0).ToString(),
                                User = dataRead.GetString(1).ToString(),
                                Name = dataRead.GetString(2).ToString(),
                                Level = int.Parse(dataRead.GetString(3)),
                                Strength = int.Parse(dataRead.GetString(4)),
                                Agility = int.Parse(dataRead.GetString(5)),
                                Defense = int.Parse(dataRead.GetString(6))
                            });
                    }
                }
                return observableCollection;
            }
        }

        public JsonResult SendOne(ModelView model)
        {
            try
            {
                return Json(new { success = true, model }, JsonRequestBehavior.AllowGet);
            }
            catch
            {
                return Json(new { success = false });
            }
        }

    }
}
