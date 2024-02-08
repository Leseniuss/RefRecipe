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

       

        // Voit lisätä muita ominaisuuksia tähän, kuten ainesosat, ohjeet jne.
        // [MaxLength(255)] // Oletuksena tiedoston polku voi olla enintään 255 merkkiä pitkä
    }
}
