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

        public JsonResult GetGamerList()
        {
            ObservableCollection<ModelView> data = new ObservableCollection<ModelView>();
            data = GetGamerListFromDatabase();
            int i =0;
      
                
                var t = Task.Run(() =>
                {
                    var ts = SendOne(data[i]);
                    return ts;
                });
               // i++;


                return SendOne(data[i]);
        }

        public ObservableCollection<ModelView> GetGamerListFromDatabase()
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
                command.CommandText = "select h.name,h.level,h.strength,h.agility,h.defense from heroes h;";
                command.ExecuteScalar();

                using (var dataRead = command.ExecuteReader())
                {
                    while (dataRead.Read())
                    {
                        observableCollection.Add(
                            new ModelView()
                            {
                                Name = dataRead.GetString(0).ToString(),
                                Level = int.Parse(dataRead.GetString(1)),
                                Strength = int.Parse(dataRead.GetString(2)),
                                Agility = int.Parse(dataRead.GetString(3)),
                                Defense = int.Parse(dataRead.GetString(4))
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
