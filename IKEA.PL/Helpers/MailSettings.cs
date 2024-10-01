

using IKEA.DAL.Entities.Email;
using IKEA.PL.Settings;
using MailKit.Net.Smtp;
using Microsoft.Extensions.Options;
using MimeKit;

namespace IKEA.PL.Helpers
{
	public class EMailSettings(IOptions<MailSettings> _options) : IMailSettings
	{
		public void SendEmail(Email email)
		{
			var mail = new MimeMessage

			{

				Sender = MailboxAddress.Parse(_options.Value.Email),

				Subject = email.Subject

			};

			mail.To.Add(MailboxAddress.Parse(email.To));

			mail.From.Add(new MailboxAddress(_options.Value.DisplayName, _options.Value.Email));

			var builder = new BodyBuilder();

			builder.TextBody = email.Body;

			mail.Body = builder.ToMessageBody();

			using var smtp = new SmtpClient();

			smtp.Connect(_options.Value.Host, _options.Value.Port, MailKit.Security.SecureSocketOptions.StartTls);

			smtp.Authenticate(_options.Value.Email, _options.Value.Password);

			smtp.Send(mail);

			smtp.Disconnect(true);
		}
	}
}
