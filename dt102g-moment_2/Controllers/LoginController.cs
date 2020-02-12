using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Http;
using dt102g_moment_2.Models;
using Newtonsoft.Json;

// For more information on enabling MVC for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace dt102g_moment_2.Controllers
{
    public class LoginController : Controller
    {
        // GET: /<controller>/
        public IActionResult Login()
        {
            return View();
        }

        // POST new visit
        [HttpPost]
        public ActionResult GetName(Name nameVar)
        {
            // Read JSON file and deserialize object
            var JsonStr = System.IO.File.ReadAllText("latestvisit.json");
            var JsonObj = JsonConvert.DeserializeObject<List<Latest>>(JsonStr);

            // Find biggest ID, to create a unique new one
            Latest biggest = JsonObj.Aggregate((i1, i2) => i1.ID > i2.ID ? i1 : i2);

            // Add new visit
            JsonObj.Add(new Latest()
            {
                ID = biggest.ID + 1,
                Name = nameVar.NameProp,
                CurrentTime = DateTime.Now
            });

            // Serialize object
            string json = JsonConvert.SerializeObject(JsonObj, Formatting.Indented);

            // Write to file
            System.IO.File.WriteAllText("latestvisit.json", json);

            // Set session of visitors name
            if(ModelState.IsValid)
            {
                // Get name from Model
                string nameStr = nameVar.NameProp;
                // Set session with name
                HttpContext.Session.SetString("nameSess", nameStr);
            }

            // Redirect to index page
            return Redirect("Index");
        }

        // Index page after name insert
        public IActionResult Index()
        {
            // Get session and save to ViewData
            ViewData["viewName"] = HttpContext.Session.GetString("nameSess");

            // Läs in JSON-fil
            var JsonStr = System.IO.File.ReadAllText("latestvisit.json");
            var JsonObj = JsonConvert.DeserializeObject<List<Latest>>(JsonStr);
            JsonObj.Reverse();

            return View(JsonObj);
        }
    }
}
