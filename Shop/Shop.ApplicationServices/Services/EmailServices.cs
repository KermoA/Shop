using Microsoft.Extensions.Configuration;
using MimeKit;
using Shop.Core.Dto;
using Shop.Core.ServiceInterface;
using MailKit.Net.Smtp;

namespace Shop.ApplicationServices.Services
{
	public class EmailServices : IEmailServices
	{
		private readonly IConfiguration _config;
		public EmailServices(IConfiguration config)
		{
			_config = config;
		}

		public void SendEmail(EmailDto dto)
		{
			var email = new MimeMessage();
			email.From.Add(MailboxAddress.Parse(_config.GetSection("EmailUsername").Value));
			email.To.Add(MailboxAddress.Parse(dto.To));
			email.Subject = dto.Subject;

			var multipart = new Multipart("mixed");
			multipart.Add(new TextPart("html") { Text = dto.Body });

			if (dto.Attachments != null && dto.Attachments.Count > 0)
			{
				foreach (var file in dto.Attachments)
				{
					var mimePart = new MimePart
					{
						Content = new MimeContent(file.OpenReadStream()),
						ContentDisposition = new ContentDisposition(ContentDisposition.Attachment),
						ContentTransferEncoding = ContentEncoding.Base64,
						FileName = file.FileName
					};
					multipart.Add(mimePart);
				}
			}

			email.Body = multipart;

			using var smtp = new SmtpClient();
			smtp.Connect(_config.GetSection("EmailHost").Value, 587, MailKit.Security.SecureSocketOptions.StartTls);
			smtp.Authenticate(_config.GetSection("EmailUsername").Value, _config.GetSection("EmailPassword").Value);
			smtp.Send(email);
			smtp.Disconnect(true);
		}
	}
}
