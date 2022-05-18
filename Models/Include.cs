using System.ComponentModel.DataAnnotations;

namespace DoItInCpp.Models
{
    public class Include
    {
        public int ID { get; set; }
        [Required]
        [StringLength(50, ErrorMessage = "Name cannot be longer than 50 characters.")]
        public string Name { get; set; }

        public static Include GetBlank() {
            Include ret = new Include();
            ret.ID = -1;
            ret.Name = "No Include";
            return ret;
        }
    }
}