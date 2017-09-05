using System;
using FreeParkingSystem.Common.Services.Models;

namespace FreeParkingSystem.Common.Services.Helpers
{
    public static class ServiceResult
    {

        #region Returns

        public static IServiceResult<TEntity> Return<TEntity>() => new EmptyServiceResult<TEntity>();

        public static IServiceResult Return() => new EmptyServiceResult();

        public static IServiceResult<TEntity> Return<TEntity>(TEntity value) => value == null ? Return<TEntity>() : new SuccessServiceResult<TEntity>(value);

        public static IServiceResult<TEntity> Return<TEntity>(IServiceResult<TEntity> result) => result;

        public static IServiceResult Return(IServiceResult result) => result;

        public static IServiceResult<TEntity> Return<TEntity>(Exception ex) => new FailureServiceResult<TEntity>(ex);

        public static IServiceResult Return(Exception ex) => new FailureServiceResult(ex);

        public static IFailureServiceResult<T> Return<T>(IFailureServiceResult failure) => new FailureServiceResult<T>(failure);

        #endregion

        #region Wrappers

        /// <summary>
        ///     Executes the <paramref name="function" /> and returns the result in an <see cref="IServiceResult{TEntity}" />.
        ///     If the return value is null, returns a <see cref="IEmptyServiceResult" />. If an exception is thrown, returns a
        ///     <see cref="IFailureServiceResult" />.
        /// </summary>
        /// <typeparam name="TEntity"></typeparam>
        /// <param name="function">The function to be wrapped in a result.</param>
        /// <param name="exceptionHandler">
        ///     The exception handler. This function allows the caller to provide the
        ///     <see cref="IFailureServiceResult" /> with a custom exception.
        /// </param>
        /// <returns></returns>
        public static IServiceResult<TEntity> Wrap<TEntity>(Func<TEntity> function, Func<Exception, Exception> exceptionHandler = null)
        {
            try
            {

                return function == null ? Return<TEntity>() : Return(function());
            }
            catch (Exception e)
            {
                return Wrap<TEntity>(exceptionHandler, e);
            }
        }

        /// <summary>
        ///     Executes the <paramref name="action" /> and returns an <see cref="IServiceResult" />.
        ///     If an exception is thrown, returns a <see cref="IFailureServiceResult" />.
        /// </summary>
        /// <param name="action">The function to be wrapped in a result.</param>
        /// <param name="exceptionHandler">
        ///     The exception handler. This function allows the caller to provide the
        ///     <see cref="IFailureServiceResult" /> with a custom exception.
        /// </param>
        /// <returns></returns>
        public static IServiceResult<bool> Wrap(Action action, Func<Exception, Exception> exceptionHandler = null)
        {
            try
            {
                action();
                return Return(true);
            }
            catch (Exception e)
            {
                return Wrap<bool>(exceptionHandler, e);
            }
        }

        internal static IServiceResult<TEntity> Wrap<TEntity>(Func<IServiceResult<TEntity>> function, Func<Exception, Exception> exceptionHandler = null)
        {
            try
            {
                return Return(function());
            }
            catch (Exception e)
            {
                return Wrap<TEntity>(exceptionHandler, e);
            }
        }

        private static IServiceResult<TEntity> Wrap<TEntity>(Func<Exception, Exception> exceptionHandler, Exception ex)
        {
            var handler = exceptionHandler ?? (e => e);
            try
            {
                return Return<TEntity>(handler(ex));
            }
            catch (Exception e)
            {
                return Return<TEntity>(new ServiceResultException("See Inner Exception", e));
            }
        }

        #endregion

        #region Extensions

        /// <summary>
        ///     Invokes a function on an <see cref="IServiceResult{T}" /> that itself yields an <see cref="IServiceResult{TOut}" />.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <typeparam name="TOut">The type of the out.</typeparam>
        /// <param name="result">The result.</param>
        /// <param name="binder">
        ///     A function that takes the value of type <typeparamref name="T" /> from a <see cref="IServiceResult{T}" />
        ///     and transforms it into an option containing a value of type <typeparamref name="TOut" />.
        /// </param>
        /// <returns></returns>
        public static IServiceResult<TOut> Bind<T, TOut>(this IServiceResult<T> result,
            Func<T, IServiceResult<TOut>> binder)
        {
            switch (result)
            {
                case ISuccessServiceResult<T> success:
                    return Wrap(() => binder(success.Value));
                case IFailureServiceResult failure:
                    return Return<TOut>(failure);
                default:
                    return Return<TOut>();
            }
        }

