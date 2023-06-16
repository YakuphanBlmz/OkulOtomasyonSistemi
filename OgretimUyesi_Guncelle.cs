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

namespace OkulOtomasyonSitemi
{
    public partial class OgretimUyesi_Guncelle : Form
    {

        OleDbConnection conn = new OleDbConnection("Provider=Microsoft.ACE.Oledb.12.0;Data Source = C:\\Users\\yakup\\OneDrive\\Masaüstü\\OgrenciOtomation1.accdb");

        public OgretimUyesi_Guncelle()
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

        private void button1_Click(object sender, EventArgs e)
        {
            if (textBox1.Text == "")
            {
                MessageBox.Show("Lütfen Bir Idari No Giriniz!", "İdari No Giriniz.", MessageBoxButtons.OK, MessageBoxIcon.Asterisk);
            }
            else
            {
                conn.Open();

                OleDbCommand cmd = new OleDbCommand("Select * from ogretimuyesi where idarino='" + textBox1.Text + "'", conn);
                OleDbDataReader read = cmd.ExecuteReader();
                if (read.Read() == true)
                {
                    if (textBox1.Text == read["idarino"].ToString())
                    {
                        textBoxName.Text = read["ad"].ToString();
                        textBoxSurname.Text = read["soyad"].ToString();
                        textBoxTC.Text = read["tc"].ToString();
                        comboBox1.Text = read["ulke"].ToString();
                        comboBox2.Text = read["sehir"].ToString();
                        textBoxPhone.Text = read["telefon"].ToString();
                        textBoxEposta.Text = read["eposta"].ToString();
                        comboBox3.Text = read["bolum"].ToString();
                        textBoxIdariNo.Text = read["idarino"].ToString();
                        dateTimePicker1.Text = read["dogumtarihi"].ToString();

                        if (read["cinsiyet"].ToString() == "Erkek")
                        {
                            radioButton1.Checked = true;
                        }
                        else if (read["cinsiyet"].ToString() == "Kadın")
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
                    MessageBox.Show(textBox1.Text + " İdari Numaralı Öğretim Üyesi Sistemde Kayıtlı Bulunamadı!", "Bulunamadı", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }

                conn.Close();
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            textBox1.Clear();
            textBoxIdariNo.Clear();
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

                DialogResult first_result = MessageBox.Show("Öğretim Üyesinin Bilgilerini Güncellemek İstediğinize Emin Misiniz?", "Emin Misin?", MessageBoxButtons.OKCancel, MessageBoxIcon.Question);
                if (first_result == DialogResult.OK)
                {
                    // Öğrenciyi sileriz.
                    OleDbCommand cmd = new OleDbCommand("DELETE FROM ogretimuyesi WHERE idarino=?", conn);
                    cmd.Parameters.AddWithValue("@idarino", textBox1.Text);
                    int result = cmd.ExecuteNonQuery();
                    if (result > 0)
                    {

                        // Öğrenci güncelleme için veri tabanına kaydedilen bilgiler.
                        OleDbCommand cmd2 = new OleDbCommand("INSERT INTO ogretimuyesi (idarino, tc,  ad, soyad, ulke, sehir, telefon, eposta, bolum, dogumtarihi, cinsiyet) VALUES (@idarino, @tc , @ad, @soyad, @ulke, @sehir, @telefon, @eposta, @bolum, @dogumtarihi, @cinsiyet)", conn);

                        cmd2.Parameters.AddWithValue("@idarino", textBoxIdariNo.Text);
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
                            MessageBox.Show("Öğretim Üyesi Bilgilerinin Güncel Hali Sisteme Eklendi!", "İşlem Başarılı.");
                            textBoxIdariNo.Clear();
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
                        MessageBox.Show("Öğretim Üyesi Güncelleme İşlemi Başarısız Oldu.", "Başarısız", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        conn.Close();
                    }
                }
            }

                
        }

        private void OgretimUyesi_Guncelle_Load(object sender, EventArgs e)
        {

        }
    }
}
