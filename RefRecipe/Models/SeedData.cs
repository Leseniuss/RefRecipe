using Microsoft.EntityFrameworkCore;
using RefRecipe.Data;

namespace RefRecipe.Models;

public static class SeedData
{
    public static void Initialize(IServiceProvider serviceProvider)
    {
        using (var context = new RefRecipeContext(
            serviceProvider.GetRequiredService<
                DbContextOptions<RefRecipeContext>>()))
        {
            if (context == null || context.Recipes == null)
            {
                throw new ArgumentNullException("Null RefRecipesContext");
            }

            
            context.Database.EnsureCreated();

            // Look for any phones.
            if (context.Recipes.Any())
            {
                return;   // DB has been seeded
            }

            context.Recipes.AddRange(
                new Recipe
                {
                    Koodi = "61020000",
                    Satsikoko = 12000,
                    Nimi = "Olutta",
                 
                },

                new Recipe
                {
                    Koodi = "61020000",
                    Satsikoko = 13000,
                    Nimi = "Siideriii",

                },



                new Recipe
                {
                    Koodi = "61020000",
                    Satsikoko = 14000,
                    Nimi = "Viiniiii",

                },

                new Recipe
                {
                    Koodi = "61020000",
                    Satsikoko = 17000,
                    Nimi = "Viinaaaa",

                }
            );
            context.SaveChanges();
        }
    }
}