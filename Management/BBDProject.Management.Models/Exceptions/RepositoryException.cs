using System;
using Microsoft.AspNetCore.Http;

namespace BBDProject.Management.Models.Exceptions
{
    /// <summary>
    /// Exception thrown from repositories while accessing database
    /// </summary>
    public class RepositoryException : Exception
    {
        private readonly string _message;
        /// <summary>
        /// Exception Message
        /// </summary>
        public override string Message => _message;
        /// <summary>
        /// HTML Status Code associated with the exception
        /// </summary>
        public int StatusCode { get; }

        public RepositoryException(string message, string details = null, int statusCode = StatusCodes.Status500InternalServerError)
        {
            _message = message;
            Data.Add("Details", details);
            StatusCode = statusCode;
        }
    }
}
