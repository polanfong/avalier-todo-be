using System;

namespace Avalier.Todo.Host.Controllers.Todo
{
    public class Todo
    {
        public Guid Id { get; set; }
        
        public string Description { get; set; }
        
        public bool IsCompleted { get; set; }
    }
}