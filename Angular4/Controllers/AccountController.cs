using System;
using Microsoft.AspNetCore.Mvc;
using VisualStudio2017.Angular4.Models;
using VisualStudio2017.Backend.Domain;

namespace VisualStudio2017.Angular4.Controllers
{
    [Route("api/account")]
	public class AccountController : Controller
    {
        [HttpPost("login")]
		public User Login([FromBody] LoginModel model)
		{
			return new User { Email = model.email };
		}

        [HttpPost("register")]
		public User Register([FromBody] RegisterModel model)
		{
			if (string.IsNullOrEmpty(model.password) || string.IsNullOrEmpty(model.confirmPassword) || model.password != model.confirmPassword)
				throw new Exception("Passwords are null or do not match.");
            
            return new User { Email = model.email };
		}

		[HttpPost("logout")]
		public string Logout()
		{
			return "it worked";
		}
    }
}