using Microsoft.EntityFrameworkCore;
using Ordering.Application.Contracts.Persistance;
using Ordering.Domain.Common;
using Ordering.Infrastructure.Persistance;

namespace Ordering.Infrastructure.Repositories
{
    public class UnitOfWork : IUnitOfWork, IDisposable
    {
        private readonly OrderContext _orderContext;
        private bool _disposed;
        public UnitOfWork(OrderContext orderContext)
        {
            _orderContext = orderContext;
        }
        public void Commit()
        {
            _orderContext.SaveChanges();
        }
        public void Rollback()
        {
            foreach (var entry in _orderContext.ChangeTracker.Entries())
            {
                switch (entry.State)
                {
                    case EntityState.Added:
                        entry.State = EntityState.Detached;
                        break;
                }
            }
        }
        public IAsyncRepository<T> Repository<T>() where T : EntityBase
        {
            return new RepositoryBase<T>(_orderContext);
        }
        private bool disposed = false;
        protected virtual void Dispose(bool disposing)
        {
            if (!this.disposed)
            {
                if (disposing)
                {
                    _orderContext.Dispose();
                }
            }
            this.disposed = true;
        }
        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }
    }
}