using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Todo.Domain.DTOs
{
    public class TodoDTO
    {
        public long Id { get; set; }
        
        [Required]
        [MaxLength(250)]
        public string Title { get; set; }
        
        [Required]
        [MaxLength(250)]
        public string Description { get; set; }
    }
}