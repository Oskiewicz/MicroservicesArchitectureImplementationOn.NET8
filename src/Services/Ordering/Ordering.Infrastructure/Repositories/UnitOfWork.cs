using Microsoft.EntityFrameworkCore;
using Ordering.Application.Contracts.Persistance;
using Ordering.Infrastructure.Persistance;

namespace Ordering.Infrastructure.Repositories
{
    public class UnitOfWork : IUnitOfWork, IDisposable
    {
        public IOrderRepository OrderRepository { get; set; }
        private ITransaction? _currentTransaction;
        private readonly OrderContext _orderContext;
        private bool _disposed;
        public UnitOfWork(OrderContext orderContext)
        {
            _orderContext = orderContext;
            OrderRepository = new OrderRepository(orderContext);
        }
        public int Commit()
        {
            return _orderContext.SaveChanges();
        }

        public async Task BeginTransactionAsync()
        {
            if (_currentTransaction is not null)
                throw new InvalidOperationException("A transaction has already been started.");
            _currentTransaction = await _orderContext.BeginTransactionAsync();
        }

        public async Task CommitAsync(CancellationToken cancellationToken = default)
        {
            if (_currentTransaction is null)
                throw new InvalidOperationException("A transaction has not been started.");

            try
            {
                await _currentTransaction.CommitAsync(cancellationToken);
                _currentTransaction.Dispose();
                _currentTransaction = null;
            }
            catch (Exception)
            {
                if (_currentTransaction is not null)
                    await _currentTransaction.RollbackAsync();
                throw;
            }
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