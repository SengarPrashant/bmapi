using System;
using System.Collections.Generic;
using Microsoft.Extensions.Configuration;
using System.Text;
using MySql.Data.MySqlClient;
using System.Data.SqlClient;
using System.Data;

namespace DAL.Common
{
    public class ConfigHelper
    {

        public static Dictionary<string, string> AppSettings { get; set; }

        public static MySqlConnection GetConnection()
        {
            return new MySqlConnection(ConfigHelper.AppSettings["MySqlCon"]);
           
        }


        static ConfigHelper()
        {
            var tmpsetting = ConfigHelper.GetConfig();
            var settingsEnumerator = tmpsetting.AsEnumerable().GetEnumerator();
            AppSettings = new Dictionary<string, string>();
            while (settingsEnumerator.MoveNext())
            {
                AppSettings.Add(((KeyValuePair<string, string>)settingsEnumerator.Current).Key, ((KeyValuePair<string, string>)settingsEnumerator.Current).Value);
            }

        }
        public static IConfiguration GetConfig()
        {
            var relativePath = @"../DAL/Common";
            var absolutePath = System.IO.Path.GetFullPath(relativePath);
            var builder = new ConfigurationBuilder().SetBasePath(absolutePath).AddJsonFile("DalSettings.json");
            return builder.Build();
        }
    }
}
