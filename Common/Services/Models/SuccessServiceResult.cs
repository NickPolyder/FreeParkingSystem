namespace FreeParkingSystem.Common.Services.Models
{
    public class SuccessServiceResult : ISuccessServiceResult
    { }

    public class SuccessServiceResult<TEntity> : SuccessServiceResult, ISuccessServiceResult<TEntity>
    {
        public TEntity Value { get; }

        public SuccessServiceResult(TEntity value)
        {
            Value = value;
        }
    }
}
