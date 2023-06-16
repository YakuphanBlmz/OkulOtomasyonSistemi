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
using static System.Windows.Forms.VisualStyles.VisualStyleElement;

namespace OkulOtomasyonSitemi
{
    public partial class OgrenciAra_Bolum : Form
    {

        OleDbConnection conn = new OleDbConnection("Provider=Microsoft.ACE.Oledb.12.0;Data Source = C:\\Users\\yakup\\OneDrive\\Masaüstü\\OgrenciOtomation1.accdb");

        public OgrenciAra_Bolum()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
        
            if( comboBox3.Text == "" )
            {
                MessageBox.Show("Lütfen Bölüm İçin Bir Seçim Yapınız!", "Seçim Yapınız.", MessageBoxButtons.OK, MessageBoxIcon.Asterisk);
            } else
            {
                conn.Open();

                OleDbCommand cmd = new OleDbCommand("SELECT * FROM ogrenciler WHERE bolum=?", conn);
                cmd.Parameters.AddWithValue("@bolum", comboBox3.Text);

                OleDbDataReader read = cmd.ExecuteReader();

                listView1.Items.Clear();  // ListView'i temizle

                while (read.Read())
                {
                    ListViewItem item = new ListViewItem();
                    item.Text = read["ad"].ToString(); // öğrenci adını ListView'e ekle
                    item.SubItems.Add(read["soyad"].ToString());  // öğrenci soyadını ListView'e ekle
                    item.SubItems.Add(read["bolum"].ToString());  // öğrenci bölümünü ListView'e ekle
                    item.SubItems.Add(read["okulno"].ToString());  // öğrenci okul numarasını ListView'e ekle
                    item.SubItems.Add(read["tc"].ToString());  // öğrenci TC'sini ListView'e ekle
                    item.SubItems.Add(read["telefon"].ToString());  // öğrenci telefonunuListView'e ekle
                    item.SubItems.Add(read["eposta"].ToString());  // öğrenci e-postasını ListView'e ekle
                    item.SubItems.Add(read["cinsiyet"].ToString());  // öğrenci cinsiyetini ListView'e ekle

                    // ÖNEMLİ : item.Text = ... fonksiyonu varsa en başa koyulmalıdır. Text() fonksiyonunu ilk alır.

                    listView1.Items.Add(item);  // ListView'e öğrenci bilgilerini ekle
                }

                conn.Close();
            } 

            
        }

        private void listView1_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void yardımAlToolStripMenuItem_Click(object sender, EventArgs e)
        {
            MessageBox.Show("Öğrencinin Okuduğu Bölümü Seçip Arama İşleminizi Gerçekleştirebilirsiniz.", "Bilgi", MessageBoxButtons.OK, MessageBoxIcon.Question);
        }

        private void çıkışToolStripMenuItem_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void yardımToolStripMenuItem_Click(object sender, EventArgs e)
        {

        }

        private void menuStrip1_ItemClicked(object sender, ToolStripItemClickedEventArgs e)
        {

        }
    }
}
