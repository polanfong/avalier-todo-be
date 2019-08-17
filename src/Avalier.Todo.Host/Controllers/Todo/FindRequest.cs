namespace Avalier.Todo.Host.Controllers.Todo
{
    public class FindRequest
    {
        public string Description { get; set; }
        
        public bool? IsCompleted { get; set; }
    }
}