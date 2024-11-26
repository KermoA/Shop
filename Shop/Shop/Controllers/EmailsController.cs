using Microsoft.AspNetCore.Mvc;
using Shop.Core.Dto;
using Shop.Core.ServiceInterface;
using Shop.Models.Emails;

namespace Shop.Controllers
{
	public class EmailsController : Controller
	{
		private readonly IEmailServices _emailServices;

		public EmailsController(IEmailServices emailServices)
		{
			_emailServices = emailServices;
		}

		public IActionResult Index()
		{
			ViewData["Title"] = "Contact Us";

			return View();
		}

		[HttpPost]
		public async Task<IActionResult> SendEmail(EmailsIndexViewModel vm)
		{
			try
			{
				var dto = new EmailDto()
				{
					To = vm.To,
					Subject = vm.Subject,
					Body = vm.Body,
					Attachments = vm.Attachments
				};

				_emailServices.SendEmail(dto);
			}
			catch (Exception ex)
			{
				Console.WriteLine($"Email sending failed: {ex.Message}");
			}

			return RedirectToAction(nameof(Index));
		}

	}
}
