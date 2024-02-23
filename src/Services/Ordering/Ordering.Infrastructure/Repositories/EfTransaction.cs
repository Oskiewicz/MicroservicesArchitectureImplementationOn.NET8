using Microsoft.EntityFrameworkCore.Storage;
using Ordering.Application.Contracts.Persistance;

namespace Ordering.Infrastructure.Repositories
{
    public class EfTransaction : ITransaction
    {
        private IDbContextTransaction _dbContextTransaction;

        public EfTransaction(IDbContextTransaction dbContextTransaction)
        {
            _dbContextTransaction = dbContextTransaction;
        }

        public Task CommitAsync(CancellationToken cancellationToken = default) => _dbContextTransaction.CommitAsync(cancellationToken);

        public Task RollbackAsync() => _dbContextTransaction.RollbackAsync();

        public void Dispose()
        {
            _dbContextTransaction?.Dispose();
            _dbContextTransaction = null!;
        }
    }
}