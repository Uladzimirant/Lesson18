using Microsoft.AspNetCore.Mvc;
using System.Runtime.Serialization;

namespace Lesson18.Exceptions
{
    public class IncorrectOperationException : Exception
    {
        public string MessageTitle = "";
        public object? Controller; 

        public IncorrectOperationException(object? controller)
        {
            Controller = controller;
        }
        public IncorrectOperationException(object? controller, string? message, string messageTitle = "") : base(message)
        {
            Controller = controller;
            MessageTitle = messageTitle;
        }
    }
}
