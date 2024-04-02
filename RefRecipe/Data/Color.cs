using System.ComponentModel.DataAnnotations;

namespace RefRecipe.Data
{
	public class Color
	{


		[Key]
		public int Id { get; set; }
		public string? MaterialColor { get; set; }

		public string? Colorname { get; set; }

		

	}
}
