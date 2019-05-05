using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.EntityFrameworkCore;
using System.Linq;

namespace StackOverflow.Data
{
	public class StackOverflowContext : DbContext
	{
		private string _connectionString;
		public StackOverflowContext(string connectionString)
		{
			_connectionString = connectionString;
		}

		public DbSet<User> Users { get; set; }
		public DbSet<Question> Questions { get; set; }
		public DbSet<Answer> Answers { get; set; }
		public DbSet<Like> Likes { get; set; }
		public DbSet<Tag> Tags { get; set; }
		public DbSet<QuestionTag> QuestionsTags { get; set; }
		
		protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
		{
			optionsBuilder.UseSqlServer(_connectionString);
		}

		protected override void OnModelCreating(ModelBuilder modelBuilder)
		{
			foreach(var relationship in modelBuilder.Model.GetEntityTypes().SelectMany (e => e.GetForeignKeys()))
			{
				relationship.DeleteBehavior = DeleteBehavior.Restrict;
			}

			modelBuilder.Entity<QuestionTag>()
				.HasKey(qt => new { qt.QuestionId, qt.TagId });

			modelBuilder.Entity<QuestionTag>()
				.HasOne(qt => qt.Question)
				.WithMany(q => q.QuestionsTags)
				.HasForeignKey(q => q.QuestionId);

			modelBuilder.Entity<QuestionTag>()
				.HasOne(qt => qt.Tag)
				.WithMany(t => t.QuestionTags)
				.HasForeignKey(q => q.TagId);

			modelBuilder.Entity<Like>()
				.HasKey(qu => new { qu.QuestionId, qu.UserId });

			modelBuilder.Entity<Like>()
				.HasOne(qu => qu.Question)
				.WithMany(q => q.Likes)
				.HasForeignKey(q => q.QuestionId);

			modelBuilder.Entity<Like>()
				.HasOne(qu => qu.User)
				.WithMany(u => u.Likes)
				.HasForeignKey(u => u.UserId);
		}
	}
}
