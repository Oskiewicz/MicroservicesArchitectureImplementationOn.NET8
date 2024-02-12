using Ordering.Domain.Common;

namespace Ordering.Application.Contracts.Persistance
{
    public interface IUnitOfWork : IDisposable
    {
        void Commit();
        void Rollback();
        IAsyncRepository<T> Repository<T>() where T : EntityBase;
    }
}