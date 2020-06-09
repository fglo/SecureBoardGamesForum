using System.Collections.Generic;
using System.Net.Mail;

namespace BBDProject.Shared.Models.Email
{
    public class EmailDefinition
    {
        public List<EmailAddress> To { get; set; }

        public string Subject { get; set; }

        public string Body { get; set; }

        public string HtmlBody { get; set; }

        public AttachmentCollection Attachments { get; set; }
    }
}
