using System;
using Microsoft.AspNetCore.Http;

namespace BBDProject.Management.Models.Exceptions
{
    /// <summary>
    /// Exception thrown from repositories while accessing database
    /// </summary>
    public class ServiceException : Exception
    {
        /// <summary>
        /// HTML Status Code associated with the exception
        /// </summary>
        public int StatusCode { get; }

        public ServiceException(string message, int statusCode = StatusCodes.Status400BadRequest) : base(message)
        {
            StatusCode = statusCode;
        }

        public ServiceException(string message, string details, int statusCode = StatusCodes.Status400BadRequest) : base(message)
        {
            Data.Add("Details", details);
        }
    }
}
