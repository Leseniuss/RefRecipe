using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using RefRecipe.Data;
using System.Diagnostics;

namespace RefRecipe.Controllers
{
    public class SettingsController : Controller
    {

		private readonly RefRecipeContext _context;
		public SettingsController(RefRecipeContext context)
		{
			_context = context;
			// ExcelPackage.LicenseContext = LicenseContext.Default;
		}

		public IActionResult Index()
        {
            
           // TempData["Color1"] = "blue";
           // TempData["Color2"] = "red";
            return View();
        }

        [HttpPost]
        public IActionResult SelectMaterialColors(string materialColor, string materialColor2)
        {
			
			string materialColor11 = "materialColor1";
            string materialColor22 = "materialColor2";
			var colorEntity = _context.Colors.FirstOrDefault(p => p.MaterialColor == materialColor11);
			var colorEntity2 = _context.Colors.FirstOrDefault(p => p.MaterialColor == materialColor22);

			colorEntity.Colorname = materialColor;
			colorEntity2.Colorname = materialColor2;
			_context.SaveChanges();
		
			return RedirectToAction("Index", "Materials"); 
           // return RedirectToAction("AuthIndex", "Home");
        }
		[HttpPost]
		public IActionResult SelectRecipeColors(string recipeColor, string recipeColor2)
		{
            
			string recipeColor11 = "recipeColor1";
            string recipeColor22 = "recipeColor2";
            var colorEntity = _context.Colors.FirstOrDefault(p => p.MaterialColor == recipeColor11);
            var colorEntity2 = _context.Colors.FirstOrDefault(p => p.MaterialColor == recipeColor22);

            colorEntity.Colorname = recipeColor;
            colorEntity2.Colorname = recipeColor2;
			_context.SaveChanges();

			return RedirectToAction("Index", "Home"); 
		}
		[HttpPost]
		public IActionResult SelectProduceColors(string produceColor, string produceColor2)
		{
			
			string produceColor11 = "produceColor1";
			string produceColor22 = "produceColor2";
			var colorEntity = _context.Colors.FirstOrDefault(p => p.MaterialColor == produceColor11);
			var colorEntity2 = _context.Colors.FirstOrDefault(p => p.MaterialColor == produceColor22);

			colorEntity.Colorname = produceColor;
			colorEntity2.Colorname = produceColor2;
			_context.SaveChanges();

			return RedirectToAction("Index", "Produced"); 
		}

		// GET: SettingsController
		public ActionResult Index22()
        {
            return View();
        }

        // GET: SettingsController/Details/5
        public ActionResult Details(int id)
        {
            return View();
        }

        // GET: SettingsController/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: SettingsController/Create
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

        // GET: SettingsController/Edit/5
        public ActionResult Edit(int id)
        {
            return View();
        }

        // POST: SettingsController/Edit/5
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

        // GET: SettingsController/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: SettingsController/Delete/5
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
