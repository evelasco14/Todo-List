using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using TodoWebApp.Models;

namespace TodoWebApp.Controllers
{
    public class TodoController : Controller
    {

        private readonly ITodoListService _todoListService;

        public TodoController(ITodoListService todoListService)
        {
            _todoListService = todoListService;
        }


        [HttpGet]
        public IActionResult Index()
        {
            var model = new TodoListViewModel { List = _todoListService.GetList() };

            return View(model);
        }

        [HttpGet]
        public IActionResult Create()
        {

            return View("Create");
        }


        //Get edit
        public ActionResult Edit(int? id)
        {

            int i = id ?? 0;       
            TodoListItem toDo = _todoListService.GetList(i);
            if (toDo == null)
            {
                return null;
            }
            return View(toDo);
        }


        //Post edit
        [HttpPost]
        [ActionName("Edit")]      
        public ActionResult EditPost(int? id, string newText)
        {
            int i = id ?? 0;
            TodoListItem toDo = _todoListService.GetList(i);

            toDo.Text = newText;    //Set new text
            toDo.Timestamp = DateTime.Now.ToString();  //Set datetime now on save

            return RedirectToAction("Index");       //Redirect after changes made
        }

        [HttpPost]
        [ActionName("Create")]
        public ActionResult Create(string newText)
        {

            _todoListService.CreateTodoListItem(newText);

            return RedirectToAction("Index");       //Redirect after changes made
        }
    }
}
