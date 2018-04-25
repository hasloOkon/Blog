using Blog.Core.Aspects;
using Blog.Core.Models;
using NHibernate;
using NHibernate.Linq;
using System.Collections.Generic;
using System.Linq;

namespace Blog.Core.Repositories
{
    public abstract class Repository<T> : IRepository<T> where T : IKeyedEntity
    {
        protected readonly ISession Session;

        protected Repository(ISession session)
        {
            this.Session = session;
        }

        public T GetById(int id)
        {
            return Session.Query<T>().First(entity => entity.Id == id);
        }

        public IEnumerable<T> GetAll()
        {
            return Session.Query<T>();
        }

        [Transaction]
        public void AddOrUpdate(T entity)
        {
            Session.SaveOrUpdate(entity);
        }

        [Transaction]
        public void Delete(int id)
        {
            var entity = GetById(id);
            Session.Delete(entity);
        }
    }
}