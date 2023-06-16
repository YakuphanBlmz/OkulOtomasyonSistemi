using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace OkulOtomasyonSitemi
{
    public partial class Form2 : Form
    {
        public Form2()
        {
            InitializeComponent();
        }

        private void tabPage1_Click(object sender, EventArgs e)
        {

        }

        private void Form2_Load(object sender, EventArgs e)
        {
            bunifuTextBoxGununTarihi.Text = DateTime.Now.ToString("dd/MM/yyyy");        // Günün Tarihi

            bunifuTextBoxKDV.Text = "%8";
            double flagYalinTutar = 0;
            if (double.TryParse(bunifuTextBoxYalinTutar.Text, out double yalinTutar))
            {
                // Double dönüştürme başarılı ise değişkene atar.
                flagYalinTutar = yalinTutar * 8 / 100;
            }
            double toplamTutar = yalinTutar + flagYalinTutar;
            bunifuTextBoxKDVveTutar.Text = string.Format("{0:N2} ({1} KDV)", toplamTutar, bunifuTextBoxKDV.Text);

        }

        private void tabPage2_Click(object sender, EventArgs e)
        {

        }

        private void buttonFiyatHesapla_Click(object sender, EventArgs e)
        {
            // textBoxMalzemeKodu, textBoxMalzemeAdi ve textBoxMalzemeFiyatı'ndan verileri al
            string malzemeKodu = textBoxMalzemeKodu.Text;
            string malzemeAdi = textBoxMalzemeAdi.Text;
            string malzemeFiyati = textBoxMalzemeFiyati.Text;

            // Verileri listView1'e ekle
            ListViewItem item = new ListViewItem(malzemeKodu);
            item.SubItems.Add(malzemeAdi);
            item.SubItems.Add(malzemeFiyati);
            listView1.Items.Add(item);

            textBoxMalzemeKodu.Text = "";
            textBoxMalzemeAdi.Text = "";
            textBoxMalzemeFiyati.Text = "";
        }

        private void buttonSeciliUrunGuncelle_Click(object sender, EventArgs e)
        {

            // tabPage2'deki listView2 öğesine kaynak listView1 öğesindeki verileri taşıma
            foreach (ListViewItem item in listView1.Items)
            {
                ListViewItem newItem = new ListViewItem(item.SubItems[0].Text);
                newItem.SubItems.Add(item.SubItems[1].Text);
                newItem.SubItems.Add(item.SubItems[2].Text);
                listView2.Items.Add(newItem);
            }

            bunifuPagesFiziksel.SetPage("FizikselKaynakYonetimi2");

            double toplamFiyat = 0; // Toplam fiyatı tutmak için değişken tanımlıyoruz

            foreach (ListViewItem item in listView1.SelectedItems)
            {
                // Seçili satırların Malzeme Fiyatı sütunundaki değerleri toplayarak toplamFiyat değişkenine ekliyoruz
                double fiyat;
                if (double.TryParse(item.SubItems[2].Text, out fiyat))
                {
                    toplamFiyat += fiyat;
                }
            }

            // toplamFiyat değerini bunifuTextBoxYalinTutar üzerinde gösteriyoruz
            bunifuTextBoxYalinTutar.Text = toplamFiyat.ToString();
        }

        private void buttonUrunIptal_Click(object sender, EventArgs e)
        {
            if (listView1.SelectedItems.Count > 0)
            {
                ListViewItem selected = listView1.SelectedItems[0];
                listView1.Items.Remove(selected);
            }
        }

        private void textBoxMalzemeKodu_TextChanged(object sender, EventArgs e)
        {

        }

        private void textBoxMalzemeKodu_KeyPress(object sender, KeyPressEventArgs e)
        {
            if(!char.IsControl(e.KeyChar) && !char.IsDigit(e.KeyChar))
            {
                e.Handled = true;
            }
        }

        private void textBoxMalzemeFiyati_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!char.IsControl(e.KeyChar) && !char.IsDigit(e.KeyChar))
            {
                e.Handled = true;
            }
        }
    }
}
