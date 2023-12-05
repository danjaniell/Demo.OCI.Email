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
                        var senderName = emailSettings["SenderName"];
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

                        using (var message = new MailMessage())
                        {
                            message.Subject = subject;
                            message.From = new MailAddress(fromEmail!, senderName);
                            message.To.Add(toEmail!);
                            message.Body = body;

                            using (var client = new SmtpClient(host, port))
                            {
                                client.Credentials = new NetworkCredential(userName, password);
                                client.EnableSsl = true;
                                client.Send(message);
                                try
                                {
                                    Console.WriteLine("Email successfully sent!");
                                }
                                catch (SmtpException ex)
                                {
                                    Console.WriteLine($"Email was unsuccessful: {ex.Message}");
                                }
                            }
                        }
                        password.Clear();
                    }
                )
                .WithName("SendEmail")
                .WithOpenApi();
        }
    }
}
