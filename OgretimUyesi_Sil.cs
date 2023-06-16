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
    public partial class OgretimUyesi_Sil : Form
    {

        OleDbConnection conn = new OleDbConnection("Provider=Microsoft.ACE.Oledb.12.0;Data Source = C:\\Users\\yakup\\OneDrive\\Masaüstü\\OgrenciOtomation1.accdb");

        public OgretimUyesi_Sil()
        {
            InitializeComponent();
        }

        private void OgretimUyesi_Sil_Load(object sender, EventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {

            if( textBox1.Text == "" )
            {
                MessageBox.Show("Lütfen Bir Idari No Giriniz!", "İdari No Giriniz.", MessageBoxButtons.OK, MessageBoxIcon.Asterisk);
            } else
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

        private void button4_Click(object sender, EventArgs e)
        {

            if (textBox1.Text == "")
            {
                MessageBox.Show("Lütfen Bir Idari No Giriniz!", "İdari No Giriniz.", MessageBoxButtons.OK, MessageBoxIcon.Asterisk);
            }
            else
            {
                DialogResult result = MessageBox.Show("Öğretim Üyesinin Bilgilerini Gerçekten Silmek İstediğinize Emin Misiniz ?", "Emin Misin?", MessageBoxButtons.OKCancel, MessageBoxIcon.Question);
                if (result == DialogResult.OK)
                {
                    conn.Open();

                    OleDbCommand cmd = new OleDbCommand("DELETE FROM ogretimuyesi WHERE idarino=?", conn);
                    cmd.Parameters.AddWithValue("@idarino", textBox1.Text);
                    int tempResult = cmd.ExecuteNonQuery();
                    if (tempResult > 0)
                    {
                        MessageBox.Show("Öğrenci Bilgileri Başarıyla Silindi.", "Başarılı", MessageBoxButtons.OK, MessageBoxIcon.Information);

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
                    else
                    {
                        MessageBox.Show("Öğrenci silme işlemi başarısız oldu.", "Başarısız", MessageBoxButtons.OK, MessageBoxIcon.Error);

                    }
                    conn.Close();
                }
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

        private void çıkışToolStripMenuItem_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void yardımAlToolStripMenuItem_Click(object sender, EventArgs e)
        {
            MessageBox.Show("Bilgilerini Silmek İstediğiniz Öğretim Üyesinin , İdari Numarasını Girip , Silme Tuşuna Basabilirsiniz.", "Yardım", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }
    }
    
}
