using System.Collections.Generic;
using BBDProject.Shared.Models.Email;
using BBDProject.Shared.Models.User;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Infrastructure;
using Microsoft.AspNetCore.Mvc.Routing;
using Microsoft.Extensions.Configuration;
using MimeKit;

namespace BBDProject.Shared.Utils.Helpers
{
    public class EmailBuilder
    {
        public IConfigurationRoot Configuration { get; set; }
        public IActionContextAccessor ActionContextAccessor { get; set; }
        public IUrlHelperFactory UrlHelperFactory { get; set; }

        private string AppWww => Configuration["AppSettings:AppWww"];

        public EmailDefinition BuildResetPasswordEmail(string token, BaseUserInfo user)
        {
            var urlHelper = UrlHelperFactory.GetUrlHelper(ActionContextAccessor.ActionContext);
            string resetPasswordLink = AppWww + urlHelper.Action("SetNewPassword", "User",
                    new { username = user.UserName, token = token });

            var builder = new BodyBuilder();
            builder.TextBody = $"Witaj {user.FirstName} {user.LastName}! Kliknij w ten link aby zresetować hasło: {resetPasswordLink}";

            EmailDefinition emailDefinition = new EmailDefinition()
            {
                To = BuildEmailAddress(user),
                Subject = "Reset hasła",
                Body = builder.TextBody
            };

            return emailDefinition;
        }

        public EmailDefinition BuilConfirmAccountEmail(string token, BaseUserInfo user)
        {
            var urlHelper = UrlHelperFactory.GetUrlHelper(ActionContextAccessor.ActionContext);
            string confirmationLink = AppWww + urlHelper.Action("ConfirmEmail", "User", 
                    new { username = user.UserName, token = token });

            var builder = new BodyBuilder();
            builder.TextBody = $"Witaj {user.FirstName} {user.LastName}! Kliknij w ten link aby potwierdzić twój adres email: {confirmationLink}";

            EmailDefinition emailDefinition = new EmailDefinition()
            {
                To = BuildEmailAddress(user),
                Subject = "Potwierdzenie adresu e-mail",
                Body = builder.TextBody
            };

            return emailDefinition;
        }

        private List<EmailAddress> BuildEmailAddress(BaseUserInfo user)
        {
            var emailAddresses = new List<EmailAddress>();
            emailAddresses.Add(new EmailAddress()
            {
                Name = $"{user.FirstName} {user.LastName}",
                Address = user.Email
            });

            return emailAddresses;
        }
    }
}
