namespace MyBoards.Entities
{
    public class Comment
    {
        public int Id { get; set; }
        public string Message { get; set; }


        public DateTime CreatedCommentDate { get; set; }
        public DateTime? UpdatedCommentDate { get; set; }


        public WorkItem WorkItem { get; set; }
        public int WorkItemId { get; set; }

        public User Author { get; set; }
        public Guid AuthorId { get; set; }
    }
}
