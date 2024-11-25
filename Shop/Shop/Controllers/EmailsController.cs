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
		public IActionResult SendEmail(EmailsIndexViewModel vm)
		{
			var dto = new EmailDto()
			{
				To = vm.To,
				Subject = vm.Subject,
				Body = vm.Body,
			};
			_emailServices.SendEmail(dto);
			return RedirectToAction(nameof(Index));
		}
	}
}