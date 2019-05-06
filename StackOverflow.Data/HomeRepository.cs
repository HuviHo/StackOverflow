using System;
using System.Collections.Generic;
using System.Text;
using System.Data.SqlClient;
using System.Linq;
using Microsoft.EntityFrameworkCore;

namespace StackOverflow.Data
{
	public class HomeRepository
	{
		private readonly string _connectionString;

		public HomeRepository(string connectionString)
		{
			_connectionString = connectionString;
		}

		public List<Question> GetQuestions()
		{
			using (var context = new StackOverflowContext(_connectionString))
			{
				return context.Questions.OrderByDescending(q => q.DatePosted).ToList();
			}
		}

		public Question GetQuestionForId(int questionId)
		{
			using (var context = new StackOverflowContext(_connectionString))
			{
				return context.Questions.FirstOrDefault(q => q.Id == questionId);
			}
		}


		public List<Tag> GetTagsForId(int questionId)
		{
			using (var context = new StackOverflowContext(_connectionString))
			{
			List<Tag> tags = context.Tags.FromSql(@"SELECT T.Id, T.Name FROM Tags T
						JOIN QuestionsTags QT
						ON T.Id=QT.TagId
						WHERE QuestionId=@questionId",
						new SqlParameter("@questionid", questionId)).ToList();
				return tags;
			}
		}	

		public int GetNumberOfLikes(int questionId)
		{
			using (var context = new StackOverflowContext(_connectionString))
			{
				int numberOfLikes= context.Likes.Where(l => l.QuestionId == questionId).ToList().Count();
				return numberOfLikes;
			}
		}

		public bool GetUserLiked(int questionId, int userId)
		{
			using (var context = new StackOverflowContext(_connectionString))
			{
				bool userLiked = context.Likes.Any(l => l.QuestionId == questionId && l.UserId==userId);
				return userLiked;
			}
		}

		public List<Answer> GetAnswersForId(int questionId)
		{
			using (var context = new StackOverflowContext(_connectionString))
			{
				List<Answer> answers = context.Answers.Where(a => a.QuestionId == questionId).ToList();
				return answers;
			}
		}


	}
}
