using System;
using MassTransit;

namespace Avalier.Todo.Host.Data.Model
{
    public class Todo
    {
        public Guid Id { get; set; } = NewId.NextGuid();
        
        public string Description { get; set; } = "";

        public bool IsCompleted { get; set; } = false;
    }
}