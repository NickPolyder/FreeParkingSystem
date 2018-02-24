namespace FreeParkingSystem.Common.Services
{
    public interface ISuccessServiceResult : IServiceResult
    { }

    public interface ISuccessServiceResult<TEntity> : ISuccessServiceResult, IServiceResult<TEntity>
    {
        TEntity Value { get; }
    }
}
