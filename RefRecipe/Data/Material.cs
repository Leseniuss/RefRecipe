using System.ComponentModel.DataAnnotations;

namespace RefRecipe.Data
{
    public class Material
    {
        [Key]
        public int Id { get; set; }

        [Required(ErrorMessage = "Nimi on pakollinen.")]
        public string? Name { get; set; }

        [Required(ErrorMessage = "Koodi on pakollinen.")]
        public string? SapCode { get; set; }

        public string? Location { get; set; }

    }
}
