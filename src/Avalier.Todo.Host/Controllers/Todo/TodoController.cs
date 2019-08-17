using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.AspNetCore.Mvc;

namespace Avalier.Todo.Host.Controllers.Todo
{
    [Route("api/[controller]")]
    [ApiController]
    public class TodoController : ControllerBase
    {
        private readonly Data.ITodoRepository _todoRepository;

        public TodoController(Data.ITodoRepository todoRepository)
        {
            _todoRepository = todoRepository;
        }

        [HttpGet("")]
        [ProducesResponseType(typeof(IEnumerable<Todo>), 200)]
        public IActionResult Find([FromQuery]FindRequest request)
        {
            var queryable = _todoRepository.Queryable;

            if (!string.IsNullOrEmpty(request.Description))
            {
                queryable = queryable.Where(o => o.Description.ToLower().StartsWith(request.Description.ToLower()));
            }

            if (request.IsCompleted.HasValue)
            {
                queryable = queryable.Where(o => o.IsCompleted == request.IsCompleted.Value);
            }

            var items = queryable
                .Select(o => o.Map())
                .ToList();

            return this.Ok(items);
        }
        
        [HttpGet("{id}")]
        [ProducesResponseType(204)]
        [ProducesResponseType(typeof(Todo), 200)]
        public IActionResult Get(Guid id)
        {
            // Retrieve //
            var todo = _todoRepository.GetById(id).Map();

            // Validate //
            if (null == todo) return this.NoContent();

            // Respond //
            return this.Ok(todo.Map());
        }

        [HttpPost()]
        [ProducesResponseType(typeof(Todo), 200)]
        public IActionResult Post(string description)
        {
            // Create //
            var todoDataModel = new Data.Model.Todo() {Description = description};
            var todoDtoModel = todoDataModel.Map();
            _todoRepository.Set(todoDataModel);

            // Respond //
            return this.Ok(todoDtoModel);
        }

        [HttpPut("{id}")]
        [ProducesResponseType(204)]
        [ProducesResponseType(typeof(Todo), 200)]
        public IActionResult Put(Guid id, [FromBody] Todo todo)
        {
            // Validate //
            var todoDataModel = _todoRepository.GetById(id);
            if (null == todoDataModel) return this.NoContent();
            if (todoDataModel.Id != id) throw new System.Exception("Route and model id's do not match");

            // Update //
            todoDataModel.Description = todo.Description;
            todoDataModel.IsCompleted = todo.IsCompleted;
            _todoRepository[todo.Id] = todoDataModel;

            // Respond //
            return this.Ok(todoDataModel.Map());
        }
    }
}
