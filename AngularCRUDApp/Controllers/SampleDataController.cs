using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;

namespace AngularCRUDApp.Controllers
{
    public class Book
    {
        public string Title { get; set; }
        public string Author { get; set; }
        public string DisplayTitleAndAuthor { get { return Title + Author; } }
    }

    [Route("api/[controller]")]
    public class SampleDataController : Controller
    {
        private static string[] Summaries = new[]
        {
            "Freezing", "Bracing", "Chilly", "Cool", "Mild", "Warm", "Balmy", "Hot", "Sweltering", "Scorching"
        };

        [HttpGet("[action]")]
        public IEnumerable<Book> DataFromDataBase()
        {

            SqlConnection connection = new SqlConnection(@"Data Source=DESKTOP-IA957IQ\SQLEXPRESS;Initial Catalog=SelectDb;Integrated Security=SSPI;");
            connection.Open();

            SqlCommand cmd = new SqlCommand();

            cmd.CommandText = "SELECT Title, Author FROM Books";

            cmd.Connection = connection;

            SqlDataReader reader = cmd.ExecuteReader();

            List<Book> books = new List<Book>();

            while(reader.Read())
            {
                Book book = new Book();
                book.Title = reader.GetString(0); 
                book.Author = reader.GetString(1);

                books.Add(book);
            }

            return books;
        }

        [HttpGet("[action]")]
        public IEnumerable<WeatherForecast> WeatherForecasts()
        {
            var rng = new Random();
            return Enumerable.Range(1, 5).Select(index => new WeatherForecast
            {
                DateFormatted = DateTime.Now.AddDays(index).ToString("d"),
                TemperatureC = rng.Next(-20, 55),
                Summary = Summaries[rng.Next(Summaries.Length)]
            });
        }

        public class WeatherForecast
        {
            public string DateFormatted { get; set; }
            public int TemperatureC { get; set; }
            public string Summary { get; set; }

            public int TemperatureF
            {
                get
                {
                    return 32 + (int)(TemperatureC / 0.5556);
                }
            }
        }
    }
}
