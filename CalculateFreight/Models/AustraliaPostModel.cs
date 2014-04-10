using System.ComponentModel.DataAnnotations;

namespace CalculateFreight.Models
{
    public class AustraliaPostModel
    {
        [Required]
        public int FromPostCode { get; set; }

        [Required]
        public int ToPostCode { get; set; }

        [Required]
        [Range(0.1, 105)]
        public double Length { get; set; }

        [Required]
        [Range(0.1, 105)]
        public double Width { get; set; }

        [Required]
        [Range(0.1, 105)]
        public double Height { get; set; }

        [Required]
        [Range(0.1, 22)]
        public double Weight { get; set; }
    }
}