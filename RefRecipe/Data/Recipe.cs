using System.ComponentModel.DataAnnotations;

namespace RefRecipe.Data
{
    public class Recipe
    {
        [Key]
        public int Id { get; set; }

        public string? Koodi { get; set; }
       
        public string? Nimi { get; set; }
        
        public string? FilePath { get; set; }

    }
}
