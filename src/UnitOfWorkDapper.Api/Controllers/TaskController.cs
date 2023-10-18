using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using UnitOfWorkDapper.Application.Interfaces;
using UnitOfWorkDapper.Core.DTO;
using UnitOfWorkDapper.Core.Entities;

[Route("api/tasks")]
[ApiController]
public class TaskController : ControllerBase
{
    private readonly ITaskService _taskService;

    public TaskController(ITaskService taskService)
    {
        _taskService = taskService;
    }

    [HttpGet]
    public async Task<ActionResult<IEnumerable<ToDoTask>>> GetTasks()
    {
        var tasks = await _taskService.GetTasksAsync();
        return Ok(tasks);
    }

    [HttpGet("{id}")]
    public async Task<ActionResult<ToDoTask>> GetTask(int id)
    {
        var task = await _taskService.GetTaskByIdAsync(id);

        if (task == null)
        {
            return NotFound(); // Tarefa não encontrada
        }

        return Ok(task);
    }

    [HttpPost]
    public async Task<ActionResult<ToDoTask>> CreateTask(CreateToDoTaskDto taskDto)
    {
        var task = await _taskService.CreateTaskAsync(taskDto);
        return CreatedAtAction(nameof(GetTask), new { id = task.Id }, task);
    }

    [HttpPut("{id}")]
    public async Task<ActionResult<ToDoTask>> UpdateTask(int id, UpdateToDoTaskDto taskDto)
    {
        var updatedTask = await _taskService.UpdateTaskAsync(id, taskDto);

        if (updatedTask == null)
        {
            return NotFound(); // Tarefa não encontrada
        }

        return Ok(updatedTask);
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> DeleteTask(int id)
    {
        var deleted = await _taskService.DeleteTaskAsync(id);

        if (!deleted)
        {
            return NotFound(); // Tarefa não encontrada
        }

        return NoContent();
    }
}
