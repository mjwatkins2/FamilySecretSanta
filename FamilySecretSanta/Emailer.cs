using System;
using System.Net.Mail;
using System.Windows.Forms;

namespace FamilySecretSanta
{
    class Emailer : IDisposable
    {
        private SmtpClient client;

        public Emailer()
        {
        }

        public void Dispose()
        {
            client?.Dispose();
        }

        public bool Connect()
        {
            try
            {
                client = new SmtpClient("smtp.gmail.com", 587)
                {
                    EnableSsl = true,
                    DeliveryMethod = SmtpDeliveryMethod.Network,
                    UseDefaultCredentials = false,
                    Credentials = new System.Net.NetworkCredential("group_email@gmail.com", "password"),
                    Timeout = 20000
                };
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
                return false;
            }
            return true;
        }

        public bool SendEmail(FamilyMember giftor, FamilyMember giftee1, FamilyMember giftee2)
        {
            string subject = "Family Secret Santa " + DateTime.Now.Year;
            string body = String.Format(
                "{0},\n\nThese are your official {3} Secret Santa giftees:\n" +
                "{1}, {2}\n\n" +
                "Rules:\n" +
                "Rule #1: You do not talk about Secret Santa Club.\n" +
                "Rule #2: You do not talk about Secret Santa Club.\n" +
                "Rule #3: Gift limit is $50.\n\n" +
                "Keep it a secret! ho ho ho.\n\n--Santa\n"
            , giftor.Name, giftee1.Name, giftee2.Name, DateTime.Now.Year);

            return SendEmail(giftor.Email, subject, body);
        }

        private bool SendEmail(string email, string subject, string body)
        {
            try
            {
                MailMessage mail = new MailMessage();
                mail.From = new MailAddress("group_email@gmail.com");
                mail.To.Add(email);
                mail.Subject = subject;
                mail.Body = body;
                client.Send(mail);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
                return false;
            }
            return true;
        }
    }
}
