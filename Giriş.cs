using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Data.OleDb;
using System.Net.Mail;

namespace OkulOtomasyonSitemi
{
    public partial class Giriş : Form
    {

        public static string userName; // Kullanıcının adini tutmak icin
        public static string flagMail; // Kullanıcının mailini tutmak için

        OleDbConnection conn  = new OleDbConnection("Provider=Microsoft.ACE.Oledb.12.0;Data Source = C:\\Users\\yakup\\OneDrive\\Masaüstü\\OgrenciOtomation1.accdb");

        public Giriş()
        {
            InitializeComponent();

        }

        private void pictureBox1_Click(object sender, EventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {
            if( textBox1.Text == "" || textBox2.Text == "" )
            {
                MessageBox.Show("Giriş Yapmak İçin Tüm Alanları Doldurun.", "Başarısız.");
            }
            else
            {
                conn.Open();
                OleDbCommand cmd = new OleDbCommand("Select * from kullanici_girisi where emailadres_kullaniciadi='" + textBox1.Text+"'" , conn);
                OleDbDataReader read = cmd.ExecuteReader();
                if( read.Read() == true )
                {
                    if(textBox1.Text == read["emailadres_kullaniciadi"].ToString() && textBox2.Text == read["sifre"].ToString())
                    { 
                        MessageBox.Show("Hoş Geldiniz! " + read["ad"].ToString());

                        userName = read["ad"].ToString();
                        flagMail = read["emailadres_kullaniciadi"].ToString();

                        // Form1 newForm = new Form1();
                        // newForm.Show();

                        MainPage mp = new MainPage();
                        mp.Show();

                        // Anasayfa Anasayfa = new Anasayfa();
                        // Anasayfa.Show();
                        
                        

                        this.Hide();
                    }
                    else
                    {
                        MessageBox.Show("HATA! Giriş Bilgilerinizi Kontrol Edin." , "Başarısız");
                        textBox1.Text = "";
                        textBox2.Text = "";
                    }
                }
                else
                {
                    MessageBox.Show("HATA! Giriş Bilgilerinizi Kontrol Edin.", "Başarısız");
                    textBox1.Text = "";
                    textBox2.Text = "";
                }
                conn.Close();
            }
        }

        private void pictureBoxEye_Click(object sender, EventArgs e)
        {
            textBox2.PasswordChar = '*';
            pictureBoxEye.Hide();
            pictureBoxHidden.Show();
        }

        private void pictureBoxHidden_Click(object sender, EventArgs e)
        {
            textBox2.PasswordChar = '\0';
            pictureBoxHidden.Hide();
            pictureBoxEye.Show();
        }

        private void label3_Click(object sender, EventArgs e)
        {

        }

        private void button2_Click(object sender, EventArgs e)
        {
            IdariKayit idariekle = new IdariKayit();
            idariekle.Show();
        }

        private void Giriş_Load(object sender, EventArgs e)
        {

        }

        public string GetUserName()
        {
            return userName;
        }

        public string GetFlagMail()
        {
            if (flagMail == null)
            {
                flagMail = "";
            }
            return flagMail;
        }

        private void groupBox1_Enter(object sender, EventArgs e)
        {

        }

        private void label4_Click(object sender, EventArgs e)
        {
            SifremiUnuttum sifre = new SifremiUnuttum();
            sifre.Show();
        }

        private void pictureBox3_Click(object sender, EventArgs e)
        {
            DialogResult = MessageBox.Show("Programdan Çıkmak İstediğinize Emin Misiniz?", "Emin Misin?", MessageBoxButtons.OKCancel, MessageBoxIcon.Question);
            if (DialogResult == DialogResult.OK)
            {
                Environment.Exit(0);
            }
        }
    }
}
