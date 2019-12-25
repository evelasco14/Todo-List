using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TodoWebApp.Models
{
    public class TodoListItem
    {
        public int Id { get; set; }
        
        public string Text { get; set; }

        public string Timestamp { get; set; } 
    }
}
