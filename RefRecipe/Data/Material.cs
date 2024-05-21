using System.ComponentModel.DataAnnotations;

namespace RefRecipe.Data
{
    public class Material
    {
        [Key]
        public int Id { get; set; }

        [Required(ErrorMessage = "Nimi on pakollinen.")]
        public string? Nimi { get; set; }

        [Required(ErrorMessage = "Koodi on pakollinen.")]
        public string? Koodi { get; set; }

        public string? Sijainti { get; set; }

    }
}
