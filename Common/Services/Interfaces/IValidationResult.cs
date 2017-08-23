using System;
using System.Collections.Generic;

namespace FreeParkingSystem.Common.Services
{
    public interface IValidationResult
    {
        bool IsValid { get; }

        IEnumerable<Exception> Errors { get; }
    }
}