namespace LMSDotnetCore.Data_Transfer_Objects
{
    public class Document
    {
        public int DocumentID { get; set; }
        public required string Title { get; set; }
        public required string Description { get; set; }
        public required int AuthorID { get; set; }
        public required bool isPublished { get; set; }
        public required bool isDeleted { get; set; }
        public required string filename { get; set; }
    }
}
