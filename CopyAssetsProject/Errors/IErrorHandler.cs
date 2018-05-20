using System;
namespace CopyAssetsProject.Errors
{
    public interface IErrorHandler
    {
        void HandleError(Exception ex);
    }
}
