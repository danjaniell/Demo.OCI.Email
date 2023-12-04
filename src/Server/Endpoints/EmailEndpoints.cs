using System.Net;
using System.Net.Mail;
using System.Security;

namespace Server.Endpoints
{
    public static class EmailEndpoints
    {
        public static void ConfigureEmailEndpoints(
            this WebApplication app,
            IConfiguration configuration
        )
        {
            app.MapPost(
                    "/send-email",
                    async () =>
                    {
                        var emailSettings = configuration.GetSection("EmailSettings");

                        var fromEmail = emailSettings["FromEmail"];
                        var toEmail = emailSettings["ToEmail"];
                        var subject = "Test Email";
                        var body = "This is a test email.";

                        var host = emailSettings["Host"];
                        var port = int.Parse(emailSettings["Port"]!);
                        var userName = emailSettings["UserName"];
                        var password = new SecureString();

                        foreach (char c in emailSettings["Password"]!)
                        {
                            password.AppendChar(c);
                        }

                        var message = new MailMessage(fromEmail!, toEmail!, subject, body);

                        var smtpClient = new SmtpClient(host, port)
                        {
                            EnableSsl = true,
                            Credentials = new NetworkCredential(userName, password)
                        };
                        password.Clear();

                        try
                        {
                            await smtpClient.SendMailAsync(message);
                        }
                        catch (SmtpException ex)
                        {
                            Console.WriteLine($"Email was unsuccessful: {ex.Message}");
                        }
                    }
                )
                .WithName("SendEmail")
                .WithOpenApi();
        }
    }
}
