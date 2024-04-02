using Microsoft.AspNetCore.Mvc;
using RefRecipe.Data;
using RefRecipe.Models;
using System.Diagnostics;
using OfficeOpenXml;
using System.Linq;
using Microsoft.Data.Sqlite;
using System.Data.SQLite;
using System.Data;
using Microsoft.CodeAnalysis.VisualBasic.Syntax;
using System.Data.Entity.Core.Common.CommandTrees.ExpressionBuilder;
using Serilog;

namespace RefRecipe.Controllers
{
    public class MaterialsController : Controller
    {

        private readonly RefRecipeContext _context;
        public MaterialsController(RefRecipeContext context)
        {
            _context = context;
            // ExcelPackage.LicenseContext = LicenseContext.Default;
        }

         public IList<Material> Material { get; set; } = default!;
       // public IList<Recipe> Recipe { get; set; } = default!;
        public IActionResult Index(string SearchText2 = "", string SearchCode2 = "")
        {
            
            string materialColor1 = "materialColor1";
			string materialColor2 = "materialColor2";
			var colorEntity = _context.Colors.FirstOrDefault(p => p.MaterialColor == materialColor1)?.Colorname;
			var colorEntity2 = _context.Colors.FirstOrDefault(p => p.MaterialColor == materialColor2)?.Colorname;
			ViewBag.materialColor1 = colorEntity;
			ViewBag.materialColor2 = colorEntity2;

			List<Material> materials;
            if (SearchText2 != "" && SearchText2 != null)
            {

#pragma warning disable CS8602 // Dereference of a possibly null reference.
              //  recipes = _context.Recipes.Where(p => p.Nimi.Contains(SearchText)).ToList();
              materials = _context.Materials.Where(p => p.Name.Contains(SearchText2)).ToList();
#pragma warning restore CS8602 // Dereference of a possibly null reference.

            }
            else if (SearchCode2 != "" && SearchCode2 != null)
            {
#pragma warning disable CS8602 // Dereference of a possibly null reference.
               // recipes = _context.Recipes.Where(p => p.Koodi.Contains(SearchCode)).ToList();
               materials = _context.Materials.Where(p => p.SapCode.Contains(SearchCode2)).ToList();
#pragma warning restore CS8602 // Dereference of a possibly null reference.
            }
            else
            {
               // recipes = _context.Recipes.OrderBy(p => p.Koodi).ToList();
               materials = _context.Materials.OrderBy(p => p.SapCode).ToList();
            }


            return View(materials);


        }
    

        public ActionResult Create()
        {
            return View();
        }
        public Material Materials { get; set; } = default!;

        

        public async Task<IActionResult> Delete(int? id)
        {


            if (id == null || _context.Materials == null)
            {
                return NotFound();
            }

            var material = await _context.Materials.FindAsync(id);
            if (material == null)
            {
                return NotFound();
            }
            return View(material);
            // return RedirectToAction("Index", "Home");


        }



        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.Materials == null)
            {
                return NotFound();
            }

