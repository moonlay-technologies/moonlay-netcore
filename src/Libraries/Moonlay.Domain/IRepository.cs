namespace Moonlay.Domain
{
    public interface IRepository<T>
    {
        IUnitOfWork UnitOfWork { get; }
    }
}
