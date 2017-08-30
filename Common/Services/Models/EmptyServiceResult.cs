namespace FreeParkingSystem.Common.Services.Models
{
    public class EmptyServiceResult : IEmptyServiceResult
    { }

    public class EmptyServiceResult<TEntity> : EmptyServiceResult, IEmptyServiceResult<TEntity>
    { }
}
