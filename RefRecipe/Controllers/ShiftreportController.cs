using Microsoft.AspNetCore.Mvc;
using RefRecipe.Data;
using Microsoft.EntityFrameworkCore;

namespace RefRecipe.Controllers
{
    public class ShiftreportController : Controller
	{

        private readonly RefRecipeContext _context;

        public ShiftreportController(RefRecipeContext context)
        {
            _context = context;

        }

        public async Task<IActionResult> Index(DateTime startdate, DateTime enddate)
        {
            string produceColor1 = "produceColor1";
            string produceColor2 = "produceColor2";
            var colorEntity = _context.Colors.FirstOrDefault(p => p.MaterialColor == produceColor1)?.Colorname;
            var colorEntity2 = _context.Colors.FirstOrDefault(p => p.MaterialColor == produceColor2)?.Colorname;
            ViewBag.produceColor1 = colorEntity;
            ViewBag.produceColor2 = colorEntity2;

            List<Shiftreport> Shiftreport;

            // TempData["volume"] = filePath;
            var Code22 = TempData["filePathsave2"] as string;

            int startdate2 = (int)startdate.Subtract(new DateTime(1970, 1, 1)).TotalSeconds;
            int enddate2 = (int)enddate.Subtract(new DateTime(1970, 1, 1)).TotalSeconds;
            if (startdate2 >= 0 && enddate2 >= 0)
            {
                Shiftreport = await _context.Shiftreports.Where(p => p.Timestamp >= startdate2 && p.Timestamp <= enddate2).ToListAsync();

               // var koodit = await _context.Shiftreports.Where(p => p.Timestamp >= startdate2 && p.Timestamp <= enddate2).Select(p => p.Id).ToListAsync();

               // int totalSatsikoko = 0;
               /* foreach (var item in koodit)
                {
                    var satsikokoSum = await _context.Recipes
                        .Where(p => p.Id == item)
                        .SumAsync(p => p.Satsikoko);

                    totalSatsikoko += satsikokoSum;
                } */

               // TempData["volume"] = totalSatsikoko;

                // Log.Information(startdate2 + "  SSTTTAAARTTTDAAAATTEE222222222222222222222222222");
                // Log.Information(enddate2 + "  EEEEEEEEENNNNNNDDDDD222222222222222222222222222");

                return View(Shiftreport);

            }
            if (startdate2 >= 0 && enddate2 <= 0)
            {
                Shiftreport = await _context.Shiftreports.Where(p => p.Timestamp >= startdate2).ToListAsync();

               // var koodit = await _context.Produced.Where(p => p.Timestamp >= startdate2).Select(p => p.SapCode).ToListAsync();

               /* int totalSatsikoko = 0;
                foreach (var item in koodit)
                {
                    var satsikokoSum = await _context.Recipes
                        .Where(p => p.Koodi == item)
                        .SumAsync(p => p.Satsikoko);

                    totalSatsikoko += satsikokoSum;
                }

                TempData["volume"] = totalSatsikoko; */

                // Log.Information(startdate2 + "  SSTTTAAARTTTDAAAATTEE222222222222222222222222222");
                // Log.Information(enddate2 + "  EEEEEEEEENNNNNNDDDDD222222222222222222222222222");

                return View(Shiftreport);

            }
            if (startdate2 <= 0 && enddate2 >= 0)
            {
                Shiftreport = await _context.Shiftreports.Where(p => p.Timestamp <= enddate2).ToListAsync();

               /* var koodit = await _context.Produced.Where(p => p.Timestamp <= enddate2).Select(p => p.SapCode).ToListAsync();

                int totalSatsikoko = 0;
                foreach (var item in koodit)
                {
                    var satsikokoSum = await _context.Recipes
                        .Where(p => p.Koodi == item)
                        .SumAsync(p => p.Satsikoko);

                    totalSatsikoko += satsikokoSum;
                }

                TempData["volume"] = totalSatsikoko; */

                // Log.Information(startdate2 + "  SSTTTAAARTTTDAAAATTEE222222222222222222222222222");
                // Log.Information(enddate2 + "  EEEEEEEEENNNNNNDDDDD222222222222222222222222222");

                return View(Shiftreport);

            }
            else
            {
                Shiftreport = _context.Shiftreports.OrderBy(p => p.Id).ToList();

               /* var koodit = await _context.Produced.Select(p => p.SapCode).ToListAsync();
                

                int totalSatsikoko = 0;
                foreach (var item in koodit)
                {
                    var satsikokoSum = await _context.Recipes
                        .Where(p => p.Koodi == item)
                        .SumAsync(p => p.Satsikoko);

                    totalSatsikoko += satsikokoSum;
                }

                TempData["volume"] = totalSatsikoko; */
                //  Log.Information(startdate2 + "  SSTTTAAARTTTDAAAATTEEelse222222222222222222222222222");
                //  Log.Information(enddate2 + "  EEEEEEEEENNNNNNDDDDDelse222222222222222222222222222");
                // var Code22 = TempData["filePathsave2"] as string;
                return View(Shiftreport);
            }

            // return View(Produce);
        }

