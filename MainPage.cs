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
using System.Reflection.Emit;
using static System.Windows.Forms.VisualStyles.VisualStyleElement.Button;
using static System.Windows.Forms.VisualStyles.VisualStyleElement;
using Sipaa.Framework;
using System.Drawing.Imaging;
using System.IO;
using Bunifu.UI.WinForms;

namespace OkulOtomasyonSitemi
{
    public partial class MainPage : Form
    {
        byte[] imageBytes;  // Profil resmi için
        int flagForUpdate = 0;
        OleDbConnection conn = new OleDbConnection("Provider=Microsoft.ACE.Oledb.12.0;Data Source = C:\\Users\\yakup\\OneDrive\\Masaüstü\\OgrenciOtomation1.accdb");

        public MainPage()
        {
            InitializeComponent();
            kullanici();
            ogrenciSayisi();
            ogretimUyesiSayisi();
            idariSayisi();
            enCokBolum();
        }

        private void MainPage_Load(object sender, EventArgs e)
        {
            profilResmi();
        }

        void profilResmi()
        {
            string mail = Giriş.flagMail;
            try
            {
                conn.Open();

                OleDbCommand cmd = new OleDbCommand("SELECT resim FROM kullanicilar WHERE eposta=?", conn);
                cmd.Parameters.AddWithValue("@eposta", mail);

                OleDbDataReader reader = cmd.ExecuteReader();
                if (reader.Read())
                {
                    if (reader["resim"] != DBNull.Value)                    // Aşağıdaki hatayı önlemek için açtım.
                    {
                        byte[] imageBytes = (byte[])reader["resim"];        // HATA ALINIYOR : System.InvalidCastException: ''System.DBNull' türündeki nesne 'System.Byte[]' türüne atılamadı.'
                        // MemoryStream nesnesinden bir Image nesnesi oluşturmak için Image.FromStream yerine Image.FromStream(ms, true, true) yöntemi kullanılabilir.
                        using (MemoryStream ms = new MemoryStream(imageBytes))
                        {
                            bunifuPictureBox1.Image = Image.FromStream(ms, true, true);
                            bunifuPictureBox2.Image = Image.FromStream(ms, true, true);
                            bunifuPictureBox3.Image = Image.FromStream(ms, true, true);
                        }
                    }
                    
                }
                reader.Close();
                conn.Close();
            }
            finally
            {
                if (conn.State == ConnectionState.Open)
                {
                    conn.Close();
                }
            }
        }

        void kullanici()     // Kullanici Adini Belirler.
        {
            if (1 == 1)
            {
                string username = Giriş.userName;
                label28.Text = username;
            }
        }

