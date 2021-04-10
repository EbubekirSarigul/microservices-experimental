namespace MicroserviceTraining.Framework.Data.Interface
{
    public interface IRepository
    {
        IUnitOfWork UnitOfWork { get; }
    }
}
