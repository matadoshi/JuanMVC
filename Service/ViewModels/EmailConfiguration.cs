using System;
using System.Collections.Generic;
using System.Text;

namespace Service.ViewModels
{
    public class EmailConfiguration
    {
        public string From { get; set; }
        public string SmtpServer { get; set; }
        public int Port { get; set; }
        public string Username { get; set; }
        public string Password { get; set; }
        public string Title { get; set; }
        public string Subject { get; set; }
    }
}
