using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Windows.Forms.DataVisualization.Charting;
using System.Data.OleDb;
using static System.Windows.Forms.VisualStyles.VisualStyleElement;
using System.Collections;


namespace OkulOtomasyonSitemi
{
    public partial class Form1 : Form
    {
        public static int flagForEvent = 0;   // Butona göre işlem sayfasına götürür.
        OleDbConnection conn = new OleDbConnection("Provider=Microsoft.ACE.Oledb.12.0;Data Source = C:\\Users\\yakup\\OneDrive\\Masaüstü\\OgrenciOtomation1.accdb");

        public Form1()
        {
            InitializeComponent();
        }

        private void pictureBox1_Paint(object sender, PaintEventArgs e)
        {
            GraphicsPath gp = new GraphicsPath();
            gp.AddEllipse(0, 0, pictureBox1.Width - 1, pictureBox1.Height - 1);
            Region rg = new Region(gp);
            pictureBox1.Region = rg;
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            kullanici();
            ogrenciSayisi();
            idariSayisi();
            ogretimUyesiSayisi();
            enCokBolum();
            

            // Chart alanı ve serisi oluşturulur
            chart1.Series["Bolumler"].Points.AddXY("Yazılım Mühendisliği", 10);
            chart1.Series["Bolumler"].Points.AddXY("Bilgisayar Mühendisliği", 20);
        }

        private void label5_Click(object sender, EventArgs e)
        {

        }

        private void panel8_Paint(object sender, PaintEventArgs e)
        {

        }

        private void label12_Click(object sender, EventArgs e)  
        {
            
        }

        public void kullanici()     // Kullanici Adini Belirler.
        {
            if (1 == 1)
            {
                string username = Giriş.userName;
                label1.Text = username;
            } 
        }
    

        public void ogrenciSayisi()     // Güncel Öğrenci Sayısı
        {

            OleDbConnection conn = new OleDbConnection("Provider=Microsoft.ACE.Oledb.12.0;Data Source = C:\\Users\\yakup\\OneDrive\\Masaüstü\\OgrenciOtomation1.accdb");

            try
            {
                conn.Open();

                OleDbCommand cmd = new OleDbCommand("SELECT COUNT(*) FROM ogrenciler ", conn);
                int rowCount = (int)cmd.ExecuteScalar();

                label12.Text = rowCount.ToString();

            }
            catch (Exception ex)
            {
                MessageBox.Show("Öğrenci Sayısı Gösterilirken Bir Hata Oluştu: " + ex.Message);
            }
            finally
            {
                if (conn != null)
                {
                    conn.Close();
                }
            }


        }

        public void idariSayisi()
        {
            OleDbConnection conn = new OleDbConnection("Provider=Microsoft.ACE.Oledb.12.0;Data Source = C:\\Users\\yakup\\OneDrive\\Masaüstü\\OgrenciOtomation1.accdb");

            try
            {
                conn.Open();

                OleDbCommand cmd = new OleDbCommand("SELECT COUNT(*) FROM kullanicilar ", conn);
                int rowCount = (int)cmd.ExecuteScalar();

                label8.Text = rowCount.ToString();

            }
            catch (Exception ex)
            {
                MessageBox.Show("İdari Kullanıcı Sayısı Gösterilirken Bir Hata Oluştu: " + ex.Message);
            }
            finally
            {
                if (conn != null)
                {
                    conn.Close();
                }
            }
        }

        public void ogretimUyesiSayisi()
        {
            OleDbConnection conn = new OleDbConnection("Provider=Microsoft.ACE.Oledb.12.0;Data Source = C:\\Users\\yakup\\OneDrive\\Masaüstü\\OgrenciOtomation1.accdb");

            try
            {
                conn.Open();

                OleDbCommand cmd = new OleDbCommand("SELECT COUNT(*) FROM ogretimuyesi ", conn);
                int rowCount = (int)cmd.ExecuteScalar();

                label16.Text = rowCount.ToString();

            }
            catch (Exception ex)
            {
                MessageBox.Show("İdari Kullanıcı Sayısı Gösterilirken Bir Hata Oluştu: " + ex.Message);
            }
            finally
            {
                if (conn != null)
                {
                    conn.Close();
                }
            }
        }

        public void enCokBolum()
        {
            OleDbConnection conn = new OleDbConnection("Provider=Microsoft.ACE.Oledb.12.0;Data Source = C:\\Users\\yakup\\OneDrive\\Masaüstü\\OgrenciOtomation1.accdb");

            try
            {
                conn.Open();

                OleDbDataAdapter adapter = new OleDbDataAdapter("SELECT * FROM ogrenciler", conn);
                DataTable dt = new DataTable();
                adapter.Fill(dt);

                var result = from row in dt.AsEnumerable()
                             group row by row.Field<string>("bolum") into grp
                             select new
                             {
                                 Element = grp.Key,
                                 Count = grp.Count()
                             };

                /*
                    "var result =" : "result" adında bir değişken tanımlanır.
                    "from row in dt.AsEnumerable()" : "dt" DataTable nesnesindeki her bir satır "row" adlı bir değişkene atılır. AsEnumerable() metodu, DataTable'yı IEnumerable<T> arayüzüne dönüştürür.
                    "group row by row.Field<string>("sütun_adı") into grp" : "row" değişkenindeki her bir satır, "sütun_adı" adlı sütuna göre gruplandırılır ve "grp" adlı bir değişkene atanır.
                    "select new { Element = grp.Key, Count = grp.Count() }" : "grp" değişkenindeki her bir grup için, bir anonim tipte bir nesne oluşturulur ve "result" değişkenine atılır. Bu anonim tipte nesne, "Element" ve "Count" adlı iki özelliğe sahiptir. "Element", grup anahtarını (yani, "sütun_adı" sütunundaki bir elemanı) ve "Count", o gruptaki eleman sayısını (yani, "sütun_adı" sütununda kaç tane aynı eleman olduğunu) temsil eder
                 */

                var maxElement = result.OrderByDescending(x => x.Count).First();
                string maxElementValue = maxElement.Element;
                int maxElementCount = maxElement.Count;

                label7.Text = maxElementValue + " - " + maxElementCount;

            }
            catch (Exception ex)
            {
                MessageBox.Show("Bölümü En Fazla Olan Öğrenci Sayısı Gösterilirken Bir Hata Oluştu: " + ex.Message);
            }
            finally
            {
                if (conn != null)
                {
                    conn.Close();
                }
            }


        }

        private void chart1_Click(object sender, EventArgs e)
        {

        }

        private void button5_Click(object sender, EventArgs e)
        {
            DialogResult = MessageBox.Show("Programdan Çıkmak İstediğinize Emin Misiniz?", "Emin Misin?", MessageBoxButtons.OKCancel, MessageBoxIcon.Question);
            if (DialogResult == DialogResult.OK)
            {
                Environment.Exit(0);
            } 
        }

        private void button4_Click(object sender, EventArgs e)
        {
            Yardım yardim = new Yardım();
            yardim.Show();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            this.Close();
            flagForEvent = 1;
            Islemler islem = new Islemler();
            
            islem.Show();

            
        }

        private void button2_Click(object sender, EventArgs e)
        {
            this.Close();
            flagForEvent = 2;
            Islemler islem = new Islemler();
            islem.Show();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            this.Close();
            flagForEvent = 3;
            Islemler islem = new Islemler();
            islem.Show();
        }

        private void label1_Click(object sender, EventArgs e)
        {

        }
    }
}
