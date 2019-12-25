using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Hosting;
using TodoWebApp.Models;
using System.IO;

namespace TodoWebApp
{
    public interface ITodoListService
    {
        List<TodoListItem> GetList();
        TodoListItem GetList(int id);
        void CreateTodoListItem(string newText);
    }

    public class TodoListService : ITodoListService
    {
        private readonly List<TodoListItem> _items = new List<TodoListItem>();
        private readonly IHostingEnvironment _base;
        private int NextId { get; set; }

        public TodoListService(IHostingEnvironment env)
        {

            _base = env;      
            _items = ReadFromFile();
            NextId = _items.Count + 1;
        }

     
        public void WriteToFile()
        {
            using (StreamWriter _wFile = new StreamWriter(_base.WebRootPath + "/data.txt"))
            {
                
                _items.ForEach(element =>
                {
                    
                    int id = element.Id;
                    string text = element.Text;
                    string dateStr = element.Timestamp ?? "";    
                    _wFile.WriteLine(id + "|" + text + "|" + dateStr);
                });
            }

        }

        public void CreateTodoListItem(string newText)
        {
            _items.Add(new TodoListItem { Id = NextId, Text = newText });
            WriteToFile();
            NextId++;
        }

        private List<TodoListItem> ReadFromFile()
        {
            
            using (StreamReader _rFile = new StreamReader(_base.WebRootPath + "/data.txt"))
            {
                string readAll = _rFile.ReadToEnd();     
                string[] allItems = readAll.Split("\n");    

                
                for (int i = 0; i < allItems.Length; i++)
                {
                    
                    string[] each = allItems[i].Split("|");     
                    if (each.Length == 1) { continue; }        
                    for (int j = 0; j < each.Length; j++)
                    {
                        each[j] = each[j].Trim();       
                    }
                                       
                    int id = int.Parse(each[0]);
                    string text = each[1];
                    string dateStr = each[2] == "" ? null : each[2];
                    _items.Add(new TodoListItem { Id = id, Text = text, Timestamp = dateStr });
                }
            }
            return _items;
        }

       
        public List<TodoListItem> GetList() => _items;

        public TodoListItem GetList(int id) => _items.Find(e => e.Id == id);
    }
}
