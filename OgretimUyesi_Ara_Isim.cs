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
    public partial class OgretimUyesi_Ara_Isim : Form
    {

        OleDbConnection conn = new OleDbConnection("Provider=Microsoft.ACE.Oledb.12.0;Data Source = C:\\Users\\yakup\\OneDrive\\Masaüstü\\OgrenciOtomation1.accdb");

        public OgretimUyesi_Ara_Isim()
        {
            InitializeComponent();
        }

        private void yardımAlToolStripMenuItem_Click(object sender, EventArgs e)
        {
            MessageBox.Show("Öğretim Üyesinin Adını Yazıp Arama İşleminizi Gerçekleştirebilirsiniz.", "Bilgi", MessageBoxButtons.OK, MessageBoxIcon.Question);
        }

        private void çıkışToolStripMenuItem_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {
            
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (textBox1.Text == "")
            {
                MessageBox.Show("Lütfen Öğretim Üyesi İsmini Giriniz!", "İsim Girin.", MessageBoxButtons.OK, MessageBoxIcon.Asterisk);
            }
            else
            {
                conn.Open();

                OleDbCommand cmd = new OleDbCommand("SELECT * FROM ogretimuyesi WHERE ad=?", conn);
                cmd.Parameters.AddWithValue("@ad", textBox1.Text);

                OleDbDataReader read = cmd.ExecuteReader();

                listView1.Items.Clear();  // ListView'i temizle

                while (read.Read())
                {
                    ListViewItem item = new ListViewItem();
                    item.Text = read["ad"].ToString(); // öğretim üyesi adını ListView'e ekle
                    item.SubItems.Add(read["soyad"].ToString());  // öğretim üyesi soyadını ListView'e ekle
                    item.SubItems.Add(read["bolum"].ToString());  // öğretim üyesi bölümünü ListView'e ekle
                    item.SubItems.Add(read["idarino"].ToString());  // öğretim üyesi idari numarasını ListView'e ekle
                    item.SubItems.Add(read["tc"].ToString());  // öğretim üyesi TC'sini ListView'e ekle
                    item.SubItems.Add(read["telefon"].ToString());  // öğretim üyesi telefonunuListView'e ekle
                    item.SubItems.Add(read["eposta"].ToString());  // öğretim üyesi e-postasını ListView'e ekle
                    item.SubItems.Add(read["cinsiyet"].ToString());  // öğretim üyesi cinsiyetini ListView'e ekle

                    // ÖNEMLİ : item.Text = ... fonksiyonu varsa en başa koyulmalıdır. Text() fonksiyonunu ilk alır.

                    listView1.Items.Add(item);  // ListView'e öğretim üyesi bilgilerini ekle
                }

                conn.Close();
            }
        }
    }
}
