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
using System.Data.OleDb;

namespace OkulOtomasyonSitemi
{

    public partial class OgretimUyesi_Ekle : Form
    {

        int flagForOld = 0; // Yaş doğrulaması 

        OleDbConnection conn = new OleDbConnection("Provider=Microsoft.ACE.Oledb.12.0;Data Source = C:\\Users\\yakup\\OneDrive\\Masaüstü\\OgrenciOtomation1.accdb");

        public OgretimUyesi_Ekle()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            comboBox1.DropDownStyle = ComboBoxStyle.DropDownList;        // Sadece seçeneklerden bir text seç
            comboBox2.DropDownStyle = ComboBoxStyle.DropDownList;
            comboBox3.DropDownStyle = ComboBoxStyle.DropDownList;
        }

        private void çıkışToolStripMenuItem_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void yardımAlToolStripMenuItem_Click(object sender, EventArgs e)
        {
            MessageBox.Show("Boşlukları Doğru Şekilde Doldurarak Öğretim Üyesi Kayıt İşleminizi Gerçekleştirebilirsiniz.", "İnfo", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        public static int TCKontrol(string TCno)
        {
            if (string.IsNullOrEmpty(TCno))
            {

                return 0;
            }
            else
            {
                int flag = 0;

                int Algoritma_Adim_Kontrol = 0, TekBasamaklarToplami = 0, CiftBasamaklarToplami = 0, TumBasamaklarToplami = 0, Basamak_10 = 0, Basamak_11 = 0;

                if (TCno.Length == 11) Algoritma_Adim_Kontrol = 1;

                foreach (char chr in TCno) { if (Char.IsNumber(chr)) Algoritma_Adim_Kontrol = 2; }

                if (TCno.Substring(0, 1) != "0") Algoritma_Adim_Kontrol = 3;

                int[] arrTC = System.Text.RegularExpressions.Regex.Replace(TCno, "[^0-9]", "").Select(x => (int)Char.GetNumericValue(x)).ToArray();

                for (int i = 0; i < TCno.Length; i++)
                {
                    TumBasamaklarToplami += Convert.ToInt32(arrTC[i]);
                    if (((i + 1) % 2) == 0)
                    {
                        if (i + 1 != 10) CiftBasamaklarToplami += Convert.ToInt32(arrTC[i]);
                        else Basamak_10 = Convert.ToInt32(arrTC[i]);
                    }
                    else
                    {
                        if (i + 1 != 11) TekBasamaklarToplami += Convert.ToInt32(arrTC[i]);
                        else
                        {
                            Basamak_11 = Convert.ToInt32(arrTC[i]);
                            TumBasamaklarToplami = TumBasamaklarToplami - Basamak_11;
                        }
                    }
                }

                int ilkDeger = (TekBasamaklarToplami * 7) - CiftBasamaklarToplami;
                int ilkDeger_mod10 = ilkDeger % 10;
                if (Basamak_10 == ilkDeger_mod10) Algoritma_Adim_Kontrol = 4;

                int ikinciDeger_mod10 = TumBasamaklarToplami % 10;
                if (Basamak_11 == ikinciDeger_mod10) Algoritma_Adim_Kontrol = 5;

                if (Algoritma_Adim_Kontrol == 5)
                {
                    flag = 1;
                    return flag;    // "TC No Doğru";
                }
                else
                    return flag;    // "TC No Yanlış";
            }
        }

        private int idariNoDogrula(string idariNo)
        {
            int flag = 0;

            conn.Open();
            OleDbCommand cmd = new OleDbCommand("Select * from ogretimuyesi where idarino = '" + textBoxIdariNo.Text + "'", conn);
            OleDbDataReader read = cmd.ExecuteReader();
            if (read.Read() == true)
            {
                if (textBoxIdariNo.Text == read["idarino"].ToString())
                {
                    flag = 1;
                }
            }

            conn.Close();

            return flag;    // 1 ise Okul NO Zaten var , 0 ise Okul No yok
        }

        private void textBoxIdariNo_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!char.IsControl(e.KeyChar) && !char.IsDigit(e.KeyChar))
            {
                e.Handled = true;
            }
        }

