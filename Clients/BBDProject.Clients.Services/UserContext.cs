using BBDProject.Clients.Db.Dao;
using BBDProject.Clients.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;

namespace BBDProject.Clients.Services
{
    /// <summary>
    /// User Context containing ID of currently signed-in user
    /// </summary>
    public class UserContext
    {
        private readonly IHttpContextAccessor _contextAccessor;
        private readonly AppSettings _appSettings;

        public ISession Session => _contextAccessor.HttpContext.Session;

        public UserContext(UserManager<DaoUser> userManager,
            IHttpContextAccessor contextAccessor,
            AppSettings appSettings)
        {
            _contextAccessor = contextAccessor;
            _appSettings = appSettings;

            if (contextAccessor.HttpContext?.User != null)
            {
                var user = userManager.GetUserAsync(contextAccessor.HttpContext.User).Result;
                if (user != null)
                {
                    UserId = user.Id;
                    UserName = user.UserName;
                }
                else
                {
                    UserId = -1;
                    UserName = null;
                }
            }
        }

        /// <summary>
        /// Currently signed-in user ID
        /// </summary>
        public int UserId { get; set; }

        /// <summary>
        /// Currently signed-in user name
        /// </summary>
        public string UserName { get; set; }
    }
}
