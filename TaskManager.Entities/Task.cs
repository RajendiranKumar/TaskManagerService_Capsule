using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace TaskManager.Entities
{
    /// <summary>
    /// This is the entity class for Task
    /// </summary>
    [Table("[dbo].[Task_Table]")]
    public class Task
    {
        [Key]
        public int Task_Id { get; set; }
        public string Task_Name { get; set; }
        public string Parent_Task { get; set; }
        public DateTime Start_Date { get; set; }
        public DateTime End_Date { get; set; }
        public int Priority { get; set; }
        public bool? Is_End_Task { get; set; }
    }
}