        /// <summary>
        ///     Returns 1 if the <paramref name="result" /> is <see cref="ISuccessServiceResult" />; otherwise returns 0.
        /// </summary>
        /// <param name="result">The input result.</param>
        /// <returns>One if the <paramref name="result" /> is <see cref="ISuccessServiceResult" />; otherwise zero.</returns>
        public static int Count(this IServiceResult result) => result.IsSuccess() ? 1 : 0;

        /// <summary>
        ///     Returns false if the <paramref name="result" /> is <see cref="IEmptyServiceResult" />; otherwise returns the result of
        ///     applying the <paramref name="predicate" /> to the result value.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="result">The result.</param>
        /// <param name="predicate">A predicate that accepts a value of type <typeparamref name="T" />.</param>
        /// <returns></returns>
        public static bool Exists<T>(this IServiceResult<T> result, Predicate<T> predicate)
            => result.Filter(predicate).IsSuccess();

        /// <summary>
        ///     If the <paramref name="result" /> is <see cref="ISuccessServiceResult" /> and the <paramref name="filter" /> predicate
        ///     returns true when passed the value in the <paramref name="result" />, this returns <see cref="ISuccessServiceResult{T}" />
        ///     .
        ///     Otherwise it returns a <see cref="IEmptyServiceResult" />.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="result">The result.</param>
        /// <param name="filter">
        ///     A function that evaluates whether the value contained in the <paramref name="result" /> should
        ///     remain, or be filtered out.
        /// </param>
        /// <returns></returns>
        public static IServiceResult<T> Filter<T>(this IServiceResult<T> result, Predicate<T> filter)
        {
            if (!(result is ISuccessServiceResult<T> success)) return result;

            switch (Wrap(() => filter(success.Value)))
            {
                case ISuccessServiceResult<bool> filterSuccess:
                    return filterSuccess.Value ? success : Return<T>();
                case IFailureServiceResult filterFailure:
                    return Return<T>(filterFailure);
                default:
                    return Return<T>();
            }
        }

        /// <summary>
        ///     Returns the <paramref name="initialState" /> if the <paramref name="result" /> is <see cref="IEmptyServiceResult" />;
        ///     otherwise returns the
        ///     updated state with the <paramref name="folder" /> and <paramref name="result" /> value.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <typeparam name="TOut">The type of the out.</typeparam>
        /// <param name="result">The result.</param>
        /// <param name="folder">A function to update the state data when given a value from a <see cref="IServiceResult{T}" />.</param>
        /// <param name="initialState">The initial state.</param>
        /// <returns>
        ///     The initial state if the <paramref name="result" /> is <see cref="IEmptyServiceResult" />; otherwise returns the
        ///     updated state with the <paramref name="folder" /> and <paramref name="result" /> value.
        /// </returns>
        /// <exception cref="ServiceResultException"><paramref name="folder" /> threw an exception.</exception>
        public static TOut Fold<T, TOut>(this IServiceResult<T> result, Func<TOut, T, TOut> folder, TOut initialState)
        {
            if (!(result is ISuccessServiceResult<T> success)) return initialState;

            try
            {
                return folder(initialState, success.Value);
            }
            catch (Exception e)
            {
                throw new ServiceResultException("Folder function threw an exception.", e);
            }
        }

        /// <summary>
        ///     Determines whether this instance is an <see cref="IFailureServiceResult" />.
        /// </summary>
        /// <param name="result">The result.</param>
        /// <returns>
        ///     <c>true</c> if the specified result is an <see cref="IFailureServiceResult" />; otherwise, <c>false</c>.
        /// </returns>
        public static bool IsFailure(this IServiceResult result) => result is IFailureServiceResult;

        /// <summary>
        ///     Determines whether this instance is an <see cref="IEmptyServiceResult" />.
        /// </summary>
        /// <param name="result">The result.</param>
        /// <returns>
        ///     <c>true</c> if the specified result is an <see cref="IEmptyServiceResult" />.; otherwise, <c>false</c>.
        /// </returns>
        public static bool IsEmpty(this IServiceResult result) => result is IEmptyServiceResult;

