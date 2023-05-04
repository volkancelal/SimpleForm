using Microsoft.AspNetCore.Mvc;
using SimpleForm.Models;
using System.Diagnostics;
using System.Text.Json;

namespace SimpleForm.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;

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

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }

        [HttpPost]
        public async Task<IActionResult> SaveName(PersonModel person)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            // Save the full name in a file called person.json. This file will be created at the root folder of the project.
            var filePath = Path.Combine(Directory.GetCurrentDirectory(), "person.json");
            using var stream = new FileStream(filePath, FileMode.Create);
            await JsonSerializer.SerializeAsync(stream, person);

            // Assuming the process was successful return a success message. Additional validations can be added here.
            ViewBag.Message = "Full name submitted successfully.";

            return View("Index");
        }
    }
}