using MimeKit;
using System;
using Microsoft.Extensions.Logging;

namespace WebMoney.Services
{
    public class EmailService
    {
        private readonly ILogger<EmailService> logger;
        public EmailService(ILogger<EmailService> logger)
        {
            this.logger = logger;
        }

        public void SendEmailCustom(string email, string messageText)
        {
            try
            {
                MimeMessage message = new MimeMessage();
                message.From.Add(new MailboxAddress("Администратор", "witcher470@gmail.com")); //отправитель сообщения
                message.To.Add(new MailboxAddress(email)); //адресат сообщения
                message.Subject = "Информация из сервиса WebMoney"; //тема сообщения
                message.Body = new BodyBuilder() { HtmlBody = "<div style=\"color: green;\">" + messageText + "</div>" }.ToMessageBody(); //тело сообщения (так же в формате HTML)

                using (MailKit.Net.Smtp.SmtpClient client = new MailKit.Net.Smtp.SmtpClient())
                {
                    client.Connect("smtp.gmail.com", 465, true); //либо использум порт 465
                    client.Authenticate("witcher470@gmail.com", "kirmx470"); //логин-пароль от аккаунта
                    client.Send(message);

                    client.Disconnect(true);
                    logger.LogInformation("Сообщение отправлено успешно!");
                }
            }
            catch (Exception e)
            {
                logger.LogError(e.GetBaseException().Message);
            }
        }

    }
}