using System;
using System.Threading.Tasks;
using CopyAssetsProject.Errors;

namespace CopyAssetsProject.Utils
{
    public static class TaskUtilities
    {
#pragma warning disable RECS0165 // Asynchronous methods should return a Task instead of void
        public static async void FireAndForgetSafeAsync(this Task task, IErrorHandler handler = null)
#pragma warning restore RECS0165 // Asynchronous methods should return a Task instead of void
        {
            try
            {
                await task;
            }
            catch (Exception ex)
            {
                // Manage the error depending on your needs...
                handler?.HandleError(ex);
            }
        }
    }
}
