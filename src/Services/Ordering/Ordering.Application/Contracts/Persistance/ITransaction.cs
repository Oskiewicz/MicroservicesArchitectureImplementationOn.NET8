namespace Ordering.Application.Contracts.Persistance
{
    public interface ITransaction : IDisposable
    {
        Task CommitAsync(CancellationToken cancellationToken = default);
        Task RollbackAsync();
    }
}