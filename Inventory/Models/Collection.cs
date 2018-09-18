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
        cmd.CommandText = @"SELECT * FROM collections;";
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
        return allItems;
      }

      public void Save()
       {
         MySqlConnection conn = DB.Connection();
         conn.Open();

         var cmd = conn.CreateCommand() as MySqlCommand;
         cmd.CommandText = @"INSERT INTO collections (name,quantity,age,cost) VALUES ("@name","@quantity","@age","@cost");";

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
         Id = cmd.LastInsertedId;     // This line is new!

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
        cmd.CommandText = @"DELETE FROM collections;";

        cmd.ExecuteNonQuery();

        conn.Close();
        if (conn != null)
        {
            conn.Dispose();
        }
      }
    }

  }
}
