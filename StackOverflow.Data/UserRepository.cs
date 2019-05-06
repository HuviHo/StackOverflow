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


		public int GetIdForEmail(string email)
		{
			int id = 0;
			using (var context = new StackOverflowContext(_connectionString))
			{
				id = context.Users.FirstOrDefault(u => u.Email == email).Id;
			}
			return id;
		}

		public void PostQuestion(Question question)
		{
			using (var context = new StackOverflowContext(_connectionString))
			{
				context.Questions.Add(question);
				context.SaveChanges();
			}
		}

		public void PostAnswer(Answer answer)
		{
			using (var context = new StackOverflowContext(_connectionString))
			{
				context.Answers.Add(answer);
				context.SaveChanges();
			}
		}
		
		public void PostLike(Like like)
		{
			using (var context = new StackOverflowContext(_connectionString))
			{
				context.Likes.Add(like);
				context.SaveChanges();
			}
		}
	}
}
