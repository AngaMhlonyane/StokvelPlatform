namespace StokvelPlatform.Services;

public class MailSender : IEmailSender
{
    private readonly IConfiguration _config;

    public MailSender(IConfiguration config)
    {
        _config = config;
    }

    public async Task SendEmailAsync(string email, string subject, string htmlMessage)
    {
        try
        {
            var mail = new MimeMessage();

            mail.From.Add(new MailboxAddress(_config["smtp:Sender"], _config["smtp:Username"]));
            mail.To.Add(new MailboxAddress("", email));
            mail.Subject = subject;
            mail.Body = new TextPart(TextFormat.Html) { Text = htmlMessage };

            using var client = new SmtpClient();

            await client.ConnectAsync(_config["smtp:Host"], Convert.ToInt32(_config["smtp:Port"]), SecureSocketOptions.StartTls);
            await client.AuthenticateAsync(_config["smtp:Username"], _config["smtp:Password"]);
            await client.SendAsync(mail);
            await client.DisconnectAsync(true);
        }
        catch (Exception ex)
        {
            //save to txt file or database.
            Console.WriteLine($"There was an error sending email to {email} with subject '{subject}'.");
            Console.WriteLine($"Error : {ex.Message.ToString()}.");
        }
    }
}
