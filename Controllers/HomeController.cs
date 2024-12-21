using System.Diagnostics;
using chaosScryfallRegexGeneratorASP.NET.Models;
using Microsoft.AspNetCore.Mvc;

namespace chaosScryfallRegexGeneratorASP.NET.Controllers
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
			return View(); // This will look for Views/Home/Index.cshtml
		}

		public IActionResult Search()
		{
			return View(); // Views/Home/Search.cshtml (if it exists)
		}

		public IActionResult Results()
		{
			return View(); // Views/Home/Results.cshtml (if it exists)
		}

		public IActionResult Summary()
		{
			return View(); // Views/Home/Summary.cshtml (if it exists)
		}

		public IActionResult Feedback()
		{
			return View(); // Views/Home/Feedback.cshtml (if it exists)
		}

		public IActionResult Suggestions()
		{
			return View(); // Views/Home/Suggestions.cshtml (if it exists)
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
    }
}
