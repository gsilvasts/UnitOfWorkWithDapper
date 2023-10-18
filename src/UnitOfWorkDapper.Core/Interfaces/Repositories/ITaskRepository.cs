using UnitOfWorkDapper.Core.Entities;

namespace UnitOfWorkDapper.Core.Interfaces.Repositories
{
    public interface ITaskRepository
    {
        Task<IEnumerable<ToDoTask>> GetAllAsync();
        Task<ToDoTask> GetByIdAsync(int taskId);
        Task InsertAsync(ToDoTask task);
        Task UpdateAsync(ToDoTask task);
        Task DeleteAsync(ToDoTask task);
    }
}
