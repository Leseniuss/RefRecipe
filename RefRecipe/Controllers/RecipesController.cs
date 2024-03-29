﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using RefRecipe.Data;

namespace RefRecipe.Controllers
{
    public class RecipesController : Controller
    {
        private readonly RefRecipeContext _context;

        public RecipesController(RefRecipeContext context)
        {
            _context = context;
        }

        // GET: Recipes
        public async Task<IActionResult> Index()
        {
            return View(await _context.Recipes.ToListAsync());
        }

        // GET: Recipes/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.Recipes == null)
            {
                return NotFound();
            }

            var recipe = await _context.Recipes
                .FirstOrDefaultAsync(m => m.Id == id);
            if (recipe == null)
            {
                return NotFound();
            }

            return View(recipe);
        }

        // GET: Recipes/Create
        public IActionResult Create()
        {
            return View();
        }
        [BindProperty]


        /* [HttpPost]
         public async Task<IActionResult> Createe()
         {
             if (!ModelState.IsValid || _context.Recipes == null || Recipe == null)
             {
                 return View();

             }

             _context.Recipes.Add(Recipe);
             await _context.SaveChangesAsync();

             return Redirect("/Home/Index");
         } */

        public Recipe Recipe { get; set; } = default!;

        // POST: Recipes/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(Recipe recipe, string password)
        {
            if (string.IsNullOrEmpty(password))
            {
                ViewBag.ErrorMessage = "Anna Salasana";
                return View();
            }
            if (ModelState.IsValid && password != null && password == "22")
            {
                _context.Add(recipe);
                await _context.SaveChangesAsync();
                return RedirectToAction("AuthIndex", "Home");
            }
            else
            {
                // Väärä salasana
                ViewBag.ErrorMessage = "VIRHEELLINEN SALASANA";
                return View();
               // return View(recipe);
            }
        }

        // GET: Recipes/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.Recipes == null)
            {
                return NotFound();
            }

            var recipe = await _context.Recipes.FindAsync(id);
            if (recipe == null)
            {
                return NotFound();
            }
            return View(recipe);
        } 

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, Recipe recipe, string password)
        {
            if (string.IsNullOrEmpty(password))
            {
                ViewBag.ErrorMessage = "Anna salasana";
                return View();
            }
            if (password != null && password == "22")
            {
                _context.Update(recipe);
                await _context.SaveChangesAsync();
                // return View(recipe);
                return RedirectToAction("AuthIndex", "Home");
            }
            else
            {
                // Väärä salasana
                ViewBag.ErrorMessage = "VIRHEELLINEN SALASANA";
                return View();

            }

        }

        // GET: Recipes/Delete/5
         public async Task<IActionResult> Delete(int? id)
         {
           

            if (id == null || _context.Recipes == null)
            {
                return NotFound();
            }

            var recipe = await _context.Recipes.FindAsync(id);
            if (recipe == null)
            {
                return NotFound();
            }
             return View(recipe);
           // return RedirectToAction("Index", "Home");


        }

        // POST: Recipes/Delete/5
        [HttpPost, ActionName("Delete")]
         [ValidateAntiForgeryToken]
         public async Task<IActionResult> Delete(int id, Recipe recipe, string password)
         {
            if (string.IsNullOrEmpty(password))
            {
                ViewBag.ErrorMessage = "Anna salasana";
                return View();
            }
            /* var recipe = await _context.Recipes.FindAsync(id);
              if (recipe != null)
              {
                  _context.Recipes.Remove(recipe);
              }

              await _context.SaveChangesAsync();
             // return RedirectToAction(nameof(Index));
             return RedirectToAction("AuthIndex", "Home"); */
            if (recipe != null && password != null && password == "22")
            {
                // _context.Materials.Remove(material);
                _context.Remove(recipe);
                await _context.SaveChangesAsync();
                return RedirectToAction("AuthIndex", "Home");
            }
            else
            {
                // Väärä salasana
                ViewBag.ErrorMessage = "VIRHEELLINEN SALASANA";
                return View();
            }
        } 

        /* private bool RecipeExists(int id)
         {
           return _context.Recipes.Any(e => e.Koodi = id);
         } */
    }
}

        // POST: Recipes/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
       /* [HttpPost]
[ValidateAntiForgeryToken]
         public async Task<IActionResult> Edit(int id, [Bind("Koodi,Satsikoko,Nimi")] Recipe recipe)
         {
             if (id != recipe.Koodi)
             {
                 return NotFound();
             }

             if (ModelState.IsValid)
             {
                 try
                 {
                     _context.Update(recipe);
                     await _context.SaveChangesAsync();
                 }
                 catch (DbUpdateConcurrencyException)
                 {
                     if (!RecipeExists(recipe.Koodi))
                     {
                         return NotFound();
                     }
                     else
                     {
                         throw;
                     }
                 }
                 return RedirectToAction(nameof(Index));
             }
             return View(recipe);
         } */

        // GET: Recipes/Delete/5
        /* public async Task<IActionResult> Delete(int? id)
         {
             if (id == null || _context.Recipes == null)
             {
                 return NotFound();
             }

             var recipe = await _context.Recipes
                 .FirstOrDefaultAsync(m => m.Koodi == id);
             if (recipe == null)
             {
                 return NotFound();
             }

             return View(recipe);
         } */

        // POST: Recipes/Delete/5
        /* [HttpPost, ActionName("Delete")]
         [ValidateAntiForgeryToken]
         public async Task<IActionResult> DeleteConfirmed(int id)
         {
             if (_context.Recipes == null)
             {
                 return Problem("Entity set 'RefRecipeContext.Recipe'  is null.");
             }
             var recipe = await _context.Recipes.FindAsync(id);
             if (recipe != null)
             {
                 _context.Recipes.Remove(recipe);
             }

             await _context.SaveChangesAsync();
             return RedirectToAction(nameof(Index));
         } */

        /* private bool RecipeExists(int id)
         {
           return _context.Recipes.Any(e => e.Koodi == id);
         } */
    

