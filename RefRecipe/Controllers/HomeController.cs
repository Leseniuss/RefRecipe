using Microsoft.AspNetCore.Mvc;
using RefRecipe.Data;
using RefRecipe.Models;
using System.Diagnostics;
using OfficeOpenXml;
using System.Linq;
using Microsoft.Data.Sqlite;
using System.Data.SQLite;
using System.Data;
using Microsoft.CodeAnalysis.CodeMetrics;
using System.Data.Entity;
using Microsoft.EntityFrameworkCore;
using Serilog;

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

        

        public IList<Password> Passwords { get; set; } = default!;

        public ActionResult Index(string password)
        {
            

            if (string.IsNullOrEmpty(password))
            {
                ViewBag.ErrorMessage = "Anna salasana";
               // Log.Information("Salasana Kenttä Tyhjä");
                return View();
            }
            else
            {

               
                // Etsi salasana tietokannasta

                string? passwords = _context.Passwords.FirstOrDefault(u => u.Passu == password)?.Passu;
               // string? passwordValue = passwords?.ToString();
               // Debug.WriteLine(passwordValue + " 4444444444444444444");
                Debug.WriteLine(passwords + " 666666666666666666666666");

                if (passwords != null && passwords == "11")
                {

                    // Oikea salasana, ohjaa AuthIndex
                    Log.Warning("Ohjataan Authindex");
                     return RedirectToAction("AuthIndex");
                    
                   // return RedirectToAction("Auth2");
                }
                if (passwords != null && passwords == "22")
                {
                    // Oikea salasana, ohjaa AuthIndex2
                    //  return RedirectToAction("Auth2");
                    //  C: \Users\Jyri Leskinen\source\repos\RefRecipe\RefRecipe\Views\Home\AuthIndex2.cshtml
                    // return Redirect("Perkele");
                    Log.Warning("Ohjataan Authindex2");
                    return RedirectToAction("AuthIndex2");
                 //  return RedirectToAction("localhost:5013/Home/AuthIndex2");


                } 
                else
                {
                    // Väärä salasana
                    ViewBag.ErrorMessage = "Virheellinen salasana";
                    Log.Information("Väärä salasana");
                    return View();

                }
            }
        }

        public ActionResult AuthenticatedIndex()
        {
            return View();
        }
    


    public IActionResult AuthIndex(string SearchText = "", string SearchCode = "")
        {

            List<Recipe> recipes;
            if (SearchText != "" && SearchText != null)
            {

#pragma warning disable CS8602 // Dereference of a possibly null reference.
                recipes = _context.Recipes.Where(p => p.Nimi.Contains(SearchText)).ToList();
#pragma warning restore CS8602 // Dereference of a possibly null reference.

            }
            else if (SearchCode != "" && SearchCode != null)
            {
#pragma warning disable CS8602 // Dereference of a possibly null reference.
                recipes = _context.Recipes.Where(p => p.Koodi.Contains(SearchCode)).ToList();
#pragma warning restore CS8602 // Dereference of a possibly null reference.
            }
            else
            {
                recipes = _context.Recipes.OrderBy(p => p.Koodi).ToList();
            }


                return View(recipes);


            }

        public IActionResult AuthIndex2(string SearchText = "", string SearchCode = "")
        {

            List<Recipe> recipes;
            if (SearchText != "" && SearchText != null)
            {

#pragma warning disable CS8602 // Dereference of a possibly null reference.
                recipes = _context.Recipes.Where(p => p.Nimi.Contains(SearchText)).ToList();
#pragma warning restore CS8602 // Dereference of a possibly null reference.

            }
            else if (SearchCode != "" && SearchCode != null)
            {
#pragma warning disable CS8602 // Dereference of a possibly null reference.
                recipes = _context.Recipes.Where(p => p.Koodi.Contains(SearchCode)).ToList();
#pragma warning restore CS8602 // Dereference of a possibly null reference.
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





        /*  public ActionResult ReadExcel2(string id)
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
          } */

        public ActionResult ReadExcel(string id)
        {
            string databasePath = "Recipes.db";

            try
            {
                using (SQLiteConnection connection = new SQLiteConnection("Data Source=" + databasePath))
                {
                    connection.Open();

                    // string baseFolderPath = @"c:\reseptit\";
                   //  string query = "SELECT FilePath FROM Recipes WHERE Koodi = @id";
                    string query = "SELECT Koodi FROM Recipes WHERE Koodi = @id";
                    // string query3 = @"C:\reseptit\";
                    // string query4 = ".xlsx";
                    //string query22 = query3 + query2 + query4;
                    Debug.WriteLine(query + " 44444444444444444444444444444444444");
                    // C:\reseptit\SELECT Koodi FROM Recipes WHERE Koodi = @id.xlsx 44444444444444444444444444444444444

                    using (SQLiteCommand command = new SQLiteCommand(query, connection))
                    {
                        command.Parameters.Add(new SQLiteParameter("@id", DbType.String) { Value = id });
                        string filePath = (string)command.ExecuteScalar();
                        

                        if (!string.IsNullOrEmpty(filePath))
                        {
                           
                            string eka = @"C:\reseptit\";
                            string toka = ".xlsx";
                            string filePath2 = eka + filePath + toka;


                            using (var package = new ExcelPackage(new FileInfo(filePath2)))
                            {
                                var worksheet = package.Workbook.Worksheets[0];

                                int rowCount = worksheet.Dimension.Rows - 8;
                                int colCount = 10;

                                List<List<string>> data = new List<List<string>>();
                                List<string> codeData = new List<string>();
                                List<string> codes = new List<string>();

                                for (int row = 1; row <= rowCount; row++)
                                {
                                    List<string> rowData = new List<string>();
                                    // List<string> codeData = new List<string>();
                                    var rowmi = row - 11;
                                   

                                    for (int col = 1; col <= colCount; col++)
                                    {
                                        var cellValue = worksheet.Cells[row, col].Value?.ToString();

                                        if (col != 4 && col != 5 && col != 6 && col != 7)
                                        {
                                            // rowData.Add(worksheet.Cells[row, col].Value?.ToString() ?? string.Empty);
                                            rowData.Add(cellValue);
                                            Debug.WriteLine(cellValue + "  CCCEEEEEELLLLLLVAAAAAALLLLUUUEEE");

                                        }
                                        if ((col == 1) && row >= 11 && row <= 20)
                                        {
                                            codes.Add(cellValue);
                                        }
                                        if ((col == 10) && row >= 11 && row <= 20)
                                        {
                                            // rowData.Add(codes[rowmi]);
                                            string locationQuery = "SELECT Location FROM Materials WHERE SapCode = @id";
                                            using (SQLiteCommand locationCommand = new SQLiteCommand(locationQuery, connection))
                                            {
                                                locationCommand.Parameters.Add(new SQLiteParameter("@id", DbType.String) { Value = codes[rowmi] });
                                                string location = (string)locationCommand.ExecuteScalar();
                                                rowData.Add(location);
                                            }
                                        }




                                    }

                                    data.Add(rowData);
                                   
                                }
                                //  ViewBag.Data = data;
                                // ViewBag.CodeData = codeData;
                                // return View();
                                Log.Information("ReadExcel {filePath}", filePath);
                                return View(data); 
                                //  Console.WriteLine(codeData[0] + " 121212121212121212121212121121212");
                               // Debug.WriteLine(codeData[0] + " 121212121212121212121212121121212");
                               // var tupleModel = new Tuple<List<List<string>>, List<string>>(data, codeData);
                                
                               // return View(tupleModel);
                                
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
                string errorMessage = "Reseptiä ei löydy. Sulje tämä välilehti. ";
                return Content(errorMessage);
               // return RedirectToAction("AuthIndex", "Home");
            }

          
        }

        public ActionResult ReadExcel2(string id)
        {
            string databasePath = "Recipes.db";

            try
            {
                using (SQLiteConnection connection = new SQLiteConnection("Data Source=" + databasePath))
                {
                    connection.Open();

                    // string baseFolderPath = @"c:\reseptit\";
                    //  string query = "SELECT FilePath FROM Recipes WHERE Koodi = @id";
                    string query = "SELECT Koodi FROM Recipes WHERE Koodi = @id";
                    // string query3 = @"C:\reseptit\";
                    // string query4 = ".xlsx";
                    //string query22 = query3 + query2 + query4;
                    Debug.WriteLine(query + " 44444444444444444444444444444444444");
                    // C:\reseptit\SELECT Koodi FROM Recipes WHERE Koodi = @id.xlsx 44444444444444444444444444444444444

                    using (SQLiteCommand command = new SQLiteCommand(query, connection))
                    {
                        command.Parameters.Add(new SQLiteParameter("@id", DbType.String) { Value = id });
                        string filePath = (string)command.ExecuteScalar();


                        if (!string.IsNullOrEmpty(filePath))
                        {
                           
                            string eka = @"C:\reseptit\";
                            string toka = ".xlsx";
                            string filePath2 = eka + filePath + toka;


                            using (var package = new ExcelPackage(new FileInfo(filePath2)))
                            {
                                var worksheet = package.Workbook.Worksheets[0];

                                int rowCount = worksheet.Dimension.Rows - 8;
                                int colCount = 10;

                                List<List<string>> data = new List<List<string>>();
                                List<string> codeData = new List<string>();
                                List<string> codes = new List<string>();

                                for (int row = 1; row <= rowCount; row++)
                                {
                                    List<string> rowData = new List<string>();
                                    // List<string> codeData = new List<string>();
                                    var rowmi = row - 11;

                                    for (int col = 1; col <= colCount; col++)
                                    {
                                        var cellValue = worksheet.Cells[row, col].Value?.ToString();

                                        if (col != 4 && col != 5 && col != 6 && col != 7)
                                        {
                                            
                                            rowData.Add(cellValue);
                                        }
                                        if (col == 1 && row >= 11 && row <= 20)
                                        {
                                            codes.Add(cellValue);

                                        }
                                        if ((col == 10) && row >= 11 && row <= 20)
                                        {
                                            // rowData.Add(codes[rowmi]);
                                            string locationQuery = "SELECT Location FROM Materials WHERE SapCode = @id";
                                            using (SQLiteCommand locationCommand = new SQLiteCommand(locationQuery, connection))
                                            {
                                                locationCommand.Parameters.Add(new SQLiteParameter("@id", DbType.String) { Value = codes[rowmi] });
                                                string location = (string)locationCommand.ExecuteScalar();
                                                rowData.Add(location);
                                            }
                                        }


                                    }

                                    data.Add(rowData);
                                }
                                //  ViewBag.Data = data;
                                // ViewBag.CodeData = codeData;
                                // return View();
                                Log.Information("ReadExcel2 {filePath}", filePath);
                                return View(data);
                                //  Console.WriteLine(codeData[0] + " 121212121212121212121212121121212");
                                // Debug.WriteLine(codeData[0] + " 121212121212121212121212121121212");
                                // var tupleModel = new Tuple<List<List<string>>, List<string>>(data, codeData);

                                // return View(tupleModel);

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
                string errorMessage = "Reseptiä ei löydy. Sulje tämä välilehti. ";
                return Content(errorMessage);
               // ViewBag.ErrorMessage = "Reseptiä ei löydy.";
               // return RedirectToAction("AuthIndex2", "Home");
               // return View();
            }

           
        }

        

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}