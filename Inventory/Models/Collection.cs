using System;
using System.Collections.Generic;
using MySql.Data.MySqlClient;
using Inventory;

namespace Inventory.Models
{
    public class Collection
    {
      public string Name {get; set;}
      public int Quantity {get; set;}
      public DateTime Age {get; set;}
      public int Cost {get; set;}
      public int Id {get; set;}

      public Collection(string name)
      {
        Name = name;
      }
      public Collection(string name, int quantity, DateTime age, int cost, int id = 0)
      {
        Name = name;
        Quantity = quantity;
        Age = age;
        Cost = cost;
        Id = id;
      }

      public static List<Collection> GetAll()
      {
        List<Collection> allCollections = new List<Collection>{};
        MySqlConnection conn = DB.Connection();
        conn.Open();
        MySqlCommand cmd = conn.CreateCommand() as MySqlCommand;
        cmd.CommandText = @"SELECT * FROM inventory ORDER BY name ASC;";
        MySqlDataReader rdr = cmd.ExecuteReader() as MySqlDataReader;
        while(rdr.Read())
        {
          int id = rdr.GetInt32(0);
          string name = rdr.GetString(1);
          int quantity = rdr.GetInt32(2);
          DateTime age = rdr.GetDateTime(3);
          int cost = rdr.GetInt32(4);
          Collection newCollection = new Collection(name,quantity,age,cost,id);
          allCollections.Add(newCollection);
        }
          conn.Close();
          if (conn != null)
          {
              conn.Dispose();
          }
          return allCollections;
      }

      public static Collection Find(int id)
      {
          MySqlConnection conn = DB.Connection();
          conn.Open();

          var cmd = conn.CreateCommand() as MySqlCommand;
          cmd.CommandText = @"SELECT * FROM `inventory` WHERE id = @thisId;";

          MySqlParameter thisId = new MySqlParameter();
          thisId.ParameterName = "@thisId";
          thisId.Value = id;
          cmd.Parameters.Add(thisId);

          var rdr = cmd.ExecuteReader() as MySqlDataReader;

          int collectionId = 0;
          string collectionName = "";
          int collectionQuantity = 0;
          DateTime collectionAge = Convert.ToDateTime("2018-02-02");
          int collectionCost = 0;

          while (rdr.Read())
          {
              collectionId = rdr.GetInt32(0);
              collectionName = rdr.GetString(1);
              collectionQuantity = rdr.GetInt32(2);
              collectionAge = rdr.GetDateTime(3);
              collectionCost = rdr.GetInt32(4);
          }

          Collection foundCollection= new Collection(collectionName, collectionQuantity, collectionAge, collectionCost, collectionId);

           conn.Close();
           if (conn != null)
           {
               conn.Dispose();
           }

          return foundCollection;  // This line is new!

      }

      public override bool Equals(System.Object otherCollection)
      {
        if (!(otherCollection is Collection))
        {
          return false;
        }
        else
        {
          Collection newCollection = (Collection) otherCollection;
          bool idEquality = (this.Id == newCollection.Id);
          bool nameEquality = (this.Name == newCollection.Name);
          bool quantityEquality = (this.Quantity == newCollection.Quantity);
          bool ageEquality = (this.Age == newCollection.Age);
          bool costEquality = (this.Cost == newCollection.Cost);
          return (nameEquality && idEquality && quantityEquality && ageEquality && costEquality);
        }
      }


      public void Save()
       {
         MySqlConnection conn = DB.Connection();
         conn.Open();

         var cmd = conn.CreateCommand() as MySqlCommand;
         cmd.CommandText = @"INSERT INTO inventory (name,quantity,age,cost) VALUES (@name,@quantity,@age,@cost);";

         MySqlParameter name = new MySqlParameter();
         name.ParameterName = "@name";
         name.Value = this.Name;
         cmd.Parameters.Add(name);

         MySqlParameter quantity = new MySqlParameter();
         quantity.ParameterName = "@quantity";
         quantity.Value = this.Quantity;
         cmd.Parameters.Add(quantity);

         MySqlParameter age = new MySqlParameter();
         age.ParameterName = "@age";
         age.Value = this.Age;
         cmd.Parameters.Add(age);

         MySqlParameter cost = new MySqlParameter();
         cost.ParameterName = "@cost";
         cost.Value = this.Cost;
         cmd.Parameters.Add(cost);

         cmd.ExecuteNonQuery();
         Id = (int) cmd.LastInsertedId;     // This line is new!

          conn.Close();
          if (conn != null)
          {
              conn.Dispose();
          }
       }

      public static void DeleteItem(int recordId)
      {
        MySqlConnection conn = DB.Connection();
        conn.Open();

        var cmd = conn.CreateCommand() as MySqlCommand;
        cmd.CommandText = @"DELETE FROM inventory WHERE id = " + recordId + ";";

        cmd.ExecuteNonQuery();

        conn.Close();
        if (conn != null)
        {
            conn.Dispose();
        }
      }

      public static void DeleteAll()
      {
        MySqlConnection conn = DB.Connection();
        conn.Open();

        var cmd = conn.CreateCommand() as MySqlCommand;
        cmd.CommandText = @"DELETE FROM inventory;";

        cmd.ExecuteNonQuery();

        conn.Close();
        if (conn != null)
        {
            conn.Dispose();
        }
      }
    }

  }