        /// <summary>
        ///     Determines whether this instance is an <see cref="ISuccessServiceResult" />.
        /// </summary>
        /// <param name="result">The result.</param>
        /// <returns>
        ///     <c>true</c> if the specified result is <see cref="ISuccessServiceResult" />; otherwise, <c>false</c>.
        /// </returns>
        public static bool IsSuccess(this IServiceResult result) => result is ISuccessServiceResult;

        /// <summary>
        /// Casts the <paramref name="result"/> to <see cref="ISuccessServiceResult{TEntity}"/>
        /// </summary>
        /// <typeparam name="TEntity"></typeparam>
        /// <param name="result"></param>
        /// <returns></returns>
        public static ISuccessServiceResult<TEntity> AsSuccess<TEntity>(this IServiceResult<TEntity> result) =>
            result as ISuccessServiceResult<TEntity>;

        /// <summary>
        /// Casts the <paramref name="result"/> to <see cref="IEmptyServiceResult{TEntity}"/>
        /// </summary>
        /// <typeparam name="TEntity"></typeparam>
        /// <param name="result"></param>
        /// <returns></returns>
        public static IEmptyServiceResult<TEntity> AsEmpty<TEntity>(this IServiceResult<TEntity> result) =>
            result as IEmptyServiceResult<TEntity>;

        /// <summary>
        /// Casts the <paramref name="result"/> to <see cref="IFailureServiceResult{TEntity}"/>
        /// </summary>
        /// <typeparam name="TEntity"></typeparam>
        /// <param name="result"></param>
        /// <returns></returns>
        public static IFailureServiceResult<TEntity> AsFailure<TEntity>(this IServiceResult<TEntity> result) =>
            result as IFailureServiceResult<TEntity>;


        /// <summary>
        /// Casts the <paramref name="result"/> to <see cref="ISuccessServiceResult{TEntity}"/>
        /// </summary>
        /// <typeparam name="TEntity"></typeparam>
        /// <param name="result"></param>
        /// <returns></returns>
        public static ISuccessServiceResult<TEntity> AsSuccess<TEntity>(this IServiceResult result) =>
            result as ISuccessServiceResult<TEntity>;

        /// <summary>
        /// Casts the <paramref name="result"/> to <see cref="IEmptyServiceResult{TEntity}"/>
        /// </summary>
        /// <typeparam name="TEntity"></typeparam>
        /// <param name="result"></param>
        /// <returns></returns>
        public static IEmptyServiceResult<TEntity> AsEmpty<TEntity>(this IServiceResult result) =>
            result as IEmptyServiceResult<TEntity>;

        /// <summary>
        /// Casts the <paramref name="result"/> to <see cref="IFailureServiceResult{TEntity}"/>
        /// </summary>
        /// <typeparam name="TEntity"></typeparam>
        /// <param name="result"></param>
        /// <returns></returns>
        public static IFailureServiceResult<TEntity> AsFailure<TEntity>(this IServiceResult result) =>
            result as IFailureServiceResult<TEntity>;

        /// <summary>
        ///     If the <paramref name="result" /> is <see cref="ISuccessServiceResult{T}" />, then the <paramref name="action" /> is
        ///     applied, otherwise no-op.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="result">The result.</param>
        /// <param name="action">The action to apply to the <paramref name="result" /> value.</param>
        /// <exception cref="ServiceResultException"><paramref name="action" /> threw an exception.</exception>
        public static void Iter<T>(this IServiceResult<T> result, Action<T> action)
        {
            if (!(result is ISuccessServiceResult<T> success)) return;

            try
            {
                action(success.Value);
            }
            catch (Exception e)
            {
                throw new ServiceResultException("Iter action threw an exception.", e);
            }
        }

        /// <summary>
        ///     If <paramref name="result" /> is <see cref="ISuccessServiceResult" />, applies the given <paramref name="mapping" />
        ///     function to the value and returns a new <see cref="IServiceResult{TOut}" />
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <typeparam name="TOut">The type of the out.</typeparam>
        /// <param name="result">The result.</param>
        /// <param name="mapping">The mapping.</param>
        /// <returns></returns>
        public static IServiceResult<TOut> Map<T, TOut>(this IServiceResult<T> result,
            Func<T, TOut> mapping)
        {
            switch (result)
            {
                case ISuccessServiceResult<T> success:
                    return Wrap(() => mapping(success.Value));
                case IFailureServiceResult failure:
                    return Return<TOut>(failure);
                default:
                    return Return<TOut>();
            }
        }

        #endregion
    }
}
