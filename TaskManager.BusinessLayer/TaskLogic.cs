using System.Collections.Generic;
using System.Linq;
using TaskManager.DataLayer;
using TaskManager.Entities;

namespace TaskManager.BusinessLayer
{

    /// <summary>
    /// This is the class which contains  the business logics  to add/update/delete/end task activities
    /// </summary>
    public class TaskLogic
    {
        /// <summary>
        /// Method  to add the task to the database
        /// </summary>
        /// <param name="item">Task with details</param>
        public void Add(Task item)
        {
            using (TaskManagerContext db = new TaskManagerContext())
            {
                db.Tasks.Add(item); 
                db.SaveChanges();
            }
        }

        /// <summary>
        /// Method to update  the task into the database
        /// </summary>
        /// <param name="item">Task which needs to be updated</param>
        public void Update(Task item)
        {
            using (TaskManagerContext db = new TaskManagerContext())
            {
                var itemToUpdate = db.Tasks.SingleOrDefault(task => task.Task_Id.Equals(item.Task_Id));
                if(itemToUpdate != null)
                {
                    itemToUpdate.Task_Name = item.Task_Name;
                    itemToUpdate.Parent_Task = item.Parent_Task;
                    itemToUpdate.Priority = item.Priority;
                    itemToUpdate.Start_Date = item.Start_Date;
                    itemToUpdate.End_Date = item.End_Date;
                    db.SaveChanges();
                }
            }
        }

        /// <summary>
        /// This is the method to end the existing task based on user interaction
        /// </summary>
        /// <param name="taskID">Task id which needs to be ended</param>
        public void EndTask(int taskID)
        {
            using (TaskManagerContext db = new TaskManagerContext())
            {
                var itemToEnd = db.Tasks.SingleOrDefault(task => task.Task_Id.Equals(taskID));
                if (itemToEnd != null)
                {
                    itemToEnd.Is_End_Task = true;
                    db.SaveChanges();
                }
            }
        }

        /// <summary>
        /// Method to delete the task based on the user interaction
        /// </summary>
        /// <param name="taskID">Task Id which needs to be deleted</param>
        public void DeleteTask(int taskID)
        {
            using (TaskManagerContext db = new TaskManagerContext())
            {
                var itemToDelete = db.Tasks.SingleOrDefault(task => task.Task_Id.Equals(taskID));
                if (itemToDelete != null)
                {
                    db.Tasks.Remove(itemToDelete);
                    db.SaveChanges();
                }
            }
        }

        /// <summary>
        /// Method to get all the existing tasks from database
        /// </summary>
        /// <returns>Returns list of task from databse</returns>
        public List<Task> GetAll()
        {
            List<Task> tasks = new List<Task>();
            using (TaskManagerContext db = new TaskManagerContext())
            {
                tasks = db.Tasks.ToList();
                return tasks;
            }
        }

        /// <summary>
        /// Method to get the task based on the user selection
        /// </summary>
        /// <param name="taskID">Task Id which needs to be returned</param>
        /// <returns>Returns the selected task details</returns>
        public Task GetTaskById(int taskID)
        {
            Task selectedItem = new Task();
            using (TaskManagerContext db = new TaskManagerContext())
            {
                selectedItem = db.Tasks.SingleOrDefault(task => task.Task_Id.Equals(taskID));
                if (selectedItem != null)
                {
                    return selectedItem;
                }
                return selectedItem;
            }
        }
    }
}
