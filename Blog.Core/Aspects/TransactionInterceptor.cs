using NHibernate;
using Ninject.Extensions.Interception;
using System;
using IInterceptor = Ninject.Extensions.Interception.IInterceptor;

namespace Blog.Core.Aspects
{
    public class TransactionInterceptor : IInterceptor
    {
        private readonly ISession session;

        public TransactionInterceptor(ISession session)
        {
            this.session = session;
        }

        public void Intercept(IInvocation invocation)
        {
            var transaction = BeginTransaction();
            try
            {
                invocation.Proceed();
                transaction.Commit();
            }
            catch (Exception)
            {
                transaction.Rollback();
                throw;
            }
            finally
            {
                transaction.Dispose();
            }
        }

        private ITransaction BeginTransaction()
        {
            var shouldBeginNewTransaction = session.Transaction == null || !session.Transaction.IsActive;

            return shouldBeginNewTransaction
                ? session.BeginTransaction()
                : new EmptyTransaction();
        }
    }
}