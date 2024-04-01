/*using System.ComponentModel.DataAnnotations;

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
} */
using System.ComponentModel.DataAnnotations;

namespace RefRecipe.Data
{
    public class Recipe
    {
        [Key]
        public int Id { get; set; }

        [Required(ErrorMessage = "Koodi on pakollinen.")]
        public string? Koodi { get; set; }

        [Required(ErrorMessage = "Nimi on pakollinen.")]
        public string? Nimi { get; set; }

        [Required(ErrorMessage = "Satsikoko on pakollinen.")]
        public int Satsikoko { get; set; }



    }
}
