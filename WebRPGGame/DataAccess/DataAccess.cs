using FirebirdSql.Data.FirebirdClient;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WebRPGGame.DataAccess
{
    public class DataAccess
    {
        public static string CreateConectionString(string databaseFile,
                      string userName, string userPass, string _charset)
        {
            FbConnectionStringBuilder ConnectionSB = new FbConnectionStringBuilder();
            ConnectionSB.Database = databaseFile;
            ConnectionSB.UserID = userName;
            ConnectionSB.Password = userPass;
            ConnectionSB.Charset = _charset;
            ConnectionSB.Pooling = false;
            return ConnectionSB.ToString();
        }
    }
}