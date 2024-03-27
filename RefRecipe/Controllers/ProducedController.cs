using DocumentFormat.OpenXml.Office2010.Excel;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using RefRecipe.Data;

namespace RefRecipe.Controllers
{
    public class ProducedController : Controller
    {
        private readonly RefRecipeContext _context;

        public ProducedController(RefRecipeContext context)
        {
            _context = context;
            // ExcelPackage.LicenseContext = LicenseContext.Default;
        }

        public IList<Produce> Produce { get; set; } = default!;

        // GET: Produced
        public ActionResult Index()
        {
            Produce = _context.Produced.OrderBy(p => p.Id).ToList();
            // var Code22 = TempData["filePathsave2"] as string;
            return View(Produce);
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
        public async Task<IActionResult> Create(Produce produce)
        {
            string? filePath2 = TempData["filePathsave2"] as string;
            var nimi = await _context.Recipes.Where(r => r.Koodi == filePath2).Select(r => r.Nimi).FirstOrDefaultAsync();
            if (produce != null)
            {
                
                // Muuta tietoja ennen tallennusta
                produce.SapCode = TempData["filePathsave2"] as string;
                produce.Name = nimi;
                produce.Timestamp = DateTime.Now.ToString();
               // produce.Name = TempData["name"] as string;
                
               // recipe.Koodi = TempData["filePathsave2"] as string;
               // recipe.Nimi = Name;
               // Timestamp = DateTime.Now.ToString(); // Esimerkki: Tallennetaan nykyinen aikaleima
                                                             // Lisää uusi tietue tietokantaan
                _context.Add(produce);
                await _context.SaveChangesAsync();
                return RedirectToAction("AuthIndex", "Home");
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
