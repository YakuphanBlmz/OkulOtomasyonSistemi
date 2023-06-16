using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Net;
using System.Net.Mail;

namespace OkulOtomasyonSitemi
{
    public partial class Form4 : Form
    {

        

        public Form4()
        {
            InitializeComponent();

        }

        private void button1_Click(object sender, EventArgs e)
        {
            SmtpClient smptc = new SmtpClient();
            smptc.Port = 587;
            smptc.Host = "smtp.mail.com";
            smptc.EnableSsl = true;
            // E posta ve şifre bilerek girilmedi.
            smptc.Credentials = new NetworkCredential("yakuphandene@gmail.com", "deneme123");

            MailMessage mail = new MailMessage();


            mail.From = new MailAddress(txtemail.Text.ToString(), txtad.Text.ToString());
            mail.To.Add("yakuphan.bilmez@istun.edu.tr");
            mail.Subject = txtkonu.Text.ToString();
            mail.IsBodyHtml = true;
            mail.Body = txtmesaj.Text.ToString();

            smptc.Send(mail);
        }
    }
}
