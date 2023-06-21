using System.ComponentModel.DataAnnotations;

namespace RefRecipe.Data
{
    public class Recipe
    {
        [Key]
        public int Id { get; set; }
        public string? Koodi { get; set; }
        public int Satsikoko { get; set; }
        public string? Nimi { get; set; }
        public double BrixMin { get; set; }
        public double BrixMax { get; set; }
        public double pHMin { get; set; }
        public double pHMax { get; set; }
        public double KokonaishapotMin { get; set; }
        public double KokonaishapotMax { get; set; }
        public string? FilePath { get; set; }

    }
}
