namespace DoItInCpp.Models
{
    public class IncludeInSnippet
    {
        public int SnippetID { get; set; }
        public int IncludeID { get; set; }
        public Snippet? Snippet { get; set; }
        public Include? Include { get; set; }
    }
}