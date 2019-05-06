using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using StackOverflow.Data;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;

namespace StackOverflow.Web.Controllers
{
	public class HomeController : Controller
	{

		private IHostingEnvironment _environment;
		private string _connectionString;

		public HomeController(IHostingEnvironment environment, IConfiguration configuration)
		{
			_environment = environment;
			_connectionString = configuration.GetConnectionString("ConStr");
		}

		public IActionResult Index()
		{
			HomeRepository repository = new HomeRepository(_connectionString);
			return View(repository.GetQuestions());
		}

		public IActionResult ViewQuestion(int questionId)
		{
			UserRepository userRepository = new UserRepository(_connectionString);
			HomeRepository repository = new HomeRepository(_connectionString);

			QuestionViewModel qvm = new QuestionViewModel();

			qvm.Question = repository.GetQuestionForId(questionId);
			qvm.Tags = repository.GetTagsForId(questionId);
			qvm.NumberOfLikes = repository.GetNumberOfLikes(questionId);
			qvm.UserLiked = true;
			if (User.Identity.IsAuthenticated)
			{
				int userId = userRepository.GetIdForEmail(User.Identity.Name);
				qvm.UserLiked = repository.GetUserLiked(questionId, userId);
			}
			qvm.Answers = repository.GetAnswersForId(questionId);

			return View(qvm);
		}
	}
}
