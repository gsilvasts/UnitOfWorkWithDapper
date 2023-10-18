using UnitOfWorkDapper.Core.DTO;
using UnitOfWorkDapper.Core.Entities;

namespace UnitOfWorkDapper.Application.Interfaces
{
    public interface ITaskService
    {
        Task<IEnumerable<ToDoTask>> GetTasksAsync();
        Task<ToDoTask> GetTaskByIdAsync(int taskId);
        Task<ToDoTask> CreateTaskAsync(CreateToDoTaskDto taskDto);
        Task<ToDoTask> UpdateTaskAsync(int taskId, UpdateToDoTaskDto taskDto);
        Task<bool> DeleteTaskAsync(int taskId);
    }
}
