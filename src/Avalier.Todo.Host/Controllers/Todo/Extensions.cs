using System.Net.Mail;

namespace Avalier.Todo.Host.Controllers.Todo
{
    public static class Extensions
    {
        public static Data.Model.Todo Map(this Todo value)
        {
            if (null == value) return null;
            return new Data.Model.Todo()
            {
                Id = value.Id,
                Description = value.Description,
                IsCompleted = value.IsCompleted
            };
        }
        
        public static Todo Map(this Data.Model.Todo value)
        {
            if (null == value) return null;
            return new Todo()
            {
                Id = value.Id,
                Description = value.Description,
                IsCompleted = value.IsCompleted
            };
        }
    }
}