            var material = await _context.Materials.FindAsync(id);
            if (material == null)
            {
                return NotFound();
            }
            return View(material);
        }

       
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(Material material, string password)
        {
            if (string.IsNullOrEmpty(password))
            {
                ViewBag.ErrorMessage = "Anna Salasana";
                return View();
            }
            if (ModelState.IsValid && password != null && password == "22")
            {
                _context.Add(material);
                await _context.SaveChangesAsync();
                Log.Information("Material Created");
                return RedirectToAction("Index", "Materials");
            }
            else
            {
                // Väärä salasana
                ViewBag.ErrorMessage = "VIRHEELLINEN SALASANA";
                return View();
            }
           // return View(material);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, Material material, string password)
        {
            if (string.IsNullOrEmpty(password))
            {
                ViewBag.ErrorMessage = "Anna salasana";
                return View();
            }
            if (password != null && password == "22")
            {
                _context.Update(material);
                await _context.SaveChangesAsync();
                // return View(recipe);
                Log.Information("Material Edited");
                return RedirectToAction("Index", "Materials");
            }
            else
            {
                // Väärä salasana
                ViewBag.ErrorMessage = "VIRHEELLINEN SALASANA";
                return View();

            }

        }
       
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Delete(int id, Material material, string password)
        {
           // var material = await _context.Materials.FindAsync(id);
            if (string.IsNullOrEmpty(password))
            {
                ViewBag.ErrorMessage = "Anna salasana";
                return View();
            }
            
            if (material != null && password != null && password == "22")
            {
               // _context.Materials.Remove(material);
               _context.Remove(material);
                await _context.SaveChangesAsync();
                Log.Information("Material Deleted");
                return RedirectToAction("Index", "Materials");
            }
            else
            {
                // Väärä salasana
                ViewBag.ErrorMessage = "VIRHEELLINEN SALASANA";
                return View();
            }

           // await _context.SaveChangesAsync();
            // return RedirectToAction(nameof(Index));
           // return RedirectToAction("Index", "Materials");
        }

       

       /* public ActionResult RecipeMaterials2COLL(string id)
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
                            Debug.WriteLine(filePath + " 6666666666666666666666666666666666666666666666666");
                            string eka = @"C:\reseptit\";
                            string toka = ".xlsx";
                            string filePath2 = eka + filePath + toka;


                            using (var package = new ExcelPackage(new FileInfo(filePath2)))
                            {
                                var worksheet = package.Workbook.Worksheets[0];

                                int rowCount = 18;
                                int colCount = 2;

                                List<List<string>> data = new List<List<string>>();
                                List<string> codeData = new List<string>();

                                for (int row = 1; row <= rowCount; row++)
                                {
                                    List<string> rowData = new List<string>();
                                    // List<string> codeData = new List<string>();

                                    for (int col = 1; col <= colCount; col++)
                                    {
                                        var cellValue = worksheet.Cells[row, col].Value?.ToString();

                                        if (row >= 9)
                                        {
                                            // rowData.Add(worksheet.Cells[row, col].Value?.ToString() ?? string.Empty);
                                            rowData.Add(cellValue);
                                        }
                                        if (row <= 3)
                                        {
                                            // rowData.Add(worksheet.Cells[row, col].Value?.ToString() ?? string.Empty);
                                            rowData.Add(cellValue);
                                        }
                                       

                                    }

                                    data.Add(rowData);
                                }
                                //  ViewBag.Data = data;
                                // ViewBag.CodeData = codeData;
                                // return View();
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
                Console.WriteLine($"Virhe: {ex.Message}");
                // Voit tehdä tässä jotain virhetilanteessa, esimerkiksi näyttää virhesivun
                // return View("Error");
                ViewBag.ErrorMessage = "Reseptiä ei löydy.";
                return RedirectToAction("AuthIndex", "Home");
            }

            
        } */

        public ActionResult RecipeMaterials(string id)
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
                            Debug.WriteLine(filePath + " 6666666666666666666666666666666666666666666666666");
                            string eka = @"C:\reseptit\";
                            string toka = ".xlsx";
                            string filePath2 = eka + filePath + toka;


                            using (var package = new ExcelPackage(new FileInfo(filePath2)))
                            {
                                var worksheet = package.Workbook.Worksheets[0];

                                int rowCount = 20;
                                int colCount = 9;

                                List<List<string>> data = new List<List<string>>();
                                List<string> codeData = new List<string>();

                                for (int row = 1; row <= rowCount; row++)
                                {
                                    List<string> rowData = new List<string>();
                                    // List<string> codeData = new List<string>();

                                    for (int col = 1; col <= colCount; col++)
                                    {
                                        var cellValue = worksheet.Cells[row, col].Value?.ToString();

                                        if ((col == 1) && (row >= 11))
                                        {
                                        // rowData.Add(worksheet.Cells[row, col].Value?.ToString() ?? string.Empty);
                                            rowData.Add(cellValue);
                                        }
                                        else if ((col == 1) && (row <= 3 || row == 10))
                                        {
                                            rowData.Add(cellValue);
                                        }
                                        else if ((col == 2) && (row >= 11 || row <= 3 || row == 10))
                                        {
                                            // rowData.Add(worksheet.Cells[row, col].Value?.ToString() ?? string.Empty);
                                            rowData.Add(cellValue);
                                        }

                                       /* else if ((col == 9) && (row >= 10))
                                        {
                                            rowData.Add(cellValue);
                                        } */
                                        /* if (row <= 3)
                                         {
                                             // rowData.Add(worksheet.Cells[row, col].Value?.ToString() ?? string.Empty);
                                             rowData.Add(cellValue);
                                         }
                                         if (row == 10)
                                         {
                                             // rowData.Add(worksheet.Cells[row, col].Value?.ToString() ?? string.Empty);
                                             rowData.Add(cellValue);
                                         }
                                         /* if (col == 1 && rowCount >= 11 && rowCount <= 20)
                                          {
                                              codeData.Add(cellValue);

                                          } */
                                    }

                                    data.Add(rowData);
                                }
                                //  ViewBag.Data = data;
                                // ViewBag.CodeData = codeData;
                                // return View();
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
                Console.WriteLine($"Virhe: {ex.Message}");
                // Voit tehdä tässä jotain virhetilanteessa, esimerkiksi näyttää virhesivun
                // return View("Error");
                ViewBag.ErrorMessage = "Reseptiä ei löydy.";
                return RedirectToAction("AuthIndex", "Home");
            }


        }

        public ActionResult RecipeMaterialsAlkuperäinen(string id)
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
                            Debug.WriteLine(filePath + " 6666666666666666666666666666666666666666666666666");
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

                                for (int row = 1; row <= rowCount; row++)
                                {
                                    List<string> rowData = new List<string>();
                                    // List<string> codeData = new List<string>();

                                    for (int col = 1; col <= colCount; col++)
                                    {
                                        var cellValue = worksheet.Cells[row, col].Value?.ToString();

                                        if (col != 4 && col != 5 && col != 6 && col != 7)
                                        {
                                            // rowData.Add(worksheet.Cells[row, col].Value?.ToString() ?? string.Empty);
                                            rowData.Add(cellValue);
                                        }
                                        if (col == 1 && rowCount >= 11 && rowCount <= 20)
                                        {
                                            codeData.Add(cellValue);

                                        }
                                    }

                                    data.Add(rowData);
                                }
                                //  ViewBag.Data = data;
                                // ViewBag.CodeData = codeData;
                                // return View();
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
                Console.WriteLine($"Virhe: {ex.Message}");
                // Voit tehdä tässä jotain virhetilanteessa, esimerkiksi näyttää virhesivun
                // return View("Error");
                ViewBag.ErrorMessage = "Reseptiä ei löydy.";
                return RedirectToAction("AuthIndex", "Home");
            }


        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
