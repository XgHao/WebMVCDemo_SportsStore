using SportsStore.Domain.Abstract;
using SportsStore.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Text;
using System.Threading.Tasks;

namespace SportsStore.Domain.Concrete
{
    /// <summary>
    /// 邮件订单处理器
    /// </summary>
    public class EmailOrderProcessor : IOrderProcessor
    {
        private EmailSettings emailSettings;
        public EmailOrderProcessor(EmailSettings _emailSettings)
        {
            emailSettings = _emailSettings;
        }

        public void ProcessOrder(Cart cart, ShippingDetails shippingDetails)
        {
            using (var smtpClient = new SmtpClient()
            {
                EnableSsl = emailSettings.UseSsl,
                Host = emailSettings.ServerName,
                Port = emailSettings.ServerPort,
                UseDefaultCredentials = false,
                Credentials = new NetworkCredential(emailSettings.UserName, emailSettings.PassWord)
            })
            {
                if (emailSettings.WriteAsFile)
                {
                    smtpClient.DeliveryMethod = SmtpDeliveryMethod.SpecifiedPickupDirectory;
                    smtpClient.PickupDirectoryLocation = emailSettings.FileLocation;
                    smtpClient.EnableSsl = false;
                }

                StringBuilder body = new StringBuilder()
                                   .AppendLine("A new order has been submitted")
                                   .AppendLine("---")
                                   .AppendLine("Items:");

                foreach (var line in cart.CartLine)
                {
                    var subtotal = line.Key.Price * line.Value;
                    body.AppendLine($"{line.Value} * {line.Key.Name}");
                }

                MailMessage mailMessage = new MailMessage(emailSettings.MailFromAddress, emailSettings.MailToAddress, "New order", body.ToString());

                if (emailSettings.WriteAsFile)
                {
                    mailMessage.BodyEncoding = Encoding.ASCII;
                }
                smtpClient.Send(mailMessage);
            }
        }
    }

    /// <summary>
    /// 邮件设置信息
    /// </summary>
    public class EmailSettings
    {
        public string MailToAddress { get; set; } = "orders@example.com";
        public string MailFromAddress { get; set; } = "sportsstore@example.com";
        public bool UseSsl { get; set; } = true;
        public string UserName { get; set; } = "MySmtpUserName";
        public string PassWord { get; set; } = "MySmtpPassWord";
        public string ServerName { get; set; } = "smtp.example.com";
        public int ServerPort { get; set; } = 587;
        public bool WriteAsFile { get; set; } = true;
        public string FileLocation { get; set; } = @"c:\sports_store_emails";
    }
}
