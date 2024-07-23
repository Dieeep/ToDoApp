using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ToDoApp.Data.Models;

namespace ToDoApp.Services.Interfaces

{
public interface ITaskService
    {
        Task<TaskItem> CreateTaskAsync(TaskItem task);
        Task<TaskItem> GetTaskAsync(int id);
        Task<TaskItem> UpdateTaskAsync(TaskItem task);
        Task DeleteTaskAsync(int id);
    }
}
