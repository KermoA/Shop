using Shop.Core.Dto;

namespace Shop.Core.ServiceInterface
{
    public interface IEmailServices
    {
		Task SendEmail(EmailDto dto);

	}
}
