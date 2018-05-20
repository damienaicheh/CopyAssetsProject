using System;
namespace CopyAssetsProject.Errors
{
    public class ErrorHandler : IErrorHandler
    {
        public void HandleError(Exception ex)
        {
            // Manage exception...
            System.Diagnostics.Debug.WriteLine(ex.ToString());
        }
    }
}
