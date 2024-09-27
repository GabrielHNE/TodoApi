using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.AccessControl;
using System.Threading.Tasks;

namespace Todo.Application.Utilities
{
    public class PagedList<T> : List<T>
    {
        public int Page { get; protected set; }
        public int Limit { get; protected set; }
        public long Total { get; protected set; }

        
        PagedList(List<T> data, int total, int limit, int page)
        {
            Total = total;
            Limit = limit;
            Page = page;

            AddRange(data);
        }

        public static PagedList<T> ToPagedList(IQueryable<T> source, int pageNumber, int pageSize)
        {
            var count = source.Count();
            var items = source.Skip((pageNumber - 1) * pageSize).Take(pageSize).ToList();
            return new PagedList<T>(items, count, pageSize, pageNumber);
        }

        // Para coleções IEnumerable (não relacionadas a banco de dados)
        public static PagedList<T> ToPagedList(IEnumerable<T> source, int pageNumber, int pageSize)
        {
            var count = source.Count();
            var items = source.Skip((pageNumber - 1) * pageSize).Take(pageSize).ToList();
            return new PagedList<T>(items, count, pageSize, pageNumber);
        }
    }
}