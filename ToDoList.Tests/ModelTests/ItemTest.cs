using Microsoft.VisualStudio.TestTools.UnitTesting;
using ToDoList.Models;
using System.Collections.Generic;
using MySql.Data.MySqlClient;
using System;

namespace ToDoList.Tests
{
  [TestClass]
  public class ItemTest: IDisposable
  {
    public ItemTest()
    {
      DBConfiguration.ConnectionString = "server=localhost;user id=root;password=root;port=8889;database=todo_test;";
    }
    public void Dispose()
    {
      Item.DeleteAll();
      Category.DeleteAll();
    }
    [TestMethod]
    public void GetDescription_ReturnsDescription_String()
    {
      //Arrange
      string description = "Walk the dog.";
      Item newItem = new Item(description);

      //Act
      string result = newItem.GetDescription();

      //Assert
      Assert.AreEqual(description, result);
    }

    [TestMethod]
    public void GetAll_ReturnsItems_ItemList()
    {
      //Arrange
      string description01 = "Walk the dog";
      string description02 = "Wash the dishes";
      Item newItem1 = new Item(description01);
      newItem1.Save();
      Item newItem2 = new Item(description02);
      newItem2.Save();
      List<Item> newList = new List<Item> { newItem1, newItem2 };

      //Act
      List<Item> result = Item.GetAll();
// Code to help print list and debug
      foreach (Item thisItem in result)
      {
        Console.WriteLine("Output: " + thisItem.GetDescription());
      }

      //Assert
      CollectionAssert.AreEqual(newList, result);
    }
    [TestMethod]
    public void GetAll_DatabaseEmptyAtFirst_0()
    {
      //Arrange, Act
      int result = Item.GetAll().Count;

      //Assert
      Assert.AreEqual(0, result);
    }
    [TestMethod]
    public void Equals_ReturnsTrueIfDescriptionsAreTheSame_Item()
    {
      // Arrange, Act
      Item firstItem = new Item("Mow the lawn",1);
      Item secondItem = new Item("Mow the lawn",1);

      // Assert
      Assert.AreEqual(firstItem, secondItem);
    }
    [TestMethod]
    public void Save_SavesToDatabase_ItemList()
    {
      //Arrange
      Item testItem = new Item("Mow the lawn",1);

      //Act
      testItem.Save();
      List<Item> result = Item.GetAll();
      List<Item> testList = new List<Item>{testItem};

      //Assert
      CollectionAssert.AreEqual(testList, result);
    }
    [TestMethod]
    public void Save_AssignsIdToObject_Id()
    {
      //Arrange
      Item testItem = new Item("Mow the lawn",1);

      //Act
      testItem.Save();
      Item savedItem = Item.GetAll()[0];

      int result = savedItem.GetId();
      int testId = testItem.GetId();

      //Assert
      Assert.AreEqual(testId, result);
    }
    [TestMethod]
    public void Find_FindsItemInDatabase_Item()
    {
      //Arrange
      Item testItem = new Item("Mow the lawn",1);
      testItem.Save();

      //Act
      Item foundItem = Item.Find(testItem.GetId());

      //Assert
      Assert.AreEqual(testItem, foundItem);
    }
    [TestMethod]
    public void Edit_UpdatesItemInDatabase_String()
    {
      //Arrange
      Item testItem = new Item ("Walk the Dog", 1,1);
      // Item testItem = new Item(firstDescription, 1);
      testItem.Save();
      string secondDescription = "Mow the lawn";

      //Act
      testItem.Edit(secondDescription);

      string result = Item.Find(testItem.GetId()).GetDescription();

      //Assert
      Assert.AreEqual(secondDescription , result);
    }
    [TestMethod]
    public void Delete_DeletesItemInDatabase_Void()
    {
      //Arrange
      string firstDescription = "Walk the Dog";
      Item testItem = new Item(firstDescription,1,2);
      testItem.Save();
      string secondDescription = "Mow the lawn";
      Item testItem2 = new Item(secondDescription,1,3);
      testItem2.Save();
      //Act
      testItem.Delete();
      List<Item> expected = new List<Item> {testItem2};
      List<Item> result = Item.GetAll();

      //Assert
      CollectionAssert.AreEqual(expected, result);
    }
  }
}
