namespace Ordering.Application.Contracts.Persistance
{
    public interface IDatabase
    {
        Task<ITransaction> BeginTransactionAsync();
    }
}