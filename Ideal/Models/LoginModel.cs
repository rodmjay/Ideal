using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Ideal.Models
{
    public class LoginModel
    {
        public string UserName { get; set; }
        public string Password { get; set; }
        public bool RememberMe { get; set; }
        public string ReturnUrl { get; set; }
    }
}