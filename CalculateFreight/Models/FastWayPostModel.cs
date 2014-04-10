using System.ComponentModel.DataAnnotations;

namespace CalculateFreight.Models
{
    public class FastWayPostModel
    {
        [Required]
        public string RFCode { get; set; }

        [Required]
        public string Suburb { get; set; }

        [Required]
        public string DestPostCode { get; set; }

        [Required]
        public double Weight { get; set; }
    }
}