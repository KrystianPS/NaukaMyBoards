namespace MyBoards.Entities
{
    public class State
    {
        public int Id { get; set; }
        public string CurrentState { get; set; }

        public List<WorkItem> WorkItems { get; set; } = new List<WorkItem>();
    }
}