        private void textBoxPhone_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!char.IsControl(e.KeyChar) && !char.IsDigit(e.KeyChar))
            {
                e.Handled = true;
            }
        }

        private void textBoxTC_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!char.IsControl(e.KeyChar) && !char.IsDigit(e.KeyChar))
            {
                e.Handled = true;
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            int tcKontrol = TCKontrol(textBoxTC.Text);      // Doğru tc girimi yapıldı mı?
            int idariNoKontrol = idariNoDogrula(textBoxIdariNo.Text);


            if( textBoxIdariNo.Text != "" && textBoxTC.Text != ""  && textBoxName.Text != "" &&
                textBoxSurname.Text != "" && comboBox1.Text != "" && comboBox2.Text != "" &&
                textBoxPhone.Text != "" && textBoxEposta.Text != "" && comboBox3.Text != "" && 
                dateTimePicker1.Value != DateTime.Now && (radioButton1.Checked == true || radioButton2.Checked == true || radioButton3.Checked == true))
            {
                if (idariNoKontrol != 1)
                {
                    if (tcKontrol == 1)
                    {

                        if( flagForOld != 1 )
                        {
                            conn.Open();

                            // Öğrenci kayıt için veri tabanına kaydedilen bilgiler.
                            OleDbCommand cmd = new OleDbCommand("INSERT INTO ogretimuyesi (idarino, tc,  ad, soyad, ulke, sehir, telefon, eposta, bolum, dogumtarihi, cinsiyet) VALUES (@idarino, @tc , @ad, @soyad, @ulke, @sehir, @telefon, @eposta, @bolum, @dogumtarihi, @cinsiyet)", conn);

                            cmd.Parameters.AddWithValue("@idarino", textBoxIdariNo.Text);
                            cmd.Parameters.AddWithValue("@tc", textBoxTC.Text);
                            cmd.Parameters.AddWithValue("@ad", textBoxName.Text);
                            cmd.Parameters.AddWithValue("@soyad", textBoxSurname.Text);
                            cmd.Parameters.AddWithValue("@ulke", comboBox1.Text);
                            cmd.Parameters.AddWithValue("@sehir", comboBox2.Text);
                            cmd.Parameters.AddWithValue("@telefon", textBoxPhone.Text);
                            cmd.Parameters.AddWithValue("@eposta", textBoxEposta.Text);
                            cmd.Parameters.AddWithValue("@bolum", comboBox3.Text);
                            cmd.Parameters.AddWithValue("@dogumtarihi", dateTimePicker1.Text);
                            cmd.Parameters.AddWithValue("@cinsiyet", radioButton1.Checked ? radioButton1.Text : radioButton2.Checked ? radioButton2.Text : radioButton3.Text);

                            try
                            {
                                cmd.ExecuteNonQuery();
                                conn.Close();
                                MessageBox.Show("Yeni Öğretim Üyesi Bilgileri Sisteme Eklendi!", "İşlem Başarılı.");
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
                                MessageBox.Show("BÖYLE BİR ÖĞRETİM ÜYESİ SİSTEMDE KAYITLI!!! ", "Başarısız!");
                                conn.Close();
                            }
                            finally
                            {
                                conn.Close();
                            }
                        } else
                        {
                            MessageBox.Show("Lütfen Yaşınızı Doğru Giriniz!" , "Yaşını Doğrula" , MessageBoxButtons.OK,MessageBoxIcon.Error);
                        }
                    }
                    else
                    {
                        MessageBox.Show("TC YANLIŞ GİRİLDİ!!!", "Başarısız.");
                    }
                }
                else
                {
                    MessageBox.Show("Bu İdari Numaraya Sahip Öğretim Üyesi Sİstemde Kayıtlıdır!", "Başarısız!");
                }
            } else
            {
                MessageBox.Show("Tüm Bilgileri Eksiksiz Giriniz Lütfen!", "Bilgiler Tam Değil", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            
        }

        private void textBoxPhone_TextChanged(object sender, EventArgs e)
        {
            // Her 3 karakter sonra bir '-' ekleyin.
            if (textBoxPhone.Text.Length == 3 || textBoxPhone.Text.Length == 7)
            {
                textBoxPhone.Text += "-";
                textBoxPhone.SelectionStart = textBoxPhone.Text.Length;
            }
        }


        
        private void dateTimePicker1_ValueChanged(object sender, EventArgs e)
        {
            
            DateTime now = DateTime.Today;
            DateTime dob = dateTimePicker1.Value;
            int age = now.Year - dob.Year;
            if (now < dob.AddYears(age)) age--;
            label14.Visible = true;

            if( age > 22 )
            {
                flagForOld = 0;
                label14.Text = $"Şu An ki Yaşınız: {age}";
            } else
            {
                label14.Text = "Lütfen Yaşınızı Doğru Girin";
            }

            if( age < 22  )
            {
                flagForOld = 1;
            }

        }

        private void label14_Click(object sender, EventArgs e)
        {

        }

        private void textBoxName_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!char.IsLetter(e.KeyChar) && !char.IsControl(e.KeyChar))
            {
                e.Handled = true; // Giriş işlemi engellenir
            }
        }

        private void textBoxSurname_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!char.IsLetter(e.KeyChar) && !char.IsControl(e.KeyChar))
            {
                e.Handled = true; // Giriş işlemi engellenir
            }
        }
    }
}
