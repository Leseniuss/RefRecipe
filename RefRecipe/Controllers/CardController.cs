using Microsoft.AspNetCore.Mvc;
using OfficeOpenXml;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium;
using Microsoft.Extensions.Hosting.Internal;
using PuppeteerSharp;



namespace RefRecipe.Controllers
{
    public class CardController : Controller
    {
        public async Task<IActionResult> TakeScreenshot()
        {
            try
            {
                var puppeteerExecutableRevision = "123.0.6312.59"; // Puppeteerin käytettävä versio

               
                var browserFetcherOptions = new BrowserFetcherOptions { Path = "puppeteer" };
                var browserFetcher = new BrowserFetcher(browserFetcherOptions);
                await browserFetcher.DownloadAsync(puppeteerExecutableRevision);

                var launchOptions = new LaunchOptions { Headless = true }; 
                using (var browser = await Puppeteer.LaunchAsync(launchOptions))
                using (var page = await browser.NewPageAsync())
                {
                   
                    await page.GoToAsync("https://www.google.com");

                  
                    var screenshotPath = Path.Combine(Directory.GetCurrentDirectory(), "screenshot.png");
                    await page.ScreenshotAsync(screenshotPath);

                   
                    return Content($"Screenshot saved: {screenshotPath}");
                }
            }
            catch (Exception ex)
            {
                return Content($"Error: {ex.Message}");
            }
        }

        public IActionResult Index2()
        {
            try
            {
                string filePath = @"C:\reseptit\Kortit\Mehukortti22.xlsx"; 
                ExcelReader excelReader = new ExcelReader();
                List<List<string>> excelData = excelReader.ReadExcel(filePath, 20);
                return View(excelData);
            }
            catch (Exception ex)
            {
               
                return View("Error", ex.Message);
            }
        }

        public ActionResult Index()
         {
             try
             {
                 // Excel-tiedoston polku
                 string filePath = @"c:\reseptit\Kortit\Mehukortti.xlsx";


                 if (!string.IsNullOrEmpty(filePath))
                 {

                     using (var package = new ExcelPackage(new FileInfo(filePath)))
                     {
                         // ExcelWorksheet worksheet = package.Workbook.Worksheets[0];
                         var worksheet = package.Workbook.Worksheets[2];
                         int rowCount = 28;
                         int colCount = 19;

                         // Luo lista, johon tallennetaan Excel-tiedoston tiedot
                         // var excelData = new List<List<string>>();
                         List<List<string>> excelData = new List<List<string>>();

                         // Käy läpi jokainen rivi ja lisää sen tiedot listaan
                         for (int row = 1; row <= rowCount; row++)
                         {
                             // var rowData = new List<string>();
                             List<string> rowData = new List<string>();

                             for (int col = 1; col <= colCount; col++)
                             {
                                 var cellValue = worksheet.Cells[row, col].Value;
                                 if (cellValue != null)
                                 {
                                     rowData.Add(cellValue.ToString());
                                 }
                                 else
                                 {
                                     rowData.Add(string.Empty);
                                 }
                             }



                             // Lisää rivin tiedot päälistalle
                             excelData.Add(rowData);
                         }

                         // Palauta näkymä, joka näyttää Excel-tiedoston tiedot
                         return View(excelData);
                     }
                 }
                 else
                 {
                     return View();
                 }
             }

             catch (Exception ex)
             {
                 Console.WriteLine($"Virhe: {ex.Message}");
                 // Voit tehdä tässä jotain virhetilanteessa, esimerkiksi näyttää virhesivun
                 // return View("Error");
                 // ViewBag.ErrorMessage = "Reseptiä ei löydy.";
                 return RedirectToAction("AuthIndex", "Home");
             }
         } 

        /* public ActionResult Index2()
         {
             try
             {
                 string filePath = @"c:\reseptit\Mehukortti.xlsx";

                 if (!string.IsNullOrEmpty(filePath))
                 {
                     using (var package = new ExcelPackage(new FileInfo(filePath)))
                     {
                         var worksheet = package.Workbook.Worksheets[2];
                         var mergedCells = worksheet.MergedCells;
                         foreach (var mergedCell in mergedCells)
                         {
                             var cell = worksheet.Cells[mergedCell];
                             var range = worksheet.Cells[cell.Start.Row, cell.Start.Column, cell.End.Row, cell.End.Column];
                             range.Merge = true; // Voit poistaa yhdistetyt solut asettamalla tämän arvon false:ksi
                         }

                         // Luo lista, johon tallennetaan Excel-tiedoston tiedot
                         List<List<string>> excelData = new List<List<string>>();

                         // Käy läpi jokainen rivi ja lisää sen tiedot listaan
                         for (int row = 1; row <= worksheet.Dimension.End.Row; row++)
                         {
                             List<string> rowData = new List<string>();

                             for (int col = 1; col <= worksheet.Dimension.End.Column; col++)
                             {
                                 var cellValue = worksheet.Cells[row, col].Value;
                                 //rowData.Add(cellValue);
                                 if (cellValue != null)
                                 {
                                     rowData.Add(cellValue.ToString());
                                 }
                                 else
                                 {
                                     rowData.Add(string.Empty);
                                 }
                             }

                             excelData.Add(rowData);
                         }

                         return View(excelData);
                     }
                 }
                 else
                 {
                     return View();
                 }
             }
             catch (Exception ex)
             {
                 Console.WriteLine($"Virhe:66666666666666666666666666666 {ex.Message}");
                 Console.WriteLine($"Stack Trace: {ex.StackTrace}"); 
                 Console.WriteLine(ex.Message);
                 return RedirectToAction("AuthIndex", "Home");
             }
         } */




        // GET: CardController
        /* public ActionResult Index()
         {
             return View();
         } */

        // GET: CardController/Details/5
        public ActionResult Details(int id)
        {
            return View();
        }

        // GET: CardController/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: CardController/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(IFormCollection collection)
        {
            try
            {
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }

        // GET: CardController/Edit/5
        public ActionResult Edit(int id)
        {
            return View();
        }

        // POST: CardController/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(int id, IFormCollection collection)
        {
            try
            {
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }

        // GET: CardController/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: CardController/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Delete(int id, IFormCollection collection)
        {
            try
            {
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }
    }


}


