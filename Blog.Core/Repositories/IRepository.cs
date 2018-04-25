using Blog.Core.Models;
using System.Collections.Generic;

namespace Blog.Core.Repositories
{
    public interface IRepository<T> where T : IKeyedEntity
    {
        T GetById(int id);
        IEnumerable<T> GetAll();
        void AddOrUpdate(T entity);
        void Delete(int id);
    }
}