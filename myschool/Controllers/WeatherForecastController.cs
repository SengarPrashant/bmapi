using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using MySql.Data.MySqlClient;
using System.Data;
using myschool.Common;


namespace myschool.Controllers
{
    //[CustomAuthorization]
    [ApiController]
    [Route("api/[controller]")]
    public class WeatherForecastController : ApiBaseController
    {
        private static readonly string[] Summaries = new[]
        {
            "Freezing", "Bracing", "Chilly", "Cool", "Mild", "Warm", "Balmy", "Hot", "Sweltering", "Scorching"
        };

        private readonly ILogger<WeatherForecastController> _logger;

        public WeatherForecastController(ILogger<WeatherForecastController> logger)
        {
            _logger = logger;
        }

        //[HttpGet]
        //public IEnumerable<WeatherForecast> Get()
        //{
        //    var rng = new Random();
        //    return Enumerable.Range(1, 5).Select(index => new WeatherForecast
        //    {
        //        Date = DateTime.Now.AddDays(index),
        //        TemperatureC = rng.Next(-20, 55),
        //        Summary = Summaries[rng.Next(Summaries.Length)]
        //    })
        //    .ToArray();
        //}
        [HttpGet]
        public IEnumerable<Test> Get()
        {
            var lst = new List<Test>();
            //"server=209.205.219.162;port=3306;database=findeasl_facto;user=findeasl_facto;password=singh@205;"
            using (MySqlConnection conn = new MySqlConnection("server=localhost;port=3306;database=MySchool;user=findeasily;password=Singh@205;"))
            {
                conn.Open();
                //select * from tbl_users limit 10
                MySqlCommand cmd = new MySqlCommand("SELECT * FROM `users`", conn);

                using (var reader = cmd.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        //lst.Add(new Test { id = Convert.ToInt32(reader["Id"]), Name = reader["Name"].ToString() });
                        lst.Add(new Test { id = Convert.ToInt32(reader["id"]), Name = reader["FName"].ToString(),
                            CreatedDate=Convert.ToDateTime(reader["CreatedDate"]) });
                    }
                }
            }
            return lst.ToArray();

           
        }

    }
}


public class Test
{
    public int id { get; set; }
    public string Name { get; set; }

    public DateTime CreatedDate { get; set; }
}