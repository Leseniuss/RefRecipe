using Microsoft.AspNetCore.Mvc;
using RefRecipe.Data;
using RefRecipe.Models;
using System.Diagnostics;
using OfficeOpenXml;
using System.Linq;
using Microsoft.Data.Sqlite;
using System.Data.SQLite;

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

        /* public IActionResult Index()
         {
             if (_context.Recipes != null)
             {
                 Recipe = _context.Recipes.OrderBy(p => p.Koodi).ToList();
             }

             return View(Recipe);


         } */
        /* public IActionResult Index()
         {
             return View();
         } */

       /* public IActionResult Index(string SearchText = "")
        {

            List<Recipe> recipes;
            if (SearchText != "" && SearchText != null && _context.Recipes != null)
            {
                recipes = _context.Recipes.Where(p => p.Nimi.Contains(SearchText)).ToList();
            }
            else
            {
                recipes = _context.Recipes.OrderBy(p => p.Koodi).ToList();
            }


            return View(recipes);


        } */

        public IActionResult Index(string SearchText = "", string SearchCode = "")
        {

            List<Recipe> recipes;
            if (SearchText != "" && SearchText != null)
            {
                recipes = _context.Recipes.Where(p => p.Nimi.Contains(SearchText)).ToList();
            }
            else if (SearchCode != "" && SearchCode != null)
            {
                recipes = _context.Recipes.Where(p => p.Koodi.Contains(SearchCode)).ToList();
            }
            else
            {
                recipes = _context.Recipes.OrderBy(p => p.Koodi).ToList();
            }


                return View(recipes);


            }

        public IActionResult Privacy()
        {
            return View();
        }

        public ActionResult ReadExcel()  // EPP HAKEE VAAN .xlsx excel tiedostoja
        {
            //string filePath = @"C:\reseptit\book1.xlsx"; // "polku_tiedostoon.xlsx"; // Vaihda polku ja tiedostonimi tarpeen mukaan
             string filePath = @"C:\reseptit\61066201.xlsx"; // "polku_tiedostoon.xlsx"; // Vaihda polku ja tiedostonimi tarpeen mukaan
            // string filePath = @"C:\reseptit\book1.xlsx"; // "polku_tiedostoon.xlsx"; // Vaihda polku ja tiedostonimi tarpeen mukaan
           // string filePath = @"C:\reseptit\book1.xlsx"; // "polku_tiedostoon.xlsx"; // Vaihda polku ja tiedostonimi tarpeen mukaan

            using (var package = new OfficeOpenXml.ExcelPackage(new FileInfo(filePath)))
            {
                // var worksheet = package.Workbook.Worksheets["Tuotanto (5)"]; // Oletetaan, että taulukko on ensimmäisellä välilehdellä
                // ExcelWorksheet worksheet = package.Workbook.Worksheets[package.Workbook.Worksheets.Count];
                // var worksheet = package.Workbook.Worksheets[package.Workbook.Worksheets.Count - 1];
                 var worksheet = package.Workbook.Worksheets[0];
                // ExcelWorksheet worksheet = package.Workbook.Worksheets.First();
               /* var currentSheet = package.Workbook.Worksheets;
                var worksheet = currentSheet.FirstOrDefault(); */

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
                        if (col != 4 && col != 5 && col != 6 && col != 7)
                            {
                            rowData.Add(worksheet.Cells[row, col].Value?.ToString() ?? string.Empty);
                        }
                    }

                    data.Add(rowData);
                }

                return View(data);
            }
        }

        public ActionResult ReadExcel2(string id)  // EPP HAKEE VAAN .xlsx excel tiedostoja
        {
            //string filePath = @"C:\reseptit\book1.xlsx"; // "polku_tiedostoon.xlsx"; // Vaihda polku ja tiedostonimi tarpeen mukaan
            // string filePath = @"C:\reseptit\61066201.xlsx"; // "polku_tiedostoon.xlsx"; // Vaihda polku ja tiedostonimi tarpeen mukaan
            // string filePath = @"C:\reseptit\book1.xlsx"; // "polku_tiedostoon.xlsx"; // Vaihda polku ja tiedostonimi tarpeen mukaan
            //string filePath = @"C:\reseptit\book1.xlsx"; // "polku_tiedostoon.xlsx"; // Vaihda polku ja tiedostonimi tarpeen mukaan
            string databasePath = "Recipes.db";
            

            using (SQLiteConnection connection = new SQLiteConnection("Data Source=" + databasePath))
            {
                connection.Open();

                // Suorita SQL-kysely ja hae tiedostopolku SQLite-tietokannasta
                 string query = "SELECT FilePath FROM Recipes WHERE Koodi = @id";
               // string q2 = ".xlsx";
               // string q1 = @"C:\reseptit\" + @id + ".xlsx";
               // string query = q1;
                SQLiteCommand command = new SQLiteCommand(query, connection);
                command.Parameters.AddWithValue("@id", id);
                string filePath = (string)command.ExecuteScalar();

                 

                // Käytä filePath muuttujaa haluamallasi tavalla, esim. avaa Excel-tiedosto
                if (!string.IsNullOrEmpty(filePath))
                {

                    // filepath db  @"C:\reseptit\61066201.xlsx"
                    // Tee jotain tiedostopolulle, esim. avaa Excel-tiedosto
                    // esim. Process.Start(filePath);

                    

                    using (var package = new ExcelPackage(new FileInfo(filePath)))
                    {
                        // var worksheet = package.Workbook.Worksheets["Tuotanto (5)"]; // Oletetaan, että taulukko on ensimmäisellä välilehdellä
                        // ExcelWorksheet worksheet = package.Workbook.Worksheets[package.Workbook.Worksheets.Count];
                        // var worksheet = package.Workbook.Worksheets[package.Workbook.Worksheets.Count - 1];
                        var worksheet = package.Workbook.Worksheets[0];
                        // ExcelWorksheet worksheet = package.Workbook.Worksheets.First();
                        /* var currentSheet = package.Workbook.Worksheets;
                         var worksheet = currentSheet.FirstOrDefault(); */

                        // Suorita SQL-kysely ja hae tiedostopolku SQLite-tietokannasta


                         int rowCount = worksheet.Dimension.Rows - 7;
                        // int colCount = worksheet.Dimension.Columns;
                       // int rowCount = 36;
                        int colCount = 10;

                        List<List<string>> data = new List<List<string>>();

                        for (int row = 1; row <= rowCount; row++)
                        {
                            List<string> rowData = new List<string>();

                            for (int col = 1; col <= colCount; col++)
                            {
                                if (col != 4 && col != 5 && col != 6 && col != 7)
                                {
                                    rowData.Add(worksheet.Cells[row, col].Value?.ToString() ?? string.Empty);
                                }
                            }

                            data.Add(rowData);
                        }

                        return View(data);
                    }
                }

                return View(); 
            }

            
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}