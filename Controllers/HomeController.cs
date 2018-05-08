using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Http;
using QuotingDojos.Connectors;
// using DbConnection;

namespace QuotingDojos.Controllers
{
    public class HomeController : Controller
    {
        private MySqlConnector cnx;
        public HomeController(){
            cnx = new MySqlConnector();
        }

        [HttpGet]
        [Route("")]
        public IActionResult Index()
        {
            return View();
        }

        [HttpPost]
        [Route("/create")]
        public IActionResult CreateQuote(string author, string content)
        {
            string query = $"INSERT INTO quotes (content, author, created_at, updated_at) VALUES ('{content}', '{author}', NOW(), NOW())";
            MySqlConnector.Execute(query);
            return RedirectToAction("Quotes");
        }

        [HttpGet]
        [Route("/quotes")]
        public IActionResult Quotes()
        {
            string query = "SELECT * FROM quotes";
            var quotes = MySqlConnector.Query(query);
            ViewBag.Quotes = quotes;
            Console.WriteLine("test");
            Console.WriteLine(ViewBag.Quotes);
            return View();
        }
    }
}