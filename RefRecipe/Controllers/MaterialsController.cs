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
    

        // GET: MaterialsController
      /*  public ActionResult Index()
        {
            return View();
        } */

        // GET: MaterialsController/Details/5
        public ActionResult Details(int id)
        {
            return View();
        }

        // GET: MaterialsController/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: MaterialsController/Create
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

        // GET: MaterialsController/Edit/5
        public ActionResult Edit(int id)
        {
            return View();
        }

        // POST: MaterialsController/Edit/5
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

        // GET: MaterialsController/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: MaterialsController/Delete/5
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
