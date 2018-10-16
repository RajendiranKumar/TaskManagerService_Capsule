using System;
using System.Web.Http;
using TaskManager.BusinessLayer;
using TaskManager.Entities;

namespace TaskManager.API.Controllers
{
    /// <summary>
    /// This is the API Controller which is used to interact the application with database
    /// </summary>
    public class TaskController : ApiController
    {
        /// <summary>
        /// This is the Get method to get all tasks
        /// </summary>
        /// <returns>Returns list of tasks from database</returns>
        [HttpGet, Route("GetAllTasks")]
        public IHttpActionResult GetAllTasks()
        {
            TaskLogic blTask = new TaskLogic();
            return Ok(blTask.GetAll());
        }

        /// <summary>
        /// This is the Get method  to get the selected  task
        /// </summary>
        /// <param name="taskId">Task Id</param>
        /// <returns>Selected Task Details</returns>
        [HttpGet, Route("GetTaskByID")]
        public IHttpActionResult GetTaskByID(int taskId)
        {
            TaskLogic blTask = new TaskLogic();
            var task = blTask.GetTaskById(taskId);
            if (task != null)
            {
                return Ok(task);
            }
            return NotFound();

        }

        /// <summary>
        /// This is the Post method to add the new task
        /// </summary>
        /// <param name="item">Task needs to be added</param>
        /// <returns></returns>
        [HttpPost, Route("AddTask")]
        public IHttpActionResult AddTask(Task item)
        {
            TaskLogic blTask = new TaskLogic();
            try
            {
                blTask.Add(item);
            }
            catch(Exception ex)
            {
                return Ok(ex.Message);
            }
            return Ok();
        }

        /// <summary>
        /// This is the Post method to update the existing task
        /// </summary>
        /// <param name="item">Task needs to be updated</param>
        /// <returns></returns>
        [HttpPost, Route("UpdateTask")]
        public IHttpActionResult UpdateTask(Task item)
        {
            TaskLogic blTask = new TaskLogic();
            try
            {
                blTask.Update(item);
            }
            catch (Exception ex)
            {
                return Ok(ex.Message);
            }
            return Ok();
        }

        /// <summary>
        /// This is the service method to end the task
        /// </summary>
        /// <param name="taskId">Task Id</param>
        /// <returns>Returns list of tasks</returns>
        [HttpGet, Route("EndTask")]
        public IHttpActionResult EndTask(int taskId)
        {
            TaskLogic blTask = new TaskLogic();
            try
            {
                blTask.EndTask(taskId);
            }
            catch (Exception ex)
            {
                return Ok(ex.Message);
            }
            return Ok(blTask.GetAll());
        }

        /// <summary>
        /// This is the service method to delete the task
        /// </summary>
        /// <param name="taskId">Task Id</param>
        /// <returns>Returns list of available tasks</returns>
        [HttpGet, Route("DeleteTask")]
        public IHttpActionResult DeleteTask(int taskId)
        {
            TaskLogic blTask = new TaskLogic();
            try
            {
                blTask.DeleteTask(taskId);
            }
            catch (Exception ex)
            {
                return Ok(ex.Message);
            }
            return Ok(blTask.GetAll());
        }
    }
}
