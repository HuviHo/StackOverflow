using System;
using System.Collections.Generic;

namespace StackOverflow.Data
{
	public class User
	{
		public int Id { get; set; }
		public string FirstName { get; set; }
		public string LastName { get; set; }
		public string Email { get; set; }
		public string Password { get; set; }
		public List<Like>Likes { get; set; }
		public List<Question> Questions { get; set; }
		public List<Answer> Answers { get; set; }
	}

	public class Question
	{
		public int Id { get; set; }
		public string Title { get; set; }
		public string Text { get; set; }
		public DateTime DatePosted { get; set; }
		public List<QuestionTag> QuestionsTags { get; set; }
		public List <Like> Likes { get; set; }
		public int UserId { get; set; }
		public User User { get; set; }
		public List<Answer> Answers { get; set; }

	}

	public class Answer
	{
		public int Id { get; set; }
		public string Text { get; set; }
		public int QuestionId { get; set; }
		public Question Question { get; set; }
		public int UserId { get; set; }
		public User User { get; set; }
	}

	public class Like
	{
		public int UserId { get; set; }
		public int QuestionId { get; set; }
		public User User { get; set; }
		public Question Question { get; set; }
	}

	public class Tag
	{
		public int Id { get; set; }
		public string Name { get; set; }
		public List<QuestionTag> QuestionTags { get; set; }
	}

	public class QuestionTag
	{
		public int QuestionId { get; set; }
		public int TagId { get; set; }
		public Question Question { get; set; }
		public Tag Tag { get; set; }
	}
}
