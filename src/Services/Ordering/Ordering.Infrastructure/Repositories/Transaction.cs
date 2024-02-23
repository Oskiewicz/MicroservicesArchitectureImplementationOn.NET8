using Ordering.Application.Contracts.Persistance;

namespace Ordering.Infrastructure.Repositories
{
    public class Transaction : ITransaction
    {
        public Task CommitAsync(CancellationToken cancellationToken = default) => Task.CompletedTask;

        public Task RollbackAsync() => Task.CompletedTask;

        public void Dispose()
        {
        }
    }
}