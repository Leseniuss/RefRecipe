using Microsoft.AspNetCore.Mvc;
using RefRecipe.Data;
using RefRecipe.Models;
using System.Diagnostics;
using OfficeOpenXml;
using System.Linq;
using Microsoft.Data.Sqlite;
using System.Data.SQLite;
using System.Data;

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

        /*  public ActionResult ReadExcel2(string id)  // EPP HAKEE VAAN .xlsx excel tiedostoja
          {

              string databasePath = "Recipes.db";


              using (SQLiteConnection connection = new SQLiteConnection("Data Source=" + databasePath))
              {
                  connection.Open();

                 string query = "SELECT FilePath FROM Recipes WHERE Koodi = @id";

                  SQLiteCommand command = new SQLiteCommand(query, connection);
                  command.Parameters.AddWithValue("@id", id);
                  string filePath = (string)command.ExecuteScalar();




                 // if (!string.IsNullOrEmpty(filePath))
                      if (!string.IsNullOrEmpty(filePath) && Path.GetExtension(filePath) == ".xlsx")
                      {





                       using (var package = new ExcelPackage(new FileInfo(filePath)))
                     // if (!string.IsNullOrEmpty(filePath) && Path.GetExtension(filePath) == ".xlsx")
                      {

                          var worksheet = package.Workbook.Worksheets[0];

                           int rowCount = worksheet.Dimension.Rows - 8;
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


          } */

        public ActionResult ReadExcel2(string id)
        {
            string databasePath = "Recipes.db";

            try
            {
                using (SQLiteConnection connection = new SQLiteConnection("Data Source=" + databasePath))
                {
                    connection.Open();

                    string query = "SELECT FilePath FROM Recipes WHERE Koodi = @id";

                    using (SQLiteCommand command = new SQLiteCommand(query, connection))
                    {
                        command.Parameters.Add(new SQLiteParameter("@id", DbType.String) { Value = id });
                        string filePath = (string)command.ExecuteScalar();

                        if (!string.IsNullOrEmpty(filePath))
                        {
                            using (var package = new ExcelPackage(new FileInfo(filePath)))
                            {
                                var worksheet = package.Workbook.Worksheets[0];

                                int rowCount = worksheet.Dimension.Rows - 8;
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
                        else
                        {
                            // Tässä reseptiä ei löydy, asetetaan viesti ViewBagiin
                            ViewBag.ErrorMessage = "Reseptiä ei löydy.";
                            return View();
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Virhe: {ex.Message}");
                // Voit tehdä tässä jotain virhetilanteessa, esimerkiksi näyttää virhesivun
                // return View("Error");
                ViewBag.ErrorMessage = "Reseptiä ei löydy.";
                return RedirectToAction("Index", "Home");
            }

            // Jos ei tapahtunut virhettä, palaa tyhjällä datalla
           // return View(new List<List<string>>());
        }


        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}