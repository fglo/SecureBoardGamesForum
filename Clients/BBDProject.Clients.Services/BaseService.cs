using System;
using AutoMapper;
using BBDProject.Clients.Models;
using BBDProject.Clients.Models.Exceptions;
using Microsoft.AspNetCore.Http;

namespace BBDProject.Clients.Services
{
    public abstract class BaseService : IBaseService
    {
        public UserContext UserContext { get; set; }
        public AppSettings AppSettings { get; set; }
        public IMapper Mapper { get; set; }

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
            throw new ServiceException(message, details, statusCode);
        }
        protected T Error<T>(Exception e, string message, string details = null, int statusCode = StatusCodes.Status400BadRequest)
        {
            if (e is RepositoryException)
            {
                var re = e as RepositoryException;
                throw re;
            }
            if (e is ServiceException)
            {
                var se = e as ServiceException;
                throw se;
            }
            throw new ServiceException(message, string.IsNullOrEmpty(details) ? e.Message: details, statusCode);
        }
    }
}