        /* public ActionResult Index()
         {
             return View();
         } */

        // GET: ShiftreportsController/Details/5
        public ActionResult Details(int id)
		{
			return View();
		}

        // GET: ShiftreportsController/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: ShiftreportsController/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(Shiftreport shiftreport, string password)
        {
            DateTime now = DateTime.Now;
            DateTimeOffset unixTime = new DateTimeOffset(now);
            int unixTimestamp = (int)unixTime.ToUnixTimeSeconds();
            if (string.IsNullOrEmpty(password))
            {
                ViewBag.ErrorMessage = "Anna Salasana";
                return View();
            }
            if (ModelState.IsValid && password != null && password == "22")
            {
                var model = new Shiftreport
                {
                    Header = shiftreport.Header,
                    Report = shiftreport.Report,
                    Timestamp = unixTimestamp
               };
                _context.Add(model);
               // _context.Add(shiftreport);
                await _context.SaveChangesAsync();
                return RedirectToAction("Index", "Shiftreport");
            }
            else
            {
                // Väärä salasana
                ViewBag.ErrorMessage = "VIRHEELLINEN SALASANA";
                return View();
                // return View(recipe);
            }
        }

        // GET: ShiftreportsController/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.Shiftreports == null)
            {
                return NotFound();
            }

            var shiftreport = await _context.Shiftreports.FindAsync(id);
            if (shiftreport == null)
            {
                return NotFound();
            }
            return View(shiftreport);
        }

        // POST: ShiftreportsController/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, Shiftreport shiftreport, string password)
        {
            if (string.IsNullOrEmpty(password))
            {
                ViewBag.ErrorMessage = "Anna salasana";
                return View();
            }
            if (password != null && password == "22")
            {
                _context.Update(shiftreport);
                await _context.SaveChangesAsync();
                // return View(recipe);
                return RedirectToAction("Index", "Shiftreport");
            }
            else
            {
                // Väärä salasana
                ViewBag.ErrorMessage = "VIRHEELLINEN SALASANA";
                return View();

            }

        }

        // GET: ShiftreportsController/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {


            if (id == null || _context.Shiftreports == null)
            {
                return NotFound();
            }

            var Shiftreport = await _context.Shiftreports.FindAsync(id);
            if (Shiftreport == null)
            {
                return NotFound();
            }
            return View(Shiftreport);
            // return RedirectToAction("Index", "Home");


        }

        // POST: ShiftreportsController/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Delete(int id, Shiftreport shiftreport, string password)
        {
            if (string.IsNullOrEmpty(password))
            {
                ViewBag.ErrorMessage = "Anna salasana";
                return View();
            }
          
            if (shiftreport != null && password != null && password == "22")
            {
                
                _context.Remove(shiftreport);
                await _context.SaveChangesAsync();
                return RedirectToAction("Index", "Shiftreport"); 
            }
            else
            {
                // Väärä salasana
                ViewBag.ErrorMessage = "VIRHEELLINEN SALASANA";
                return View();
            }
        }
    }
}
