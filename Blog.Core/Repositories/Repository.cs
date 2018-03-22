using System.Collections.Generic;
using System.Linq;
using Blog.Core.Models;
using NHibernate;

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

        public void AddOrUpdate(T entity)
        {
            using (var transaction = Session.BeginTransaction())
            {
                Session.SaveOrUpdate(entity);
                transaction.Commit();
            }
        }

        public void Delete(int id)
        {
            using (var transaction = Session.BeginTransaction())
            {
                var entity = GetById(id);
                Session.Delete(entity);
                transaction.Commit();
            }
        }
    }
}