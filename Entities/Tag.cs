namespace MyBoards.Entities
{
    public class Tag
    {
        public int Id { get; set; }
        public string Value { get; set; }
<<<<<<< HEAD
=======
        public string Category { get; set; }
>>>>>>> 6b9643e3b9d2034939d40569d23d02fed8450f75
        public List<WorkItem> WorkItems { get; set; } = new List<WorkItem>();

    }
}
