using System;
using BBDProject.Management.Db;
using BBDProject.Management.Models.Exceptions;
using Microsoft.AspNetCore.Http;

namespace BBDProject.Management.Repositories
{
    public abstract class BaseRepository : ISaoaaBaseRepository
    {
        public ManagementDbContext DbContext { get; set; }

        protected void Error(string message, string details = null, int statusCode = StatusCodes.Status400BadRequest)
        {
            Error<int>(message, details, statusCode);
        }
        protected void Error(Exception e, string message, string details = null, int statusCode = StatusCodes.Status400BadRequest)
        {
            Error<int>(e, message, details, statusCode);
        }

        protected T Error<T>(string message, string details = null, int statusCode = StatusCodes.Status400BadRequest)
        {
            throw new RepositoryException(message, details, statusCode);
        }
        protected T Error<T>(Exception e, string message, string details = null, int statusCode = StatusCodes.Status400BadRequest)
        {
            if (e is RepositoryException)
            {
                var re = e as RepositoryException;
                throw new RepositoryException(re.Message, statusCode: re.StatusCode);
            }
            throw new RepositoryException(message, string.IsNullOrEmpty(details) ? e.Message : details, statusCode);
        }
    }
}
