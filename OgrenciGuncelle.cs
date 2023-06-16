using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.OleDb;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;
using static System.Windows.Forms.VisualStyles.VisualStyleElement;

namespace OkulOtomasyonSitemi
{
    public partial class OgrenciGuncelle : Form
    {

        OleDbConnection conn = new OleDbConnection("Provider=Microsoft.ACE.Oledb.12.0;Data Source = C:\\Users\\yakup\\OneDrive\\Masaüstü\\OgrenciOtomation1.accdb");

        public OgrenciGuncelle()
        {
            InitializeComponent();
        }

        private void yardımAlToolStripMenuItem_Click(object sender, EventArgs e)
        {
            MessageBox.Show("Bilgilerini Güncellemek İstediğiniz Öğrencinin Okul Numarasını Giriniz. Çıkan Bilgileri Düzenleyerek Güncelleme İşlemini Tamamlayınız.", "Yardım Al");
        }

        private void çıkışToolStripMenuItem_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void OgrenciGuncelle_Load(object sender, EventArgs e)
        {

        }

        // Okul no sayı biçiminde alıp sorgulatma işleminde sıkıntı çıktı. String halletmeye çalış.
        private void button1_Click(object sender, EventArgs e)
        {
            conn.Open();

            OleDbCommand cmd = new OleDbCommand("Select * from ogrenciler where okulno='" + textBox1.Text + "'", conn);
            OleDbDataReader read = cmd.ExecuteReader();
            if( read.Read() == true )
            {
                if (textBox1.Text == read["okulno"].ToString() )
                {
                    textBoxName.Text = read["ad"].ToString();
                    textBoxSurname.Text = read["soyad"].ToString();
                    textBoxTC.Text = read["tc"].ToString();
                    comboBox1.Text = read["ulke"].ToString();
                    comboBox2.Text = read["sehir"].ToString();
                    textBoxPhone.Text = read["telefon"].ToString();
                    textBoxEposta.Text = read["eposta"].ToString();
                    comboBox3.Text = read["bolum"].ToString();
                    textBoxOkulno.Text = read["okulno"].ToString();
                    dateTimePicker1.Text = read["dogumtarihi"].ToString();
                    
                    if (read["cinsiyet"].ToString() == "Erkek" )
                    {
                        radioButton1.Checked = true;
                    } else if(read["cinsiyet"].ToString() == "Kadın")
                    {
                        radioButton2.Checked = true;
                    }
                    else
                    {
                        radioButton3.Checked = true;
                    }
                }
            }
            else
            {
                MessageBox.Show(textBox1.Text + " Okul Numaralı Öğrenci Sistemde Kayıtlı Bulunamadı!" , "Bulunamadı" , MessageBoxButtons.OK,MessageBoxIcon.Information);
            }

            conn.Close();

        }

        private void textBoxTC_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!char.IsControl(e.KeyChar) && !char.IsDigit(e.KeyChar))
            {
                e.Handled = true;
            }
        }

        private void textBoxPhone_TextChanged(object sender, EventArgs e)
        {

        }

        private void textBoxPhone_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!char.IsControl(e.KeyChar) && !char.IsDigit(e.KeyChar))
            {
                e.Handled = true;
            }
        }

        private void textBoxOkulno_TextChanged(object sender, EventArgs e)
        {

        }

        private void textBoxOkulno_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!char.IsControl(e.KeyChar) && !char.IsDigit(e.KeyChar))
            {
                e.Handled = true;
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            textBox1.Clear();
            textBoxOkulno.Clear();
            textBoxTC.Clear();
            textBoxName.Clear();
            textBoxSurname.Clear();
            comboBox1.Text = "";
            comboBox2.Text = "";
            textBoxPhone.Clear();
            textBoxEposta.Clear();
            comboBox3.Text = "";
            dateTimePicker1.Value = DateTime.Now;
            radioButton1.Checked = false;
            radioButton2.Checked = false;
            radioButton3.Checked = false;
        }

        private void button4_Click(object sender, EventArgs e)
        {
            

            if (textBox1.Text == "")
            {
                MessageBox.Show("Lütfen Bir Idari No Giriniz!", "İdari No Giriniz.", MessageBoxButtons.OK, MessageBoxIcon.Asterisk);
            }
            else
            {
                conn.Open();

                DialogResult first_result = MessageBox.Show("Öğrencinin Bilgilerini Güncellemek İstediğinize Emin Misiniz?", "Emin Misin?", MessageBoxButtons.OKCancel, MessageBoxIcon.Question);
                if (first_result == DialogResult.OK)
                {
                    // Öğrenciyi sileriz.
                    OleDbCommand cmd = new OleDbCommand("DELETE FROM ogrenciler WHERE okulno=?", conn);
                    cmd.Parameters.AddWithValue("@okulno", textBox1.Text);
                    int result = cmd.ExecuteNonQuery();
                    if (result > 0)
                    {
                        // Öğrenci güncelleme için veri tabanına kaydedilen bilgiler.
                        OleDbCommand cmd2 = new OleDbCommand("INSERT INTO ogrenciler (okulno, tc,  ad, soyad, ulke, sehir, telefon, eposta, bolum, dogumtarihi, cinsiyet) VALUES (@okulno, @tc , @ad, @soyad, @ulke, @sehir, @telefon, @eposta, @bolum, @dogumtarihi, @cinsiyet)", conn);

                        cmd2.Parameters.AddWithValue("@okulno", textBoxOkulno.Text);
                        cmd2.Parameters.AddWithValue("@tc", textBoxTC.Text);
                        cmd2.Parameters.AddWithValue("@ad", textBoxName.Text);
                        cmd2.Parameters.AddWithValue("@soyad", textBoxSurname.Text);
                        cmd2.Parameters.AddWithValue("@ulke", comboBox1.Text);
                        cmd2.Parameters.AddWithValue("@sehir", comboBox2.Text);
                        cmd2.Parameters.AddWithValue("@telefon", textBoxPhone.Text);
                        cmd2.Parameters.AddWithValue("@eposta", textBoxEposta.Text);
                        cmd2.Parameters.AddWithValue("@bolum", comboBox3.Text);
                        cmd2.Parameters.AddWithValue("@dogumtarihi", dateTimePicker1.Text);
                        cmd2.Parameters.AddWithValue("@cinsiyet", radioButton1.Checked ? radioButton1.Text : radioButton2.Checked ? radioButton2.Text : radioButton3.Text);

                        try
                        {
                            cmd2.ExecuteNonQuery();
                            conn.Close();
                            MessageBox.Show("Öğrenci Bilgilerinin Güncel Hali Sisteme Eklendi!", "İşlem Başarılı.");
                            textBoxOkulno.Clear();
                            textBoxTC.Clear();
                            textBoxName.Clear();
                            textBoxSurname.Clear();
                            comboBox1.Text = "";
                            comboBox2.Text = "";
                            textBoxPhone.Clear();
                            textBoxEposta.Clear();
                            comboBox3.Text = "";
                            dateTimePicker1.Value = DateTime.Now;
                            radioButton1.Checked = false;
                            radioButton2.Checked = false;
                            radioButton3.Checked = false;

                        }
                        catch (Exception ex)
                        {
                            MessageBox.Show("Bir Hata Oluştu : " + ex.Message, "Başarısız!");
                            conn.Close();
                        }
                        finally
                        {
                            conn.Close();
                        }
                    }
                    else
                    {
                        MessageBox.Show("Öğrenci Güncelleme İşlemi Başarısız Oldu.", "Başarısız", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        conn.Close();
                    }
                }
            }

                
        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {

        }
    }
}
