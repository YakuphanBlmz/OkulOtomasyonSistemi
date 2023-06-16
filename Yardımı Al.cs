using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Net.Mail;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace OkulOtomasyonSitemi
{
    public partial class Yardım : Form
    {

        MailMessage myMessage = new MailMessage();

        public Yardım()
        {
            InitializeComponent();
        }

        private void label1_Click(object sender, EventArgs e)
        {
            System.Diagnostics.Process.Start("https://mail.google.com/mail/u/0/#inbox?compose=GTvVlcSGMGFDVHgMwgpjFfxsPRkPxhcCmwrmHBWrpSGFpnwHnxZsgSkmNRklcGFRXspbcLgrMMTMV");
        }

        private void pictureBox1_Click(object sender, EventArgs e)
        {
            System.Diagnostics.Process.Start("https://www.instagram.com/istunedu/?hl=tr");
        }

        private void pictureBox2_Click(object sender, EventArgs e)
        {
            System.Diagnostics.Process.Start("https://www.facebook.com/istunedu/?locale=tr_TR");
        }

        private void pictureBox3_Click(object sender, EventArgs e)
        {
            System.Diagnostics.Process.Start("https://twitter.com/istunedu?ref_src=twsrc%5Egoogle%7Ctwcamp%5Eserp%7Ctwgr%5Eauthor");
        }

        private void button1_Click(object sender, EventArgs e)
        {
            Form4 acil_yardim = new Form4();
            acil_yardim.Show();
        }

        private void Yardım_Load(object sender, EventArgs e)
        {

        }

        private void çıkışToolStripMenuItem_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void çıkışToolStripMenuItem_Click_1(object sender, EventArgs e)
        {
            this.Close();
        }

        private void button1_Click_1(object sender, EventArgs e)
        {
            try
            {
                if (txtkonu.Text != "")
                {
                    if (txtmesaj.Text != "")
                    {

                        myMessage.To.Add("resmioyunbozanralph@hotmail.com");
                        myMessage.From = new MailAddress("yakuphan_bilmez312@hotmail.com");
                        myMessage.Subject = txtkonu.Text.ToString();
                        myMessage.Body = txtmesaj.Text.ToString();

                        SmtpClient smptc = new SmtpClient();

                        smptc.Credentials = new System.Net.NetworkCredential("resmioyunbozanralph@hotmail.com", "oyunbozanralph123+");
                        smptc.Host = "smtp.live.com";
                        smptc.EnableSsl = true;
                        smptc.Port = 587;   

                        smptc.Send(myMessage);


                        // Outlook kolay olduğundan yaptım. İstenirse Gmail ile de yapılabilir. Yalnız Gmail'de güvenlik ayarını yapmayı unutma.
                        MessageBox.Show("Mesajınız bizlere geldi. Çok teşekkür ederiz.", "Başarılı", MessageBoxButtons.OK, MessageBoxIcon.Information);

                    }
                    else
                    {
                        MessageBox.Show("Lütfen Mesajınızı Giriniz.", "Mesaj Gir", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }
                }
                else
                {
                    MessageBox.Show("Lütfen Mesaj Konusunu Giriniz.", "Konu Gir", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }

            } catch (Exception ex) 
            {
                MessageBox.Show("İletinizi Gönderirken Bir Hata ile Karşılaşıldı. Hata :" + ex.ToString(), "Hata", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            
            
        }
    }
}
