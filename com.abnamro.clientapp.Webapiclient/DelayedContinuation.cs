using System;
using System.Threading;
using System.Threading.Tasks;

namespace com.abnamro.clientapp.webapiclient
{
    public static class DelayedContinuation
    {
        /// <summary>
        /// Creates a cancellable action continuation that executes asynchronously after a specified time interval.
        /// </summary>
        /// <param name="delay">The time span to wait before continuing the action.</param>
        /// <param name="continuationAction">The action to run after the specified time interval.</param>
        /// <param name="cancellationToken">The cancellation token that will be checked prior to continuing the action.</param>
        /// <returns></returns>
        public static async Task ContinueWith(TimeSpan delay, Action continuationAction, CancellationToken cancellationToken)
        {
            await Task.Delay(delay, cancellationToken).ContinueWith(task =>
            {
                if (task.IsCompleted && !task.IsCanceled) continuationAction?.Invoke();
            });
        }

        /// <summary>
        /// Creates a cancellable function continuation that executes asynchronously after a specified time interval.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="delay">The time span to wait before continuing the function.</param>
        /// <param name="continuationFunction">The function to run after the specified time interval.</param>
        /// <param name="cancellationToken">The cancellation token that will be checked prior to continuing the function.</param>
        /// <returns></returns>
        public static async Task<T> ContinueWith<T>(TimeSpan delay, Func<T> continuationFunction, CancellationToken cancellationToken)
        {
            return await Task.Delay(delay, cancellationToken).ContinueWith(task =>
            {
                return (task.IsCompleted && !task.IsCanceled && continuationFunction is Func<T>)? continuationFunction() : default(T);
            });
        }
    }
}
