using System.ComponentModel.DataAnnotations;
using System.Collections.Generic;

namespace DoItInCpp.Models
{
    public class Snippet
    {
        public int ID { get; set; }
        [Required]
        [StringLength(50, ErrorMessage = "Name cannot be longer than 50 characters.")]
        public string Name { get; set; }
        
        [Required]
        [StringLength(5000, ErrorMessage = "Description cannot be longer than 5000 characters.")]
        public string? Description { get; set; }
        
        [Required]
        [StringLength(5000, ErrorMessage = "Code cannot be longer than 5000 characters.")]
        public string? Code { get; set; }
        
        [Required]
        [StringLength(5000, ErrorMessage = "Documentation cannot be longer than 5000 characters.")]
        public string? Documentation { get; set; }
        public int VersionID { get; set; }

        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}")]
        [Display(Name = "Date Created")]
        // [DatabaseGenerated(DatabaseGeneratedOption.Identity)] // https://www.learnentityframeworkcore.com/configuration/data-annotation-attributes/databasegenerated-attribute
        public DateTime Created { get; set; }

        [DisplayFormat(DataFormatString = "{0:g}")]
        [Display(Name = "Last Updated")]
        // [DatabaseGenerated(DatabaseGeneratedOption.Computed)] // https://www.learnentityframeworkcore.com/configuration/data-annotation-attributes/databasegenerated-attribute
        public DateTime LastUpdated { get; set; }
        
        // navigation property
        public LanguageVersion? Version { get; set; }
        public ICollection<AddOnInSnippet>? AddOns { get; set; }
        public ICollection<IncludeInSnippet>? Includes { get; set; }
        public ICollection<CategoryInSnippet>? Categories { get; set; }
    }
}