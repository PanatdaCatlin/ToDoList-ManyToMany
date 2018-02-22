using Microsoft.AspNetCore.Mvc;
using ToDoList.Models;
using System.Collections.Generic;
using System;

namespace ToDoList.Controllers
{
    public class ItemsController : Controller
    {
//need?
        [HttpGet("/categories/{categoryId}/items/new")]
        public ActionResult CreateForm(int categoryId)
        {
          Dictionary<string, object> model = new Dictionary<string, object>();
          Category category = Category.Find(categoryId);
          return View(category);
        }
//need?
        [HttpGet("/categories/{categoryId}/items/{itemId}")]
        public ActionResult Details(int categoryId, int itemId)
        {
          Item item = Item.Find(itemId);
          Dictionary<string, object> model = new Dictionary<string, object>();
          Category category = Category.Find(categoryId);
          model.Add("item", item);
          model.Add("category", category);
          return View(item);
        }
        [HttpGet("/items")]
        public ActionResult Index()
        {
            List<Item> allItems = Item.GetAll();
            return View(allItems);
        }
//need?
        [HttpGet("/items/new")]
        public ActionResult CreateForm()
        {
            return View();
        }
        [HttpPost("/items")]
        public ActionResult Create()
        {
          Item newItem = new Item (Request.Form["item-description"],Int32.Parse(Request.Form["category-id"]));
          newItem.Save();
          List<Item> allItems = Item.GetAll();
          return RedirectToAction("Index");
        }
        // [HttpPost("/items/delete")]
        // public ActionResult DeleteAll(int id)
        // {
        //   Item thisItem = Item.Find(id);
        //   thisItem.Delete();
        //   return RedirectToAction("Index");
        // }
        [HttpGet("/items/{id}/delete")]
        public ActionResult DeleteOne(int id)
        {
          Item thisItem = Item.Find(id);
          thisItem.Delete();
          return RedirectToAction("details");
        }
//need?
        [HttpGet("/items/{id}")]
        public ActionResult Details(int id)
        {
            Item item = Item.Find(id);
            return View(item);
        }
        [HttpGet("/items/{id}/update")]
        public ActionResult UpdateForm(int id)
        {
            Item thisItem = Item.Find(id);
            return View("update", thisItem);
        }
        [HttpPost("/items/{id}/update")]
        public ActionResult Update(int id)
        {
          Item thisItem = Item.Find(id);
          thisItem.Edit(Request.Form["newname"]);
          return RedirectToAction("Index");
        }
    }
}
