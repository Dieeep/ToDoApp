using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ToDoApp.Data.Context;
using ToDoApp.Data.Models;
using ToDoApp.Services.Interfaces;

namespace ToDoApp.Services.Services
{
    public class TaskService : ITaskService
    {
        private readonly ToDoContext _context;

        public TaskService(ToDoContext context)
        {
            _context = context;
        }

        public async Task<TaskItem>CreateTaskAsync(TaskItem task)

        {
            _context.Tasks.Add(task);
            await _context.SaveChangesAsync();
            return task;
        }

        public async Task<TaskItem> GetTaskAsync(int id)
        {
            return await _context.Tasks.Include(t => t.Status).FirstOrDefaultAsync(t => t.Id == id);
        }

        public async Task<TaskItem> UpdateTaskAsync(TaskItem task)
        {
            var existingTask = await _context.Tasks.Include(t => t.Status).FirstOrDefaultAsync(t => t.Id == task.Id);
            if (existingTask == null)
            {
                throw new Exception("Task not found");
            }

            if (!IsValidStatusTransition(existingTask.Status.Name, task.Status.Name))
            {
                throw new Exception("Invalid status transition");
            }

            existingTask.Title = task.Title;
            existingTask.Description = task.Description;
            existingTask.StatusId = task.StatusId;
            existingTask.AssigneeId = task.AssigneeId;

            _context.Entry(existingTask).State = EntityState.Modified;
            await _context.SaveChangesAsync();

            return existingTask;
        }

        public async Task DeleteTaskAsync(int id)
        {
            var task = await _context.Tasks.FindAsync(id);
            if (task != null)
            {
                _context.Tasks.Remove(task);
                await _context.SaveChangesAsync();
            }
        }

        private bool IsValidStatusTransition(string currentStatus, string newStatus)
        {
            switch (currentStatus)
            {
                case "To Do":
                    return newStatus == "In Progress";
                case "In Progress":
                    return newStatus == "To Do" || newStatus == "Done";
                case "Done":
                    return false; 
                default:
                    return false;
            }
        }
    }
}
