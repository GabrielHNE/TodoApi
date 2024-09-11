using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Todo.Domain.Entities
{
    public abstract class Entity
    {
        public long Id { get; protected set; }
    }
}