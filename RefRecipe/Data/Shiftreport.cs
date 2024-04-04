using System.ComponentModel.DataAnnotations;

namespace RefRecipe.Data
{
    public class Shiftreport
    {


        [Key]
        public int Id { get; set; }
        [Required(ErrorMessage = "Otsikko on pakollinen.")]
        public string? Header { get; set; }
        [Required(ErrorMessage = "Raportti on pakollinen.")]
        public string? Report { get; set; }

        public int Timestamp { get; set; }



    }
}