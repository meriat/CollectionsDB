using Microsoft.VisualStudio.TestTools.UnitTesting;
using Inventory.Models;
using System;
using MySql.Data.MySqlClient;
using System.Collections.Generic;


namespace Inventory.Tests
{
  [TestClass]
  public class CollectionTests : IDisposable
  {
    public void Dispose()
    {
      Collection.DeleteAll();
    }
    public CollectionTests()
    {
      DBConfiguration.ConnectionString = "server=localhost;user id=root;password=root;port=8889;database=collections_test;";
    }
    [TestMethod]
    public void GetAll_DbStartsEmpty_0()
    {
      //Arrange
      //Act
      int result = Collection.GetAll().Count;
      Console.WriteLine("result" + result);
      //Assert
      Assert.AreEqual(0, result);
    }

    [TestMethod]
    public void Save_DbCompareObjects_Equal()
    {

      Collection collection1 = new Collection("Toys", 5, Convert.ToDateTime("1999-02-02"), 100);
      collection1.Save();

      Collection test = Collection.GetAll()[0];

      Assert.AreEqual("Toys" ,test.Name);
    }

    [TestMethod]
    public void Find_FindsItemInDatabase_Item()
    {
      //Arrange
      Collection testCollection = new Collection("Toys", 5, Convert.ToDateTime("1999-02-02"), 100);
      testCollection.Save();

      //Act
      Collection foundCollection = Collection.Find(testCollection.Id);

      //Assert
      Assert.AreEqual(testCollection, foundCollection);
    }
  }
}
