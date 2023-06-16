using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics.Eventing.Reader;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace OkulOtomasyonSitemi
{
    public partial class Islemler : Form
    {

        int flagToTransaction = 0;
        int Event = Form1.flagForEvent;

        public static int flagForEvent = 0;   // Butona göre işlem sayfasına götürür.

        public Islemler()
        {
            InitializeComponent();
            buttonChecked();
            kullanici();
        }

        private void Islemler_Load(object sender, EventArgs e)
        {
            
        }

        public void buttonChecked()
        {

            int flagToEvent = Form1.flagForEvent;
            if (Event == 1)
            {
                label1.Text = "Öğrenci İşlemleri";
                button6.Text = "Öğrenci Bilgileri Ekle";
                button7.Text = "Öğrenci Bilgileri Çıkar";
                button8.Text = "Öğrenci Bilgilerini Ara - İsim";
                button10.Text = "Öğrenci Bilgilerini Ara - Bölüm";
                button9.Text = "Öğrenci Bilgilerini Güncelle";


            } else if( Event == 2) 
            {

                label1.Text = "Öğretim Üyesi İşlemleri";
                button6.Text = "Öğretim Üyesi Bilgileri Ekle";
                button7.Text = "Öğretim Üyesi Bilgileri Çıkar";
                button8.Text = "Öğretim Üyesi Bilgilerini Ara - İsim";
                button10.Text = "Öğretim Üyesi Bilgilerini Ara - Bölüm";
                button9.Text = "Öğretim Üyesi Bilgilerini Güncelle";
  

            } else if( Event == 3)
            {

                label1.Text = "İdari İşlemleri";
                button6.Text = "İdari Bilgileri Ekle";
                button7.Text = "İdari Bilgileri Çıkar";
                button8.Text = "İdari Bilgilerini Ara - İsim";
                button10.Text = "İdari Bilgilerini Ara - Bölüm";
                button9.Text = "İdari Bilgilerini Güncelle";
            } else
            {

            }

            
        }

        

        private void button4_Click(object sender, EventArgs e)
        {
            Yardım yardim = new Yardım();
            yardim.Show();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            Event = 1;
            Islemler islem = new Islemler();

            islem.Show();


        }

        private void button2_Click(object sender, EventArgs e)
        {
            Event = 2;
            Islemler islem = new Islemler();
            islem.Show();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            Event = 3;
            Islemler islem = new Islemler();
            islem.Show();
        }

        public void kullanici()     // Kullanici Adini Belirler.
        {
            if (1 == 1)
            {
                string username = Giriş.userName;
                label4.Text = username;
            }
        }

        private void button6_Click(object sender, EventArgs e)
        {
            if( Event == 1 )
            {
                OgrenciEkle ogr_ekle = new OgrenciEkle();
                ogr_ekle.Show();
            } else if(  Event == 2 ) {
                OgretimUyesi_Ekle ogr_ekle = new OgretimUyesi_Ekle();
                ogr_ekle.Show();
            }

        }

        private void button1_Click_1(object sender, EventArgs e)
        {
            Event = 1;
            buttonChecked();
        }

        private void button2_Click_1(object sender, EventArgs e)
        {
            Event = 2;
            buttonChecked();
        }

        private void button3_Click_1(object sender, EventArgs e)
        {
            Event = 3;
            buttonChecked();
        }

        private void button7_Click(object sender, EventArgs e)
        {
            if( Event == 1)
            {
                OgrenciSil ogrenciSil = new OgrenciSil();
                ogrenciSil.Show();
            } else if( Event == 2 )
            {
                OgretimUyesi_Sil ogr_Sil = new OgretimUyesi_Sil();
                ogr_Sil.Show();
            }
        }

        private void button8_Click(object sender, EventArgs e)
        {
            if( Event == 1 )
            {
                OgrenciAra_Isim ogr_ara_isim = new OgrenciAra_Isim();
                ogr_ara_isim.Show();
            } else if( Event == 2)
            {
                OgretimUyesi_Ara_Isim ogr_ara_isim = new OgretimUyesi_Ara_Isim();
                ogr_ara_isim.Show();
            }
        }

        private void button10_Click(object sender, EventArgs e)
        {
            if( Event == 1)
            {
                OgrenciAra_Bolum ogr_ara_bolum = new OgrenciAra_Bolum();
                ogr_ara_bolum.Show();
            } else if( Event == 2)
            {
                OgretimUyesi_Ara_Bolum ogr_ara_bolum = new OgretimUyesi_Ara_Bolum();
                ogr_ara_bolum.Show();
            }
        }

        private void button9_Click(object sender, EventArgs e)
        {
            if( Event == 1  )
            {
                OgrenciGuncelle ogrenciGuncelle = new OgrenciGuncelle();
                ogrenciGuncelle.Show();
            } else if( Event == 2 )
            {
                OgretimUyesi_Guncelle ogr_Guncelle = new OgretimUyesi_Guncelle();
                ogr_Guncelle.Show();
            } 
        }

        private void button4_Click_1(object sender, EventArgs e)
        {
            Yardım yardim = new Yardım();
            yardim.Show();
        }

        private void button5_Click(object sender, EventArgs e)
        {
            DialogResult = MessageBox.Show("Programdan Çıkmak İstediğinize Emin Misiniz?", "Emin Misin?", MessageBoxButtons.OKCancel, MessageBoxIcon.Question);
            if( DialogResult == DialogResult.OK )
            {
                Environment.Exit(0);
            }
            
        }

        private void pictureBox13_Click(object sender, EventArgs e)
        {
            this.Hide();
            Form1 anasayfa = new Form1();
            anasayfa.Show();
        }
    }
}
