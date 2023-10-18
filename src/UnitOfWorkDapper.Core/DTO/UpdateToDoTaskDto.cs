namespace UnitOfWorkDapper.Core.DTO
{
    public class UpdateToDoTaskDto
    {
        public string Title { get; set; }
        public string Description { get; set; }
        public DateTime? CompletedAt { get; set; }
    }
}
