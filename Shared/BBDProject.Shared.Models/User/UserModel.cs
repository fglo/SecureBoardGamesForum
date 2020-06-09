using Microsoft.AspNetCore.Identity;

namespace BBDProject.Shared.Models.User
{
    public class UserModel
    {
        public int Id { get; set; }
        public string UserName { get; set; }
        public string Email { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public bool LockedOut { get; set; }
    }
}

