namespace DoItInCpp.Models
{
    public class AddOnInSnippet
    {
        public int SnippetID { get; set; }
        public int AddOnID { get; set; }
        public Snippet? Snippet { get; set; }
        public AddOn? AddOn { get; set; }
    }
}