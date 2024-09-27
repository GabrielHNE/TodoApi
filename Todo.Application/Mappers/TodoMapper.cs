using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.NetworkInformation;
using System.Threading.Tasks;
using Todo.Domain.DTOs;
using Todo.Domain.Entities;

namespace Todo.Application.Mappers
{
    public static class TodoMapper
    {
        public static TodoDTO ToDTO(this TodoItem todo)
            => new () 
            {
                Id = todo.Id,
                Title = todo.Title,
                Description = todo.Description
            };

        public static TodoItem ToModel(this TodoDTO dto)
            => new()
            {
                Id = dto.Id,
                Title = dto.Title,
                Description = dto.Description
            };
    }
}