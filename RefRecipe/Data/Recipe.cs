using System.ComponentModel.DataAnnotations;

namespace RefRecipe.Data
{
    public class Recipe
    {
        [Key]
        public int Koodi { get; set; }
        public int Satsikoko { get; set; }
        public string? Nimi { get; set; }
     
    }
}
