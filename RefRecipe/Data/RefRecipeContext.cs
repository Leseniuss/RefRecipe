using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

namespace RefRecipe.Data
{
    public class RefRecipeContext : DbContext
    {
        public RefRecipeContext (DbContextOptions<RefRecipeContext> options)
            : base(options)
        {
        }

        public DbSet<Recipe> Recipes { get; set; } = default!;
        public DbSet<Material> Materials { get; set; } = default!;
    }
}
