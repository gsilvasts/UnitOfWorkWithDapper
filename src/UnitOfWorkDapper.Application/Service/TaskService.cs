using UnitOfWorkDapper.Application.Interfaces;
using UnitOfWorkDapper.Core.DTO;
using UnitOfWorkDapper.Core.Entities;
using UnitOfWorkDapper.Core.Interfaces.Repositories;
using UnitOfWorkDapper.Core.Interfaces.UnitOfWork;

namespace UnitOfWorkDapper.Application.Service
{
    public class TaskService : ITaskService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly ITaskRepository _taskRepository;

        public TaskService(IUnitOfWork unitOfWork, ITaskRepository taskRepository)
        {
            _unitOfWork = unitOfWork;
            _taskRepository = taskRepository;
        }

        public async Task<IEnumerable<ToDoTask>> GetTasksAsync()
        {
            return await _taskRepository.GetAllAsync();
        }

        public async Task<ToDoTask> GetTaskByIdAsync(int taskId)
        {
            return await _taskRepository.GetByIdAsync(taskId);
        }

        public async Task<ToDoTask> CreateTaskAsync(CreateToDoTaskDto taskDto)
        {
            try
            {
                _unitOfWork.BeginTransaction();

                var task = new ToDoTask
                {
                    Title = taskDto.Title,
                    Description = taskDto.Description,
                    CreatedAt = DateTime.Now
                };

                _taskRepository.InsertAsync(task);

                _unitOfWork.Commit(); // Commit the transaction asynchronously

                return task;
            }
            catch (Exception)
            {
                _unitOfWork.Rollback();
                throw;
            }
        }

        public async Task<ToDoTask> UpdateTaskAsync(int taskId, UpdateToDoTaskDto taskDto)
        {
            try
            {
                var existingTask = await _taskRepository.GetByIdAsync(taskId);

                if (existingTask == null)
                {
                    return null; // Task not found
                }

                existingTask.Title = taskDto.Title;
                existingTask.Description = taskDto.Description;
                existingTask.CompletedAt = taskDto.CompletedAt;

                _unitOfWork.BeginTransaction();

                _taskRepository.UpdateAsync(existingTask);

                _unitOfWork.Commit(); // Commit the transaction asynchronously

                return existingTask;
            }
            catch (Exception)
            {
                _unitOfWork.Rollback();
                throw;
            }            
        }

        public async Task<bool> DeleteTaskAsync(int taskId)
        {
            try
            {
                var existingTask = await _taskRepository.GetByIdAsync(taskId);

                if (existingTask == null)
                {
                    return false; // Task not found
                }
                _unitOfWork.BeginTransaction();

                _taskRepository.DeleteAsync(existingTask);

                _unitOfWork.Commit(); // Commit the transaction asynchronously

                return true;
            }
            catch (Exception)
            {
                _unitOfWork.Rollback();
                return false;
            }            
        }
    }


}
