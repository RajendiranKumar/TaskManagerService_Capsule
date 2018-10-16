using System.Data.Entity;
using TaskManager.Entities;

namespace TaskManager.DataLayer
{
    /// <summary>
    /// This is the DB Context class to interact with database tables
    /// </summary>
    public class TaskManagerContext : DbContext
    {
        public TaskManagerContext():base("name = taskmanagerdbconn")
        {

        }

        public DbSet<Task> Tasks { get; set; }
    }
}
