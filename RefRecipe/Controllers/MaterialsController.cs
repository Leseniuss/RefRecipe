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

            // List<Recipe> recipes;
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

        // POST: Recipes/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
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
                return RedirectToAction("Index", "Materials");
            }
            else
            {
                // Väärä salasana
                ViewBag.ErrorMessage = "VIRHEELLINEN SALASANA";
                return View();

            }

        }
        // POST: Recipes/Delete/5
        // [HttpPost, ActionName("Delete")]
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
    }
}
