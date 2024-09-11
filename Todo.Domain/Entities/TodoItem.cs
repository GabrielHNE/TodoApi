using System;
using System.Collections.Generic;
using System.Diagnostics.Contracts;
using System.Linq;
using System.Threading.Tasks;
using Todo.Domain.Models;

namespace Todo.Domain.Entities
{
    public class TodoItem : Entity
    {
        public string Title { get; set; }
        public string Description { get; set; }
        public long IdUser { get; set; }
        public User User { get; set; }
    }
}