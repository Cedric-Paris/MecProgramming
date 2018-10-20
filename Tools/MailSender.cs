using System;
using System.Net;
using System.Net.Mail;
using System.Windows;

namespace MecProgramming.Tools
{
    public class MailSender
    {

        private const string SMTP_CLIENT_NAME = "smtp.gmail.com";
        private const int SMTP_PORT = 587;

        private const string SENDER_MAIL_ADDRESS = "don.l.speaks@gmail.com";
        private const string PASSWORD = "DonSpeaks123";


        public void SendMail(string content)
        {
            try
            {
                using (MailMessage mail = new MailMessage())
                {
                    mail.From = new MailAddress(SENDER_MAIL_ADDRESS);
                    mail.To.Add(SENDER_MAIL_ADDRESS);
                    mail.Subject = "Don speaks";
                    mail.Body = content;
                    mail.IsBodyHtml = true;

                    using (SmtpClient smtp = new SmtpClient(SMTP_CLIENT_NAME, SMTP_PORT))
                    {
                        smtp.Credentials = new NetworkCredential(SENDER_MAIL_ADDRESS, PASSWORD);
                        smtp.EnableSsl = true;
                        smtp.Send(mail);
                    }
                }
            }
            catch(Exception e)
            {
                MessageBox.Show(e.Message);
            }

        }
    }
}
