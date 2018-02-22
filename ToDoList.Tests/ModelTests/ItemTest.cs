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

    // failed because the change in GETALL()????????
    [TestMethod]
    public void GetAll_ReturnsItems_ItemList()
    {
      //Arrange
      string description01 = "Walk the dog";
      string description02 = "Wash the dishes";
      Item newItem1 = new Item(description01);
      Item newItem2 = new Item(description02);
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
    public void Dispose()
    {
      Item.ClearAll();
    }
  }
}
