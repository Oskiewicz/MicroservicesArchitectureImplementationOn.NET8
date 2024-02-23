using Ordering.Domain.Common;

namespace Ordering.Application.Contracts.Persistance
{
    public interface IUnitOfWork : IDisposable
    {
        IOrderRepository OrderRepository { get; set; }
        Task BeginTransactionAsync();
        Task CommitAsync(CancellationToken cancellationToken = default);
        int Commit();
        void Rollback();
    }
}