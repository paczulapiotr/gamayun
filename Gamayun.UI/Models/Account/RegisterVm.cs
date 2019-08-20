using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Gamayun.UI.Models.Account
{
    public class RegisterVm
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Login { get; set; }
        public string Email { get; set; }
        public string RepeatEmail { get; set; }
        public string Password { get; set; }
        public string RepeatPassword { get; set; }
        public bool AgreeToTerms { get; set; }
    }
}
