using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NUnit.Framework;
using TaskManager.BusinessLayer;
using TaskManager.Entities;

namespace TaskManager.Test
{
    [TestFixture]
    public class TestTaskManagerUtility
    {
        [Test]
        [Description("Method to Test adding the task details to the database")]
        public void TestAdd()
        {
            Entities.Task itemToAdd = new Entities.Task();

            itemToAdd.Task_Name = "TestName_Nunit_1";
            itemToAdd.Parent_Task = "TestParent_Nunit_1";
            itemToAdd.Priority = 4;
            itemToAdd.Start_Date = DateTime.Now;
            itemToAdd.End_Date = DateTime.Now.AddDays(15);
            itemToAdd.Is_End_Task = false;

            TaskLogic businessObject = new TaskLogic();
            int actualCount = businessObject.GetAll().Count + 1;
            businessObject.Add(itemToAdd);
            int expectedCount = businessObject.GetAll().Count;
            Assert.AreEqual(actualCount, expectedCount);
        }

        [Test]
        public void TestUpdate()
        {
            bool actualResult = true;
            bool expectedResult = true;
            Entities.Task itemToUpdate = new Entities.Task();
            
            TaskLogic businessObject = new TaskLogic();
            List<Entities.Task> taskList = new List<Entities.Task>();
            taskList = businessObject.GetAll();

            itemToUpdate = taskList.FirstOrDefault();

            itemToUpdate.Task_Name = "UpdatedTaskName_Nunit";
            businessObject.Update(itemToUpdate);

            itemToUpdate = businessObject.GetTaskById(itemToUpdate.Task_Id);

            if(!itemToUpdate.Task_Name.Equals("UpdatedTaskName_Nunit"))
            {
                actualResult = false;
            }

            itemToUpdate.Parent_Task = "UpdatedParentTask_Nunit";
            businessObject.Update(itemToUpdate);
            itemToUpdate = businessObject.GetTaskById(itemToUpdate.Task_Id);
            if(!itemToUpdate.Parent_Task.Equals("UpdatedParentTask_Nunit"))
            {
                actualResult = false;
            }

            Assert.AreEqual(actualResult, expectedResult);
        }

        [Test]
        public void TestEndTask()
        {
            bool actualResult = false;
            bool expectedResult = true;
            Entities.Task itemToEnd = new Entities.Task();

            TaskLogic businessObject = new TaskLogic();
            List<Entities.Task> taskList = new List<Entities.Task>();
            taskList = businessObject.GetAll();

            itemToEnd = taskList.Where(item => item.Is_End_Task.Equals(false)).FirstOrDefault();
            if(itemToEnd != null)
            {
                businessObject.EndTask(itemToEnd.Task_Id);
                taskList = businessObject.GetAll();
                itemToEnd = taskList.Where(item => item.Task_Id.Equals(itemToEnd.Task_Id)).FirstOrDefault();
                if(itemToEnd.Is_End_Task ?? true)
                {
                    actualResult = true;
                }
            }
            Assert.AreEqual(actualResult, expectedResult);

        }

        [Test]
        public void TestDeleteTask()
        {
            bool actualResult = false;
            bool expectedResult = true;
            Entities.Task itemToDelete = new Entities.Task();

            TaskLogic businessObject = new TaskLogic();
            List<Entities.Task> taskList = new List<Entities.Task>();
            taskList = businessObject.GetAll();

            itemToDelete = taskList.FirstOrDefault();

            businessObject.DeleteTask(itemToDelete.Task_Id);

            itemToDelete = businessObject.GetTaskById(itemToDelete.Task_Id);
            if(itemToDelete == null)
            {
                actualResult = true;
            }
            Assert.AreEqual(actualResult, expectedResult);
        }

        [Test]
        public void TestGetAll()
        {
            bool actualResult = false;
            bool expectedResult = true;

            TaskLogic businessObject = new TaskLogic();
            List<Entities.Task> taskList = new List<Entities.Task>();
            taskList = businessObject.GetAll();
            if(taskList.Count > 0)
            {
                actualResult = true;
            }
            Assert.AreEqual(actualResult, expectedResult);
        }

        [Test]
        public void TestGetTaskById()
        {
            bool actualResult = false;
            bool expectedResult = true;
            Entities.Task actualTask = new Entities.Task();
            Entities.Task expectedTask = new Entities.Task();

            TaskLogic businessObject = new TaskLogic();
            List<Entities.Task> taskList = new List<Entities.Task>();
            taskList = businessObject.GetAll();

            actualTask = taskList.FirstOrDefault();
            expectedTask = businessObject.GetTaskById(actualTask.Task_Id);
            if(actualTask.Task_Id.Equals(expectedTask.Task_Id))
            {
                actualResult = true; 
            }
            Assert.AreEqual(actualResult, expectedResult);
        }
    }
}