        void ogrenciSayisi()     // Güncel Öğrenci Sayısı
        {

            OleDbConnection conn = new OleDbConnection("Provider=Microsoft.ACE.Oledb.12.0;Data Source = C:\\Users\\yakup\\OneDrive\\Masaüstü\\OgrenciOtomation1.accdb");

            try
            {
                conn.Open();

                OleDbCommand cmd = new OleDbCommand("SELECT COUNT(*) FROM ogrenciler ", conn);
                int rowCount = (int)cmd.ExecuteScalar();

                label4.Text = rowCount.ToString();

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

        void idariSayisi()
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

        void ogretimUyesiSayisi()
        {
            OleDbConnection conn = new OleDbConnection("Provider=Microsoft.ACE.Oledb.12.0;Data Source = C:\\Users\\yakup\\OneDrive\\Masaüstü\\OgrenciOtomation1.accdb");

            try
            {
                conn.Open();

                OleDbCommand cmd = new OleDbCommand("SELECT COUNT(*) FROM ogretimuyesi ", conn);
                int rowCount = (int)cmd.ExecuteScalar();

                label11.Text = rowCount.ToString();

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

        void enCokBolum()
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

                label5.Text = maxElementValue + " - " + maxElementCount;

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

        void MovieIndıcator(Control control)
        {
            indicator.Top = control.Top;
            indicator.Height = control.Height;
        }

       

        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void bunifuButton1_Click(object sender, EventArgs e)
        {
            ogrenciSayisi();
            ogretimUyesiSayisi();
            idariSayisi();
            enCokBolum();

            MovieIndıcator((Control)sender);
            bunifuPages1.SetPage("Anasayfa");
        }

        private void bunifuButton2_Click(object sender, EventArgs e)
        {
            MovieIndıcator((Control)sender);
            bunifuPages1.SetPage("Öğrenci İşlemleri");
        }

        private void bunifuButton3_Click(object sender, EventArgs e)
        {
            MovieIndıcator((Control)sender);
            bunifuPages1.SetPage("Öğretim Üyesi İşlemleri");
        }

        private void bunifuButton4_Click(object sender, EventArgs e)
        {
            MovieIndıcator((Control)sender);
            bunifuPages1.SetPage("İdari İşlemler");
        }

        private void bunifuButton5_Click(object sender, EventArgs e)
        {
            MovieIndıcator((Control)sender);
        }

        private void bunifuButton7_Click(object sender, EventArgs e)
        {
            MovieIndıcator((Control)sender);
            Yardım yardim = new Yardım();
            yardim.Show();
        }

        private void pictureBox1_Click(object sender, EventArgs e)
        {

        }

        private void label7_Click(object sender, EventArgs e)
        {

        }

        private void label8_Click(object sender, EventArgs e)
        {

        }

        private void pictureBox3_Click(object sender, EventArgs e)
        {

        }

        private void tabPage1_Click(object sender, EventArgs e)
        {

        }

        private void bunifuButton5_Click_1(object sender, EventArgs e)
        {
            MovieIndıcator((Control)sender);
            DialogResult = MessageBox.Show("Giriş Sayfasına Dönmek İstediğinize Emin Misiniz?", "Emin Misin?", MessageBoxButtons.OKCancel, MessageBoxIcon.Question);
            if (DialogResult == DialogResult.OK)
            {
                this.Close();
                Giriş grs = new Giriş();
                grs.Show();
            }
        }

        private void pictureBox4_Click(object sender, EventArgs e)
        {

        }

        private void bunifuButton18_Click(object sender, EventArgs e)
        {
            OgrenciEkle ogr_ekle = new OgrenciEkle();
            ogr_ekle.Show();
        }

        private void bunifuButton17_Click(object sender, EventArgs e)
        {
            OgrenciSil ogr_sil = new OgrenciSil();
            ogr_sil.Show();
        }

        private void bunifuButton16_Click(object sender, EventArgs e)
        {
            OgrenciAra_Isim ogr_ara_isim = new OgrenciAra_Isim();
            ogr_ara_isim.Show();
        }

        private void bunifuButton15_Click(object sender, EventArgs e)
        {
            OgrenciAra_Bolum ogr_ara_bolum = new OgrenciAra_Bolum();
            ogr_ara_bolum.Show();
        }

        private void bunifuButton14_Click(object sender, EventArgs e)
        {
            OgrenciGuncelle ogr_guncelle = new OgrenciGuncelle();
            ogr_guncelle.Show();
        }

        private void bunifuButton9_Click(object sender, EventArgs e)
        {
            OgretimUyesi_Ekle ogr_uye_ekle = new OgretimUyesi_Ekle();
            ogr_uye_ekle.Show();
        }

        private void bunifuButton10_Click(object sender, EventArgs e)
        {
            OgretimUyesi_Sil ogr_uye_sil = new OgretimUyesi_Sil();
            ogr_uye_sil.Show();
        }

        private void bunifuButton11_Click(object sender, EventArgs e)
        {
            OgretimUyesi_Ara_Isim ogr_uye_ara_isim = new OgretimUyesi_Ara_Isim();
            ogr_uye_ara_isim.Show();
        }

        private void bunifuButton12_Click(object sender, EventArgs e)
        {
            OgretimUyesi_Ara_Bolum ogr_uye_ara_bolum = new OgretimUyesi_Ara_Bolum();
            ogr_uye_ara_bolum.Show();
        }

        private void bunifuButton13_Click(object sender, EventArgs e)
        {
            OgretimUyesi_Guncelle ogr_uye_guncelle = new OgretimUyesi_Guncelle();
            ogr_uye_guncelle.Show();
        }

        private void bunifuButton19_Click(object sender, EventArgs e)
        {
            if (flagForUpdate != 0)
            {
                DialogResult result = MessageBox.Show("Bilgilerinizi Güncellemek İstediğinize Emin Misiniz?", "Emin Misin?", MessageBoxButtons.OKCancel, MessageBoxIcon.Question);

                if (result == DialogResult.OK)
                {
                    // Resmi byte dizisine dönüştürür
                    byte[] imageBytes = null;
                    using (MemoryStream ms = new MemoryStream())
                    {
                        bunifuPictureBox3.Image.Save(ms, bunifuPictureBox2.Image.RawFormat);
                        imageBytes = ms.ToArray();
                    }

                    conn.Open();

                    try
                    {
                        // Kişiyi sileriz.
                        OleDbCommand cmd = new OleDbCommand("DELETE FROM kullanicilar WHERE eposta=?", conn);
                        cmd.Parameters.AddWithValue("@eposta", bunifuTextBoxMail.Text);
                        int deleteResult = cmd.ExecuteNonQuery();

                        if (deleteResult > 0)
                        {
                            // Kişi güncelleme için veri tabanına kaydedilen bilgiler.
                            string sql = "INSERT INTO kullanicilar (tc, ad, soyad, ulke, sehir, telefon, eposta, dogumtarihi, cinsiyet, resim , guvenlik_sorusu , guvenlik_sorusu_cevabi , sifre) VALUES (@tc, @ad, @soyad, @ulke, @sehir, @telefon, @eposta, @dogumtarihi, @cinsiyet, @resim , @guvenlik_sorusu , @guvenlik_sorusu_cevabi , @sifre)";

                            // OleDbCommand nesnesi oluşturun ve parametreleri ekleyin
                            OleDbCommand cmd2 = new OleDbCommand(sql, conn);
                            cmd2.Parameters.AddWithValue("@tc", bunifuTextBoxTC.Text);
                            cmd2.Parameters.AddWithValue("@ad", bunifuTextBoxName.Text);
                            cmd2.Parameters.AddWithValue("@soyad", bunifuTextBoxSurname.Text);
                            cmd2.Parameters.AddWithValue("@ulke", bunifuTextBoxUlke.Text);
                            cmd2.Parameters.AddWithValue("@sehir", bunifuTextBoxSehir.Text);
                            cmd2.Parameters.AddWithValue("@telefon", bunifuTextBoxPhone.Text);
                            cmd2.Parameters.AddWithValue("@eposta", bunifuTextBoxMail.Text);
                            cmd2.Parameters.AddWithValue("@dogumtarihi", bunifuTextBoxDogum.Text);
                            cmd2.Parameters.AddWithValue("@cinsiyet", bunifuTextBoxCinsiyet.Text);
                            cmd2.Parameters.AddWithValue("@resim", imageBytes);
                            cmd2.Parameters.AddWithValue("@guvenlik_sorusu", comboBoxGuvenlik.Text);
                            cmd2.Parameters.AddWithValue("@guvenlik_sorusu_cevabi", bunifuTextBoxGuvenlikCevabi.Text);
                            cmd2.Parameters.AddWithValue("@sifre", bunifuTextBoxSifrem.Text);

                            int insertResult = cmd2.ExecuteNonQuery();
                            if (insertResult > 0)
                            {
                                DialogResult temp =  MessageBox.Show("Bilgilerinizin Güncel Hali Sisteme Eklendi!", "İşlem Başarılı." , MessageBoxButtons.OK);
                                if(temp == DialogResult.OK)
                                {
                                    bunifuPictureBox1.Image = bunifuPictureBox3.Image;
                                    bunifuPictureBox2.Image = bunifuPictureBox3.Image;
                                }
                            }
                            else
                            {
                                MessageBox.Show("Bilgilerinizi Güncelleme İşlemi Başarısız Oldu.", "Başarısız", MessageBoxButtons.OK, MessageBoxIcon.Error);
                            }
                        }
                        else
                        {
                            MessageBox.Show("Bilgilerinizi Güncelleme İşlemi Başarısız Oldu.", "Başarısız", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        }
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show("Bir Hata Oluştu : " + ex.Message, "Başarısız!");
                    }
                    finally
                    {
                        conn.Close();
                    }
                }
            }
            else
            {
                MessageBox.Show("Bilgilerinizi Güncellemek İçin Değişiklik Yapmak Zorundasınız!", "Güncelleme Sıkıntısı", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }

        private void bunifuTextBoxPhone_TextChange(object sender, EventArgs e)
        {
            string eskiDeger = bunifuTextBoxPhone.Tag as string;
            string yeniDeger = bunifuTextBoxPhone.Text;

            if (eskiDeger != yeniDeger)
            {
                flagForUpdate = 1;
            }

            bunifuTextBoxPhone.Tag = yeniDeger;
        }


        private void bunifuTextBoxMail_TextChanged(object sender, EventArgs e)
        {
            string eskiDeger = bunifuTextBoxMail.Tag as string;
            string yeniDeger = bunifuTextBoxMail.Text;

            if (eskiDeger != yeniDeger)
            {
                flagForUpdate = 2;
            }

            bunifuTextBoxMail.Tag = yeniDeger;
        }

        private void bunifuButton8_Click(object sender, EventArgs e)
        {

            string mail = Giriş.flagMail;
            bunifuPages1.SetPage("MyProfile");

            using (OleDbConnection conn = new OleDbConnection("Provider=Microsoft.ACE.Oledb.12.0;Data Source=C:\\Users\\yakup\\OneDrive\\Masaüstü\\OgrenciOtomation1.accdb"))
            {
                conn.Open();

                if (conn.State == ConnectionState.Open)
                {
                    using (OleDbCommand cmd = new OleDbCommand("SELECT * FROM kullanicilar WHERE eposta=@mail", conn))
                    {
                        cmd.Parameters.AddWithValue("@mail", mail);
                        using (OleDbDataReader read = cmd.ExecuteReader())
                        {
                            if (read.Read())
                            {
                                labelName.Text = read["ad"].ToString();
                                labelSurname.Text = read["soyad"].ToString();
                                labelTC.Text = read["tc"].ToString();
                                labelUlke.Text = read["ulke"].ToString();
                                labelSehir.Text = read["sehir"].ToString();
                                labelTelefon.Text = read["telefon"].ToString();
                                labelEposta.Text = read["eposta"].ToString();
                                labelDogum.Text = read["dogumtarihi"].ToString();
                                labelCinsiyet.Text = read["cinsiyet"].ToString();
                            }
                        }
                    }
                }
                else
                {
                    // Connection could not be opened
                }

                conn.Close();
            }


        }

        private void showMyInformation()
        {
            string mail = Giriş.flagMail;
            bunifuPages1.SetPage("MyProfileUpdate");

            OleDbConnection conn = new OleDbConnection("Provider=Microsoft.ACE.Oledb.12.0;Data Source = C:\\Users\\yakup\\OneDrive\\Masaüstü\\OgrenciOtomation1.accdb");
            conn.Open();

            OleDbCommand cmd = new OleDbCommand("Select * from kullanicilar where eposta='" + mail + "'", conn);
            OleDbDataReader read = cmd.ExecuteReader();
            if (read.Read() == true)
            {
                if (mail == read["eposta"].ToString())
                {
                    bunifuTextBoxName.Text = read["ad"].ToString();
                    bunifuTextBoxSurname.Text = read["soyad"].ToString();
                    bunifuTextBoxTC.Text = read["tc"].ToString();
                    bunifuTextBoxUlke.Text = read["ulke"].ToString();
                    bunifuTextBoxSehir.Text = read["sehir"].ToString();
                    bunifuTextBoxPhone.Text = read["telefon"].ToString();
                    bunifuTextBoxMail.Text = read["eposta"].ToString();
                    bunifuTextBoxDogum.Text = read["dogumtarihi"].ToString();
                    bunifuTextBoxCinsiyet.Text = read["cinsiyet"].ToString();
                    comboBoxGuvenlik.Text = read["guvenlik_sorusu"].ToString();
                    bunifuTextBoxGuvenlikCevabi.Text = read["guvenlik_sorusu_cevabi"].ToString();
                    bunifuTextBoxSifrem.Text = read["sifre"].ToString();

                }
            }
            conn.Close();
        }

        private void bunifuButton6_Click(object sender, EventArgs e)
        {
            showMyInformation();        
        }

        private void bunifuPictureBox3_Click(object sender, EventArgs e)
        {

        }

        // Resimlerin yönetimi işlemi olmadığından veritabanında resimler için ayrı bir tablo açmayacağım. 
        private void pictureBox8_Click(object sender, EventArgs e)
        {
            OpenFileDialog ofd = new OpenFileDialog();
            ofd.Title = "Resim Seçin";
            ofd.Filter = "Resim Dosyaları (*.bmp;*.jpg;*.jpeg;*.gif;*.png)|*.bmp;*.jpg;*.jpeg;*.gif;*.png";

            if (ofd.ShowDialog() == DialogResult.OK)
            {
                bunifuPictureBox3.Image = new Bitmap(ofd.FileName);
            }
        }



        private void bunifuTextBox1_TextChange(object sender, EventArgs e)
        {
            
        }

        private void bunifuButton24_Click(object sender, EventArgs e)
        {
            IdariEkibiGoruntule idari_goruntule = new IdariEkibiGoruntule();
            idari_goruntule.Show();
        }

        private void label28_Click(object sender, EventArgs e)
        {

        }

        private void bunifuPictureBox3_LoadCompleted(object sender, AsyncCompletedEventArgs e)
        {
            flagForUpdate = 1;
        }

        private void bunifuButton21_Click(object sender, EventArgs e)
        {
            BolumEkle bolumEkle = new BolumEkle();
            bolumEkle.Show();
        }

        private void bunifuButton20_Click(object sender, EventArgs e)
        {
            SifremiUnuttum sfrUnuttum = new SifremiUnuttum();
            sfrUnuttum.Show();
        }
    }
}
