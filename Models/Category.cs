using System.ComponentModel.DataAnnotations;

namespace DoItInCpp.Models
{
    public class Category
    {
        public int ID { get; set; }
        [Required]
        [StringLength(50, ErrorMessage = "Name cannot be longer than 50 characters.")]
        public string Name { get; set; }

        public static Category GetBlank() {
            Category ret = new Category();
            ret.ID = -1;
            ret.Name = "No Category";
            return ret;
        }
    }
}