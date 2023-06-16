using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.OleDb;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using static System.Windows.Forms.VisualStyles.VisualStyleElement.Button;
using static System.Windows.Forms.VisualStyles.VisualStyleElement;
using Sipaa.Framework;
using static System.Windows.Forms.VisualStyles.VisualStyleElement.ListView;

namespace OkulOtomasyonSitemi
{
    public partial class SifremiUnuttum : Form
    {
        int mailBulunmadi = 0;
        OleDbConnection conn = new OleDbConnection("Provider=Microsoft.ACE.Oledb.12.0;Data Source = C:\\Users\\yakup\\OneDrive\\Masaüstü\\OgrenciOtomation1.accdb");

        public SifremiUnuttum()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (textBox1.Text == "")
            {
                MessageBox.Show("Lütfen E-Mail Adresinizi Giriniz!", "E-Mail Giriniz.", MessageBoxButtons.OK, MessageBoxIcon.Asterisk);
            }
            else
            {
                conn.Open();

                OleDbCommand cmd = new OleDbCommand("Select * from kullanicilar where eposta='" + textBox1.Text + "'", conn);
                OleDbDataReader read = cmd.ExecuteReader();
                if (read.Read() == true)
                {
                    if (textBox1.Text == read["eposta"].ToString())
                    {
                        conn.Close();
                        bunifuPages1.SetPage("SifremiUnuttum2");
                        guvenlikSorusuGoster();
                        
                    }
                }
                else
                {
                    MessageBox.Show(textBox1.Text + " Mailli Kullanıcı Sistemde Kayıtlı Bulunamadı!", "Bulunamadı", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                conn.Close();
            }
        }

        private void labelGuvenlikSorusu_Click(object sender, EventArgs e)
        {

        }

        private void guvenlikSorusuGoster()
        {
            conn.Open();

            string guvenlik_sorusu = "";
            OleDbCommand cmd = new OleDbCommand("SELECT * FROM kullanicilar WHERE eposta = @eposta", conn);
            cmd.Parameters.AddWithValue("@eposta", textBox1.Text);

            OleDbDataReader read = cmd.ExecuteReader();
            if (read.Read())
            {
                labelGuvenlikSorusu.Text = read["guvenlik_sorusu"].ToString(); // güvenlik sorusunu label'a yazdır
                conn.Close();
            }
        }

        private void button2_Click(object sender, EventArgs e)  // Güvenlik sorusu cevabı için buton
        {
            conn.Open();

            string email = textBox1.Text; // kullanıcının girdiği e-posta adresi
            string cevap = textBoxCevap.Text; // kullanıcının girdiği güvenlik sorusu cevabı
            OleDbCommand cmd = new OleDbCommand("SELECT * FROM kullanicilar WHERE eposta='" + email + "' AND guvenlik_sorusu_cevabi='" + cevap + "'", conn);
            OleDbDataReader read = cmd.ExecuteReader();
            if (read.Read() == true)
            {
                bunifuPages1.SetPage("SifremiUnuttum3"); // eşleşme varsa sayfa geçişi yap
            }
            else
            {
                MessageBox.Show("Güvenlik sorusu cevabı yanlış!", "Hata", MessageBoxButtons.OK, MessageBoxIcon.Error); // eşleşme yoksa hata mesajı göster
                textBoxCevap.Text = "";
            }
            conn.Close();
        }

        private void tabPage2_Click(object sender, EventArgs e)
        {

        }

        private void button3_Click(object sender, EventArgs e)
        {
            if (textBoxYeniSifre1.Text == textBoxYeniSifre2.Text)
            {
                string email = textBox1.Text; // kullanıcının girdiği e-posta adresi
                string sifre = textBoxYeniSifre1.Text;
                string yenidenSifre = textBoxYeniSifre1.Text;

                conn.Open();
                OleDbCommand cmd = new OleDbCommand("UPDATE kullanicilar SET sifre=@sifre , yenidensifre=@yenidensifre WHERE eposta=@eposta", conn);
                cmd.Parameters.AddWithValue("@sifre", sifre);
                cmd.Parameters.AddWithValue("@eposta", email);
                cmd.Parameters.AddWithValue("@yenidensifre", yenidenSifre);

                OleDbCommand cmd2 = new OleDbCommand("UPDATE kullanici_girisi SET sifre=@sifre2  WHERE emailadres_kullaniciadi=@email", conn);
                cmd2.Parameters.AddWithValue("@sifre2", sifre);
                cmd2.Parameters.AddWithValue("@email", email);

                int result2 = cmd2.ExecuteNonQuery();
                int result = cmd.ExecuteNonQuery();
                if (result > 0 && result2 > 0)
                {
                    MessageBox.Show("Şifre başarıyla güncellendi.", "Başarılı", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                else
                {
                    if (result == 0)
                    {
                        MessageBox.Show("E-posta adresi bulunamadı veya şifre zaten güncel.", "Hata", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                    else
                    {
                        MessageBox.Show("Şifre güncellenirken bir hata oluştu.", "Hata", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }

                conn.Close();
            }
            else
            {
                MessageBox.Show("Lütfen Girilen Şifreleriniz Aynı Olsun!", "Şifreler Aynı Değil.", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void tabPage3_Click(object sender, EventArgs e)
        {

        }
    }
}
