using Dapper;
using UnitOfWorkDapper.Core.Entities;
using UnitOfWorkDapper.Core.Interfaces.Repositories;

namespace UnitOfWorkDapper.Infrastructure.Persistence
{
    public class TaskRepository : ITaskRepository
    {
        private readonly DapperDbSession _session;

        public TaskRepository(DapperDbSession session)
        {
            _session = session;
        }

        public async Task<IEnumerable<ToDoTask>> GetAllAsync()
        {
            string sql = "SELECT * FROM ToDoTasks";
            return await _session.Connection.QueryAsync<ToDoTask>(sql);
        }

        public async Task<ToDoTask> GetByIdAsync(int taskId)
        {
            string sql = "SELECT * FROM ToDoTasks WHERE Id = @Id";
            return await _session.Connection.QueryFirstOrDefaultAsync<ToDoTask>(sql, new { Id = taskId });
        }

        public async Task InsertAsync(ToDoTask task)
        {
            string sql = "INSERT INTO ToDoTasks (Title, Description, CreatedAt) VALUES (@Title, @Description, @CreatedAt)";
            await _session.Connection.ExecuteAsync(sql, new
            {
                Title = task.Title,
                Description = task.Description,
                CreatedAt = task.CreatedAt
            });
        }

        public async Task UpdateAsync(ToDoTask task)
        {
            string sql = "UPDATE ToDoTasks SET Title = @Title, Description = @Description, CompletedAt = @CompletedAt WHERE Id = @Id";
            await _session.Connection.ExecuteAsync(sql, new
            {
                Id = task.Id,
                Title = task.Title,
                Description = task.Description,
                CompletedAt = task.CompletedAt
            });
        }

        public async Task DeleteAsync(ToDoTask task)
        {
            string sql = "DELETE FROM ToDoTasks WHERE Id = @Id";
            await _session.Connection.ExecuteAsync(sql, new { Id = task.Id });
        }
    }

}
