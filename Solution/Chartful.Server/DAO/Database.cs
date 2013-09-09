using System;
using System.Configuration;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using MySql.Data.MySqlClient;

namespace Chartful.Server.DAO
{
    public static class Database
    {
        public static MySqlConnection GetConnection()
        {
            //return new MySqlConnection(ConfigurationManager.ConnectionStrings["dbChartful"].ConnectionString);
            return new MySqlConnection(ConfigurationManager.ConnectionStrings["test"].ConnectionString);
        }
    }
}