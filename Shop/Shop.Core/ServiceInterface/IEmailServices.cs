using Shop.Core.Dto;

namespace Shop.Core.ServiceInterface
{
    public interface IEmailServices
    {
		public void SendEmail(EmailDto dto);

	}
}
