using System;
using System.Net;
using System.Net.Mail; // Library to send mail
using System.Windows;

namespace MecProgramming.Tools
{
    /// <summary>
    /// Class that enables the user to send email via the GUI
    /// </summary>
    public class MailSender
    {
        /// <summary>
        /// Name of the SMTP server to connect on
        /// </summary>
        private const string SMTP_CLIENT_NAME = "smtp.gmail.com";

        /// <summary>
        /// Connection port for SMTP using gmail
        /// </summary>
        private const int SMTP_PORT = 587;

        /// <summary>
        /// Email address with want to connect to
        /// Just a temporary address for now
        /// </summary>
        private const string SENDER_MAIL_ADDRESS = "don.l.speaks@gmail.com";

        /// <summary>
        /// Password to connect to the gmail account
        /// Just a temporary address for now
        /// </summary>
        private const string PASSWORD = "DonSpeaks123";


        /// <summary>
        /// Send an email to itself demaonstration
        /// In the futur we can change the receiver of the email
        /// </summary>
        /// <param name="content">Content of the mail to send</param>
        public void SendMail(string content)
        {
            try
            {
                using (MailMessage mail = new MailMessage())
                {
                    mail.From = new MailAddress(SENDER_MAIL_ADDRESS);
                    mail.To.Add(SENDER_MAIL_ADDRESS);                   // Address of the receiver
                    mail.Subject = "Don speaks";                        // Subject of the mail
                    mail.Body = content;                                // Content of the mail (in the parameters)
                    mail.IsBodyHtml = true;

                    using (SmtpClient smtp = new SmtpClient(SMTP_CLIENT_NAME, SMTP_PORT))
                    {
                        smtp.Credentials = new NetworkCredential(SENDER_MAIL_ADDRESS, PASSWORD); // Connect to the SMTP server to send the mail
                        smtp.EnableSsl = true; // Enable SSL for security
                        smtp.Send(mail);       // Send the mail
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
