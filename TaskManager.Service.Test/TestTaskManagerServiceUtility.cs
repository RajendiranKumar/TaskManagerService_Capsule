using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using NUnit.Framework;
using System.Web.Http;
using TaskManager.Entities;
using TaskManager.API;
using TaskManager.API.Controllers;
using System.Web.Http.Results;

namespace TaskManager.Service.Test
{
    [TestFixture]
    public class TestTaskManagerServiceUtility
    {
        [Test]
        public void TestAddService()
        {
            Entities.Task itemToAdd = new Entities.Task();

            itemToAdd.Task_Name = "TestName_Nunit_1";
            itemToAdd.Parent_Task = "TestParent_Nunit_1";
            itemToAdd.Priority = 4;
            itemToAdd.Start_Date = DateTime.Now;
            itemToAdd.End_Date = DateTime.Now.AddDays(15);
            itemToAdd.Is_End_Task = false;

            var controller = new TaskController();
            var actionResult = controller.GetAllTasks() as OkNegotiatedContentResult<List<Task>>;
            int actualCount = actionResult.Content.Count + 1;
            controller.AddTask(itemToAdd);
            actionResult = controller.GetAllTasks() as OkNegotiatedContentResult<List<Task>>;
            int expectedCount = actionResult.Content.Count;
            Assert.AreEqual(actualCount, expectedCount);
        }

        [Test]
        public void TestUpdateService()
        {
            bool actualResult = true;
            bool expectedResult = true;
            //Entities.Task itemToUpdate = new Entities.Task();

            var controller = new TaskController();
            var actionResult = controller.GetAllTasks() as OkNegotiatedContentResult<List<Task>>;

            var itemToUpdate = actionResult.Content.FirstOrDefault();

            itemToUpdate.Task_Name = "UpdatedTaskName_Nunit";
            controller.UpdateTask(itemToUpdate);

            var updatedItem = controller.GetTaskByID(itemToUpdate.Task_Id) as OkNegotiatedContentResult<Task>;

            if (!updatedItem.Content.Task_Name.Equals("UpdatedTaskName_Nunit"))
            {
                actualResult = false;
            }

            Assert.AreEqual(actualResult, expectedResult);
        }

        [Test]
        public void TestEndTaskService()
        {
            bool actualResult = false;
            bool expectedResult = true;
            Entities.Task itemToEnd = new Entities.Task();

            var controller = new TaskController();
            var actionResult = controller.GetAllTasks() as OkNegotiatedContentResult<List<Task>>;

            itemToEnd = actionResult.Content.Where(item => item.Is_End_Task.Equals(false)).FirstOrDefault();
            if (itemToEnd != null)
            {
                controller.EndTask(itemToEnd.Task_Id);
                actionResult = controller.GetAllTasks() as OkNegotiatedContentResult<List<Task>>;
                itemToEnd = actionResult.Content.Where(item => item.Task_Id.Equals(itemToEnd.Task_Id)).FirstOrDefault();
                if (itemToEnd.Is_End_Task ?? true)
                {
                    actualResult = true;
                }
            }
            Assert.AreEqual(actualResult, expectedResult);

        }

        [Test]
        public void TestDeleteTaskService()
        {
            bool actualResult = false;
            bool expectedResult = true;
            Entities.Task itemToDelete = new Entities.Task();

            var controller = new TaskController();
            var actionResult = controller.GetAllTasks() as OkNegotiatedContentResult<List<Task>>;

            itemToDelete = actionResult.Content.FirstOrDefault();

            controller.DeleteTask(itemToDelete.Task_Id);

            var deletedItem = controller.GetTaskByID(itemToDelete.Task_Id) as OkNegotiatedContentResult<Task>;
            if (deletedItem == null)
            {
                actualResult = true;
            }
            Assert.AreEqual(actualResult, expectedResult);
        }

        [Test]
        public void TestGetAllService()
        {
            bool actualResult = false;
            bool expectedResult = true;

            var controller = new TaskController();
            var actionResult = controller.GetAllTasks() as OkNegotiatedContentResult<List<Task>>;
            if (actionResult.Content.Count > 0)
            {
                actualResult = true;
            }
            Assert.AreEqual(actualResult, expectedResult);
        }

        [Test]
        public void TestGetTaskByIdService()
        {
            bool actualResult = false;
            bool expectedResult = true;
            Entities.Task actualTask = new Entities.Task();
            //Entities.Task expectedTask = new Entities.Task();

            var controller = new TaskController();
            var actionResult = controller.GetAllTasks() as OkNegotiatedContentResult<List<Task>>;

            actualTask = actionResult.Content.FirstOrDefault();
            var expectedTask = controller.GetTaskByID(actualTask.Task_Id) as OkNegotiatedContentResult<Task>;
            if (actualTask.Task_Id.Equals(expectedTask.Content.Task_Id))
            {
                actualResult = true;
            }
            Assert.AreEqual(actualResult, expectedResult);
        }
    }
}
