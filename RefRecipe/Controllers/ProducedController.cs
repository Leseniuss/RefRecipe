using DocumentFormat.OpenXml.Office2010.Excel;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using RefRecipe.Data;
using Serilog;

namespace RefRecipe.Controllers
{
    public class ProducedController : Controller
    {
        private readonly RefRecipeContext _context;

        public ProducedController(RefRecipeContext context)
        {
            _context = context;
            
        }

       

        public async Task<IActionResult> Index(DateTime startdate, DateTime enddate)
        {
            List<Produce> Produce;

            // TempData["volume"] = filePath;
            var Code22 = TempData["filePathsave2"] as string;

            int startdate2 = (int)startdate.Subtract(new DateTime(1970, 1, 1)).TotalSeconds;
            int enddate2 = (int)enddate.Subtract(new DateTime(1970, 1, 1)).TotalSeconds;
            if (startdate2 >= 0 && enddate2 >= 0)
            {
                Produce = await _context.Produced.Where(p => p.Timestamp >= startdate2 && p.Timestamp <= enddate2).ToListAsync();

                var koodit = await _context.Produced.Where(p => p.Timestamp >= startdate2 && p.Timestamp <= enddate2).Select(p => p.SapCode).ToListAsync();

                int totalSatsikoko = 0;
                foreach (var item in koodit)
                {
                    var satsikokoSum = await _context.Recipes
                        .Where(p => p.Koodi == item)
                        .SumAsync(p => p.Satsikoko);

                    totalSatsikoko += satsikokoSum;
                }

                TempData["volume"] = totalSatsikoko;

                // Log.Information(startdate2 + "  SSTTTAAARTTTDAAAATTEE222222222222222222222222222");
                // Log.Information(enddate2 + "  EEEEEEEEENNNNNNDDDDD222222222222222222222222222");

                return View(Produce);
                
            }
            if (startdate2 >= 0 && enddate2 <= 0)
            {
                Produce = await _context.Produced.Where(p => p.Timestamp >= startdate2).ToListAsync();

                var koodit = await _context.Produced.Where(p => p.Timestamp >= startdate2).Select(p => p.SapCode).ToListAsync();

                int totalSatsikoko = 0;
                foreach (var item in koodit)
                {
                    var satsikokoSum = await _context.Recipes
                        .Where(p => p.Koodi == item)
                        .SumAsync(p => p.Satsikoko);

                    totalSatsikoko += satsikokoSum;
                }

                TempData["volume"] = totalSatsikoko;

                // Log.Information(startdate2 + "  SSTTTAAARTTTDAAAATTEE222222222222222222222222222");
                // Log.Information(enddate2 + "  EEEEEEEEENNNNNNDDDDD222222222222222222222222222");

                return View(Produce);

            }
            if (startdate2 <= 0 && enddate2 >= 0)
            {
                Produce = await _context.Produced.Where(p => p.Timestamp <= enddate2).ToListAsync();

                var koodit = await _context.Produced.Where(p => p.Timestamp <= enddate2).Select(p => p.SapCode).ToListAsync();

                int totalSatsikoko = 0;
                foreach (var item in koodit)
                {
                    var satsikokoSum = await _context.Recipes
                        .Where(p => p.Koodi == item)
                        .SumAsync(p => p.Satsikoko);

                    totalSatsikoko += satsikokoSum;
                }

                TempData["volume"] = totalSatsikoko;

                // Log.Information(startdate2 + "  SSTTTAAARTTTDAAAATTEE222222222222222222222222222");
                // Log.Information(enddate2 + "  EEEEEEEEENNNNNNDDDDD222222222222222222222222222");

                return View(Produce);

            }
            else
            {
                Produce = _context.Produced.OrderBy(p => p.Id).ToList();

                var koodit = await _context.Produced.Select(p => p.SapCode).ToListAsync();
                // var groupedKoodit = koodit.GroupBy(x => x).ToDictionary(g => g.Key, g => g.Count());
                // int totalSatsikoko = await _context.Recipes.Where(r => groupedKoodit.ContainsKey(r.Koodi)).SumAsync(r => r.Satsikoko * groupedKoodit[r.Koodi]);

                // int totalSatsikoko = await _context.Recipes.Where(r => koodit.Contains(r.Koodi)).SumAsync(r => r.Satsikoko);

                int totalSatsikoko = 0;
                foreach (var item in koodit)
                {
                    var satsikokoSum = await _context.Recipes
                        .Where(p => p.Koodi == item)
                        .SumAsync(p => p.Satsikoko);

                    totalSatsikoko += satsikokoSum;
                }

                TempData["volume"] = totalSatsikoko;
                //  Log.Information(startdate2 + "  SSTTTAAARTTTDAAAATTEEelse222222222222222222222222222");
                //  Log.Information(enddate2 + "  EEEEEEEEENNNNNNDDDDDelse222222222222222222222222222");
                // var Code22 = TempData["filePathsave2"] as string;
                return View(Produce);
            }
            
           // return View(Produce);
        } 

        // GET: Produced/Details/5
        public ActionResult Details(int id)
        {
            return View();
        }

        // GET: Produced/Create
       /* public ActionResult Create()
        {
            return View();
        } */ 
        public async Task<IActionResult> Create()
        {
            var Code22 = TempData["filePathsave2"] as string;

            if (Code22 == null || _context.Recipes == null)
            {
                return NotFound();
            }

             var product = await _context.Recipes.FirstOrDefaultAsync(p => p.Koodi == Code22);
           // var product = await _context.Recipes.FindAsync(id);
            if (product == null)
            {
                return NotFound();
            }
            return View(product);
            // return RedirectToAction("Index", "Home");


        }
        public Produce produce { get; set; } = default!;

        // POST: Produced/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(Produce produce, bool myCheckbox)
        {
            string? filePath2 = TempData["filePathsave2"] as string;
            var nimi = await _context.Recipes.Where(r => r.Koodi == filePath2).Select(r => r.Nimi).FirstOrDefaultAsync();
            
            // var satsikoko = await _context.Recipes.Where(r => r.Koodi == filePath2).Select(r => r.Satsikoko).FirstOrDefaultAsync();



            if (produce != null)
            {
                DateTime localTime = DateTime.Now;
                DateTime utcTime = localTime.ToUniversalTime();
                int unixTime = (int)utcTime.Subtract(new DateTime(1970, 1, 1)).TotalSeconds;
                // Muuta tietoja ennen tallennusta
                produce.SapCode = TempData["filePathsave2"] as string;
                produce.Name = nimi;
                produce.Timestamp = unixTime;
               // produce.Volume = satsikoko;
               
                _context.Add(produce);
                await _context.SaveChangesAsync();
                return RedirectToAction("Index", "Produced");
            }

            else
            {
                return RedirectToAction("AuthIndex", "Home");
            }
                
              
            
           // return View(produce);
           
        }

        // GET: Produced/Edit/5
        public ActionResult Edit(int id)
        {
            return View();
        }

        // POST: Produced/Edit/5
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

        // GET: Produced/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: Produced/Delete/5
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
