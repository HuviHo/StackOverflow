using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using StackOverflow.Data;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;
using Microsoft.AspNetCore.Authorization;
using System.Security.Claims;
using Microsoft.AspNetCore.Authentication;

namespace StackOverflow.Web.Controllers
{
    public class UserController : Controller		
    {
		private IHostingEnvironment _environment;
		private string _connectionString;

		public UserController (IHostingEnvironment environment, IConfiguration configuration )
		{
			_environment = environment;
			_connectionString = configuration.GetConnectionString("ConStr");
		}
		public IActionResult SignUp()
        {
            return View();
        }

		[HttpPost]
		public IActionResult SignUp (User user)
		{
			UserRepository repository = new UserRepository(_connectionString);
			repository.AddUser(user);
			return Redirect("/user/logIn");
		}

		public IActionResult LogIn()
		{
			return View();
		}

		[HttpPost]
		public IActionResult LogIn (string email, string password)
		{
			UserRepository repository = new UserRepository(_connectionString);
			string passwordFromDB = repository.GetPasswordForEmail(email);
			if (passwordFromDB==null)
			{
				return Redirect("/user/logIn");
			}

			var claims = new List<Claim>
			{
				new Claim ("user",email)
			};
			HttpContext.SignInAsync(new ClaimsPrincipal(
				new ClaimsIdentity(claims, "Cookies", "user", "role"))).Wait();
			return Redirect("/home/index");
		}

		public IActionResult AskQuestion()
		{
			if (!User.Identity.IsAuthenticated)
			{
				return Redirect("/user/logIn");
			}
			return View();
		}

		[HttpPost]
		public IActionResult PostQuestion()
		{
			
			return Redirect("/home/index");
		}

		public IActionResult LogOut()
		{
			HttpContext.SignOutAsync().Wait();
			return Redirect("/home/index");
		}
	}
}