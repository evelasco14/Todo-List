using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TodoWebApp.Models
{
    public class TodoListViewModel
    {
        public List<TodoListItem> List { get; set; }
        public TodoListItem Item { get; set; }
    }
}
