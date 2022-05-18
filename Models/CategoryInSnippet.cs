namespace DoItInCpp.Models
{
    public class CategoryInSnippet
    {
        public int SnippetID { get; set; }
        public int CategoryID { get; set; }
        public Snippet? Snippet { get; set; }
        public Category? Category { get; set; }
    }
}