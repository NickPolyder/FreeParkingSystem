using System;

namespace FreeParkingSystem.Common.Services.Models
{
    public class FailureServiceResult : IFailureServiceResult
    {
        public Exception Exception { get; }

        public FailureServiceResult(Exception ex)
        {
            Exception = ex;
        }
    }

    public class FailureServiceResult<TEntity> : FailureServiceResult, IFailureServiceResult<TEntity>
    {
        public FailureServiceResult(Exception ex) : base(ex)
        { }

        public FailureServiceResult(IFailureServiceResult result) : base(result?.Exception)
        {

        }
    }
}
