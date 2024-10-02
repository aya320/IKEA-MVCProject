using IKEA.DAL.Entities.Email;

namespace IKEA.PL.Helpers
{
	public interface IMailSettings
	{
		public void SendEmail(Email email);

	}
}
