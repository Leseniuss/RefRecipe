using Microsoft.AspNetCore.Mvc;
using RefRecipe.Data;
using RefRecipe.Models;
using System.Diagnostics;
using OfficeOpenXml;

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
           // ExcelPackage.LicenseContext = LicenseContext.Default;
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

        public ActionResult ReadExcel()
        {
            string filePath = @"C:\reseptit\book1.xlsx"; // "polku_tiedostoon.xlsx"; // Vaihda polku ja tiedostonimi tarpeen mukaan
           // string filePath = @"C:\reseptit\61018721.xlsx"; // "polku_tiedostoon.xlsx"; // Vaihda polku ja tiedostonimi tarpeen mukaan

            using (var package = new OfficeOpenXml.ExcelPackage(new FileInfo(filePath)))
            {
                // var worksheet = package.Workbook.Worksheets["Tuotanto"]; // Oletetaan, että taulukko on ensimmäisellä välilehdellä
                ExcelWorksheet worksheet = package.Workbook.Worksheets[package.Workbook.Worksheets.Count - 1];

               // int rowCount = worksheet.Dimension.Rows;
               // int colCount = worksheet.Dimension.Columns;
                int rowCount = 36;
                int colCount = 10;

                List<List<string>> data = new List<List<string>>();

                for (int row = 1; row <= rowCount; row++)
                {
                    List<string> rowData = new List<string>();

                    for (int col = 1; col <= colCount; col++)
                    {
                        if (col != 5 && col != 6 && col != 7)
                            {
                            rowData.Add(worksheet.Cells[row, col].Value?.ToString() ?? string.Empty);
                        }
                    }

                    data.Add(rowData);
                }

                return View(data);
            }
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}