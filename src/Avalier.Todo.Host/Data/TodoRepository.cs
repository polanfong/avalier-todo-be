using System;
using System.Linq;
using System.Collections.Generic;

namespace Avalier.Todo.Host.Data
{
    public class TodoRepository : ITodoRepository
    {
        private IDictionary<Guid, Model.Todo> _items = new Dictionary<Guid, Model.Todo>();

        public IQueryable<Model.Todo> Queryable => _items.Values.AsQueryable();

        public Model.Todo this[Guid index]
        {
            get { return this.GetById(index); }
            set { this.SetById(index, value); }
        }

        public Model.Todo GetById(Guid id)
        {
            return _items.ContainsKey(id) ? _items[id] : null;
        }

        public void SetById(Guid id, Model.Todo value)
        {
            _items[id] = value;
        }

        public void Set(Model.Todo todo)
        {
            this.SetById(todo.Id, todo);
        }

    }
}