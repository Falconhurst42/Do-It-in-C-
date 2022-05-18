using System.ComponentModel.DataAnnotations;

namespace DoItInCpp.Models
{
    public class AddOn
    {
        public int ID { get; set; }
        [Required]
        [StringLength(50, ErrorMessage = "Name cannot be longer than 50 characters.")]
        public string Name { get; set; }

        public string? Tagline {get; set; }
        
        [Required]
        [StringLength(10000, ErrorMessage = "Description cannot be longer than 10000 characters.")]
        public string? Description { get; set; }

        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}")]
        [Display(Name = "Date Created")]
        // [DatabaseGenerated(DatabaseGeneratedOption.Identity)] // https://www.learnentityframeworkcore.com/configuration/data-annotation-attributes/databasegenerated-attribute
        public DateTime Created { get; set; }

        [DisplayFormat(DataFormatString = "{0:g}")]
        [Display(Name = "Last Updated")]
        // [DatabaseGenerated(DatabaseGeneratedOption.Computed)] // https://www.learnentityframeworkcore.com/configuration/data-annotation-attributes/databasegenerated-attribute
        public DateTime LastUpdated { get; set; }

        public static AddOn GetBlank() {
            AddOn ret = new AddOn();
            ret.ID = -1;
            ret.Name = "No AddOn";
            ret.Tagline = "Placeholder AddOn";
            return ret;
        }
    }
}