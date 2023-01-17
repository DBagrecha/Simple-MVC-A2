using Assignment2.Models;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;
using MongoDB.Driver;
using MongoDB.Bson;

namespace Assignment2.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        MongoClient client = new MongoClient("mongodb://localhost:27017");
        public HomeController(ILogger<HomeController> logger)
        {
            _logger = logger;
        }

        public IActionResult Index()
        {
            return View();
        }

        public IActionResult Privacy()
        {
            return View();
        }

        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Create(Student st)
        {
            var db = client.GetDatabase("A2");
            var table = db.GetCollection<Student>("Student");
            table.InsertOne(st);
            ViewBag.Mgs = "Successfully inserted.";
            return View();
        }

        public IActionResult Show()
        {
            var db = client.GetDatabase("A2");
            var table = db.GetCollection<Student>("Student");
            var StudentDetails = table.Find(FilterDefinition<Student>.Empty).ToList();
            return View(StudentDetails);
        }

        public IActionResult Details(int id,string name)
        {
            Console.WriteLine(id); Console.WriteLine(name);
            var db = client.GetDatabase("A2");
            var table = db.GetCollection<Student>("Student");
            var st = table.Find(c=>c.id==id).FirstOrDefault();
            if (st == null)
            {
                return NotFound();
            }
            else
            {
                return View(st);
            }
        }
       

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}