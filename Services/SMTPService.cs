using JellyFlix_MediaHub.Utils;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MailKit.Net.Smtp;
using MailKit.Security;
using MimeKit;

namespace JellyFlix_MediaHub.Services
{
    internal class SMTPService
    {
        private readonly string smtp_host;
        private readonly int smtp_port;
        private readonly string smtp_username;
        private readonly string smtp_password;
        private readonly bool is_configured;

        public SMTPService()
        {
            var config = ConfigManager.LoadConfig();
            smtp_host = config.SmtpHost;
            smtp_port = config.SmtpPort ?? 587;
            smtp_username = config.SmtpUsername;
            smtp_password = config.SmtpPassword;
            is_configured = config.SmtpConfigured;
        }

        public bool IsConfigured => is_configured;

        public async Task<bool> SendInvitationEmail(string recipientEmail, string inviteCode)
        {
            if (!is_configured)
                return false;

            try
            {
                var message = new MimeMessage();
                message.From.Add(new MailboxAddress("JellyFlix", smtp_username));
                message.To.Add(new MailboxAddress("", recipientEmail));
                message.Subject = "Welcome to JellyFlix ~ Your Invitation";

                var bodyBuilder = new BodyBuilder
                {
                    HtmlBody = GenerateInvitationEmailBody(inviteCode)
                };
                message.Body = bodyBuilder.ToMessageBody();

                using (var client = new SmtpClient())
                {
                    // For detailed logging
                    client.MessageSent += (sender, args) => {
                        Console.WriteLine($"Message sent successfully to {args.Message.To}");
                    };

                    Console.WriteLine($"Attempting to connect to {smtp_host}:{smtp_port}");

                    // Choose appropriate security options based on port
                    SecureSocketOptions secureOptions;
                    if (smtp_port == 465)
                        secureOptions = SecureSocketOptions.SslOnConnect;
                    else if (smtp_port == 587)
                        secureOptions = SecureSocketOptions.StartTls;
                    else
                        secureOptions = SecureSocketOptions.Auto;

                    // Connect with a timeout
                    await client.ConnectAsync(smtp_host, smtp_port, secureOptions);
                    Console.WriteLine("Connected successfully");

                    // Authenticate
                    await client.AuthenticateAsync(smtp_username, smtp_password);
                    Console.WriteLine("Authenticated successfully");

                    // Send the message
                    await client.SendAsync(message);
                    Console.WriteLine("Email sent successfully");

                    // Properly disconnect
                    await client.DisconnectAsync(true);
                    return true;
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error sending email: {ex.Message}");
                if (ex.InnerException != null)
                    Console.WriteLine($"Inner Exception: {ex.InnerException.Message}");
                return false;
            }
        }

        private string GenerateInvitationEmailBody(string inviteCode)
        {
            return $@"
                <html>
                <body style='font-family: Arial, sans-serif; color: #333;'>
                    <div style='max-width: 600px; margin: 0 auto; padding: 20px; border: 1px solid #ddd; border-radius: 5px;'>
                        <h2 style='color: #e63946;'>Welcome to JellyFlix!</h2>
                        <p>You've been invited to join JellyFlix, your new home for entertainment.</p>
                        <p>Use the following invitation code to create your account:</p>
                        <div style='background-color: #f8f9fa; padding: 15px; text-align: center; font-size: 24px; letter-spacing: 5px; font-weight: bold;'>
                            {inviteCode}
                        </div>
                        <p style='margin-top: 20px;'>If you didn't request this invitation, please ignore this email.</p>
                        <p>Enjoy your streaming experience!</p>
                        <p>- The JellyFlix Team</p>
                    </div>
                </body>
                </html>";
        }

        public static async Task<bool> ValidateSmtpSettings(string host, int port, string username, string password)
        {
            try
            {
                using (var client = new SmtpClient())
                {
                    // Configure timeout
                    client.Timeout = 5000; // 5 seconds

                    // Choose security options based on port
                    SecureSocketOptions secureOptions;
                    if (port == 465)
                        secureOptions = SecureSocketOptions.SslOnConnect;
                    else if (port == 587)
                        secureOptions = SecureSocketOptions.StartTls;
                    else
                        secureOptions = SecureSocketOptions.Auto;

                    // Connect with a cancellation token for timeout handling
                    var cts = new System.Threading.CancellationTokenSource(10000); // 10 second timeout
                    await client.ConnectAsync(host, port, secureOptions, cts.Token);

                    // Authenticate
                    await client.AuthenticateAsync(username, password, cts.Token);

                    // Properly disconnect
                    await client.DisconnectAsync(true, cts.Token);
                    return true;
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"SMTP validation failed: {ex.Message}");
                if (ex.InnerException != null)
                    Console.WriteLine($"Inner Exception: {ex.InnerException.Message}");
                return false;
            }
        }

        public static string GenerateInviteCode()
        {
            Random random = new Random();
            const string chars = "ABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789";

            char[] code = new char[10];
            for (int i = 0; i < 10; i++)
            {
                code[i] = chars[random.Next(chars.Length)];

                if (i == 4)
                    code[i] = '-';
            }

            return new string(code);
        }
    }
}
