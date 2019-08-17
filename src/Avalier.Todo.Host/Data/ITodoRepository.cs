using System;
using System.Linq;

namespace Avalier.Todo.Host.Data
{
    public interface ITodoRepository
    {
        IQueryable<Model.Todo> Queryable { get; }
        Model.Todo this[Guid index] { get; set; }
        Model.Todo GetById(Guid id);
        void SetById(Guid id, Model.Todo value);
        void Set(Model.Todo todo);
    }
}