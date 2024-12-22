namespace MyBoards.Entities
{
    public class Comment
    {
        public string Message { get; set; }
        public string Author { get; set; }

        public DateTime CreatedCommentDate { get; set; }
        public DateTime? UpdatedCommentDate { get; set; }

    }
}
