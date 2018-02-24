using System;

namespace FreeParkingSystem.Common.Services
{
    public interface IFailureServiceResult : IEmptyServiceResult
    {
        Exception Exception { get; }
    }

    public interface IFailureServiceResult<TEntity> : IFailureServiceResult, IEmptyServiceResult<TEntity>
    { }
}
