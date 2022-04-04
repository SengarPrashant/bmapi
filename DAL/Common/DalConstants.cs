using System;
using System.Collections.Generic;
using System.Text;

namespace DAL.Common
{
    public class DalConstants
    {
        public static string ConString { get; set; } = "server=103.228.112.182;port=3306;database=MySchool;user=findeasily;password=Singh@205;";
        //public static string ConString { get; set; } = "server=localhost;port=3306;database=MySchool;user=findeasily;password=Singh@205;";
        public static string SqlConString { get; set; } = "Data Source=DESKTOP-5QIHQBD;Initial Catalog=BusinessMitra;Integrated Security=True";
        //public static string SqlConString { get; set; } = "workstation id=businessmitra.mssql.somee.com;packet size=4096;user id=prashant89m_SQLLogin_1;pwd=6hsfobm6db;data source=businessmitra.mssql.somee.com;persist security info=False;initial catalog=businessmitra";
    }
}
