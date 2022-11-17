using Microsoft.AspNetCore.Mvc;
using RefRecipe.Data;
using RefRecipe.Models;
using System.Diagnostics;

namespace RefRecipe.Controllers
{
    public class HomeController : Controller
    {
        /* private readonly ILogger<HomeController> _logger;

         public HomeController(ILogger<HomeController> logger)
         {
             _logger = logger;
         } */
        private readonly RefRecipeContext _context;

        public HomeController(RefRecipeContext context)
        {
            _context = context;
        }

        public IList<Recipe> Recipe { get; set; } = default!;

         public IActionResult Index()
         {
             if (_context.Recipes != null)
             {
                 Recipe = _context.Recipes.OrderBy(p => p.Koodi).ToList();
             }

             return View(Recipe);


         } 
       /* public IActionResult Index()
        {
            return View();
        } */

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