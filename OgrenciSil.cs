using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.OleDb;
using System.Diagnostics;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace OkulOtomasyonSitemi
{
    public partial class OgrenciSil : Form
    {

        OleDbConnection conn = new OleDbConnection("Provider=Microsoft.ACE.Oledb.12.0;Data Source = C:\\Users\\yakup\\OneDrive\\Masaüstü\\OgrenciOtomation1.accdb");


        public OgrenciSil()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {

            if( textBox1.Text == "" )
            {
                MessageBox.Show("Lütfen Bir Okul No Giriniz!", "Okul No Giriniz.", MessageBoxButtons.OK, MessageBoxIcon.Asterisk);
            } else
            {
                conn.Open();

                OleDbCommand cmd = new OleDbCommand("Select * from ogrenciler where okulno='" + textBox1.Text + "'", conn);
                OleDbDataReader read = cmd.ExecuteReader();
                if (read.Read() == true)
                {
                    if (textBox1.Text == read["okulno"].ToString())
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
                    MessageBox.Show(textBox1.Text + " Okul Numaralı Öğrenci Sistemde Kayıtlı Bulunamadı!", "Bulunamadı", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }

                conn.Close();
            }

            
        }

        private void textBoxTC_TextChanged(object sender, EventArgs e)
        {
           
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
                MessageBox.Show("Lütfen Bir Okul No Giriniz!", "Okul No Giriniz.", MessageBoxButtons.OK, MessageBoxIcon.Asterisk);
            }
            else
            {
                DialogResult result = MessageBox.Show("Öğrencinin  Bilgilerini Gerçekten Silmek İstediğinize Emin Misiniz ?", "Emin Misin?", MessageBoxButtons.OKCancel, MessageBoxIcon.Question);
                if (result == DialogResult.OK)
                {
                    conn.Open();

                    OleDbCommand cmd = new OleDbCommand("DELETE FROM ogrenciler WHERE okulno=?", conn);
                    cmd.Parameters.AddWithValue("@okulno", textBox1.Text);
                    int tempResult = cmd.ExecuteNonQuery();
                    if (tempResult > 0)
                    {
                        MessageBox.Show("Öğrenci Bilgileri Başarıyla Silindi.", "Başarılı", MessageBoxButtons.OK, MessageBoxIcon.Information);

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
                    else
                    {
                        MessageBox.Show("Öğrenci silme işlemi başarısız oldu.", "Başarısız", MessageBoxButtons.OK, MessageBoxIcon.Error);

                    }
                    conn.Close();
                }
            }
                

            
        }

        private void OgrenciSil_Load(object sender, EventArgs e)
        {

        }

        private void çıkışToolStripMenuItem_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void yardımAlToolStripMenuItem_Click(object sender, EventArgs e)
        {
            MessageBox.Show("Bilgilerini Silmek İstediğiniz Öğrencinin , Öğrenci Numarasını Girip , Silme Tuşuna Basabilirsiniz.", "Yardım",MessageBoxButtons.OK,MessageBoxIcon.Information);
        }
    }
}
