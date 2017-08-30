namespace FreeParkingSystem.Common.Services
{
    public interface IEmptyServiceResult : IServiceResult
    { }

    public interface IEmptyServiceResult<TEntity> : IEmptyServiceResult, IServiceResult<TEntity>
    { }
}
