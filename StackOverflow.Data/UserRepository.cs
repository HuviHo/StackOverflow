using System;
using System.Collections.Generic;
using System.Text;
using System.Data.SqlClient;
using System.Linq;
using Microsoft.EntityFrameworkCore;

namespace StackOverflow.Data
{
	public class UserRepository
	{
		private readonly string _connectionString;

		public UserRepository(string connectionString)
		{
			_connectionString = connectionString;
		}

		public void AddUser(User user)
		{
			user.Password = BCrypt.Net.BCrypt.HashPassword(user.Password);
			using (var context = new StackOverflowContext(_connectionString))
			{
				context.Users.Add(user);
				context.SaveChanges();
			}
		}

		public string GetPasswordForEmail (string email)
		{
			string password = "";
			using (var context = new StackOverflowContext(_connectionString))
			{
				password = context.Users.FirstOrDefault(u => u.Email==email).Password;
			}
			return password;
		}
	}
}
