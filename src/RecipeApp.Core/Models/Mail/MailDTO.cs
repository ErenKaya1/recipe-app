using System.Collections.Generic;

namespace RecipeApp.Core.Models.Mail
{
    public class MailDTO
    {
        public string Subject { get; set; }
        public string Content { get; set; }
        public List<string> To { get; set; }
        public string From { get; set; }
    }
}