using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc;
using Inventory.Models;
using System;

namespace Inventory.Controllers
{
    public class CollectionsController : Controller
    {
      [HttpGet("/collections")]
      public ActionResult ToIndex()
      {
        List<Collection> allCollections = Collection.GetAll();
        return View("Index", allCollections);
      }
      [HttpPost("/collections")]
      public ActionResult Add()
      {
        string name = Request.Form["name"];
        int quantity = int.Parse(Request.Form["quantity"]);
        int cost = int.Parse(Request.Form["cost"]);
        DateTime age = Convert.ToDateTime(Request.Form["age"]);
        Collection newCollection = new Collection(name, quantity, age, cost);
        newCollection.Save();
        return RedirectToAction("ToIndex");
      }
      [HttpPost("/collections/delete/{id}")]
      public ActionResult Remove(int id)
      {
        Collection.DeleteItem(id);
        return RedirectToAction("ToIndex");
      }

    }
}
