using System.Collections.Generic;
using Blog.Core.Models;

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