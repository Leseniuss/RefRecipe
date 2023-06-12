using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace RefRecipe.Data
{
    public class Ingredient
    {
        [Key]
        public int Id { get; set; }
        public int IngKoodi { get; set; }
        public string? Nimi { get; set; }
        public double KgSatsi { get; set; }
        public int Tuotekoodi { get; set; }

        
       
    }
}
