using FirebirdSql.Data.FirebirdClient;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
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

        public ObservableCollection<ModelView> GetInfo()
        {

            string connectionString = DataAccess.DataAccess.CreateConectionString(
                @"C:\Users\user\Documents\Visual Studio 2013\Projects\WebRPGGame\WebRPGGame\DataAccess\RPGDatabase",
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
                            new ModelView(dataRead.GetString(0).ToString(),
                                    int.Parse(dataRead.GetString(1)),
                                    int.Parse(dataRead.GetString(2)),
                                    int.Parse(dataRead.GetString(3)),
                                    int.Parse(dataRead.GetString(4))));
                    }
                }
                return observableCollection;
            }
        }

        public JsonResult SendGamerList()
        {
            ObservableCollection<ModelView> data = new ObservableCollection<ModelView>();
            data = GetInfo();

            try
            {
                int i = 0;
                var len = data.Count;
                while(i<len){
                    SendOne(data[i]);
                    i++;
                }
                return Json(new { success = true},JsonRequestBehavior.AllowGet);
            }
            catch (Exception e)
            {
                return Json(e.Message);
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
