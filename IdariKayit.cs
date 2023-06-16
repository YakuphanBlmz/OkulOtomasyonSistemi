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
using static System.Windows.Forms.VisualStyles.VisualStyleElement;
using System.Security.Policy;

namespace OkulOtomasyonSitemi
{
    public partial class IdariKayit : Form
    {
        int flagForOld = 0;
        OleDbConnection conn = new OleDbConnection("Provider=Microsoft.ACE.Oledb.12.0;Data Source = C:\\Users\\yakup\\OneDrive\\Masaüstü\\OgrenciOtomation1.accdb");

        public IdariKayit()
        {
            InitializeComponent();
            
        }

        private void button1_Click(object sender, EventArgs e)
        {
            int tcKontrol = TCKontrol(textBoxTC.Text);      // Doğru tc girimi yapıldı mı?
            int EpostaDogrula = EpostaKontrol(textBoxEposta.Text);  // Daha önceden veri tabanına kayıtlı eposta var mı?
            int tcDogrulama = TCDogrula(textBoxTC.Text);    // Daha önceden veri tabanına kayıtlı tc var mı?


            if( textBoxName.Text != "" && textBoxSurname.Text != "" && comboBoxGuvenlik.Text != "" && textBoxGuvenlik.Text != "" &&
                textBoxTC.Text != ""   && textBoxSifre.Text != "" &&  
                textBoxSifre2.Text != "" && textBoxEposta.Text != "" && comboBox1.Text != "" && comboBox2.Text != "" &&
                 dateTimePicker1.Value != DateTime.Now && textBoxPhone.Text != "" &&
                 (radioButton1.Checked == true || radioButton2.Checked == true || radioButton3.Checked == true))
            {
                if (flagForOld != 1)
                {
                    if (EpostaDogrula == 0)
                    {
                        if (tcKontrol == 1)
                        {
                            if (tcDogrulama == 0)
                            {
                                if (textBoxSifre.Text == textBoxSifre2.Text)
                                {
                                    conn.Open();

                                    // İdari kayıt için veri tabanına kaydedilen bilgiler.
                                    OleDbCommand cmd = new OleDbCommand("INSERT INTO kullanicilar (tc, ad, soyad, ulke, sehir, telefon, eposta, sifre, yenidensifre, dogumtarihi, cinsiyet, guvenlik_sorusu , guvenlik_sorusu_cevabi) VALUES (@tc, @ad, @soyad, @ulke, @sehir, @telefon, @eposta, @sifre, @yenidensifre, @dogumtarihi, @cinsiyet , @guvenlik_sorusu , @guvenlik_sorusu_cevabi)", conn);

                                    cmd.Parameters.AddWithValue("@tc", textBoxTC.Text);
                                    cmd.Parameters.AddWithValue("@ad", textBoxName.Text);
                                    cmd.Parameters.AddWithValue("@soyad", textBoxSurname.Text);
                                    cmd.Parameters.AddWithValue("@ulke", comboBox1.Text);
                                    cmd.Parameters.AddWithValue("@sehir", comboBox2.Text);
                                    cmd.Parameters.AddWithValue("@telefon", textBoxPhone.Text);
                                    cmd.Parameters.AddWithValue("@eposta", textBoxEposta.Text);
                                    cmd.Parameters.AddWithValue("@sifre", textBoxSifre.Text);
                                    cmd.Parameters.AddWithValue("@yenidensifre", textBoxSifre2.Text);
                                    cmd.Parameters.AddWithValue("@dogumtarihi", dateTimePicker1.Text);
                                    cmd.Parameters.AddWithValue("@cinsiyet", radioButton1.Checked ? radioButton1.Text : radioButton2.Checked ? radioButton2.Text : radioButton3.Text);
                                    cmd.Parameters.AddWithValue("@guvenlik_sorusu", comboBoxGuvenlik.Text);
                                    cmd.Parameters.AddWithValue("@guvenlik_sorusu_cevabi", textBoxGuvenlik.Text);


                                    // Kullanıcı Girişi için veri tabanına kaydedilen bilgiler.
                                    OleDbCommand cmd2 = new OleDbCommand("INSERT INTO kullanici_girisi (emailadres_kullaniciadi ,ad , sifre) VALUES (@eposta ,  @ad,  @sifre)", conn);
                                    cmd2.Parameters.AddWithValue("@emailadres_kullaniciadi", textBoxEposta.Text);
                                    cmd2.Parameters.AddWithValue("@ad", textBoxName.Text);
                                    cmd2.Parameters.AddWithValue("@sifre", textBoxSifre.Text);

                                    try
                                    {
                                        cmd.ExecuteNonQuery();
                                        cmd2.ExecuteNonQuery();
                                        conn.Close();
                                        MessageBox.Show("Yeni İdari Kullanıcı Sisteme Eklendi!", "Hayırlı Olsun.");
                                        textBoxName.Clear();
                                        textBoxSurname.Clear();
                                        textBoxTC.Clear();
                                        comboBox1.Items.Clear();
                                        comboBox2.Items.Clear();
                                        comboBoxGuvenlik.Items.Clear();
                                        textBoxGuvenlik.Clear();
                                        textBoxPhone.Clear();
                                        textBoxEposta.Clear();
                                        textBoxSifre.Clear();
                                        textBoxSifre2.Clear();
                                        dateTimePicker1.Value = DateTime.Now;
                                        radioButton1.Checked = false;
                                        radioButton2.Checked = false;
                                        radioButton3.Checked = false;

                                    }
                                    catch (Exception ex)
                                    {
                                        MessageBox.Show("Hata Alınıyor. Hata Sebebi :  " + ex.Message, "Başarısız!");
                                        conn.Close();
                                    }
                                }
                                else
                                {
                                    MessageBox.Show("GİRİLEN ŞİFRELER AYNI DEĞİL!!!", "Başarısız.", MessageBoxButtons.OK, MessageBoxIcon.Error);
                                }
                            }
                            else
                            {
                                MessageBox.Show("BU TC NUMARASINA SAHİP KİŞİ ZATEN KAYITLI!!!", "Başarısız.", MessageBoxButtons.OK, MessageBoxIcon.Error);
                            }
                        }
                        else
                        {
                            MessageBox.Show("TC YANLIŞ GİRİLDİ!!!", "Başarısız.", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        }
                    }
                    else
                    {
                        MessageBox.Show("Bu E-Posta Adresine Sahip Kullanıcı Sistemde Zaten Kayıtllı!", "Kullanıcı Zaten Var!", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }
                else
                {
                    MessageBox.Show("Lütfen Yaşınızı Doğru Giriniz!", "Yaşını Doğrula", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            } else
            {
                MessageBox.Show("Lütfen Tüm Bilgileri Eksiksiz Giriniz!", "Tam Doldurunuz.", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            
            
            
        }
        private void textBox4_TextChanged(object sender, EventArgs e)
        {
            // Her 3 karakter sonra bir '-' ekleyin.
            if (textBoxPhone.Text.Length == 3 || textBoxPhone.Text.Length == 7)
            {
                textBoxPhone.Text += "-";
                textBoxPhone.SelectionStart = textBoxPhone.Text.Length;
            }
        }

        private void textBoxTC_TextChanged(object sender, EventArgs e)
        {

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

        private void IdariKayit_Load(object sender, EventArgs e)
        {

            comboBox1.DropDownStyle = ComboBoxStyle.DropDownList;       // Sadece içindeki var olan textlerden seçimi sağlar.
            comboBox2.DropDownStyle = ComboBoxStyle.DropDownList;

            
        }

        private int EpostaKontrol(string eposta)
        {
            int flag = 0;

            conn.Open();
            OleDbCommand cmd = new OleDbCommand("Select * from kullanicilar where eposta = '" + textBoxEposta.Text + "'", conn);
            OleDbDataReader read = cmd.ExecuteReader();
            if (read.Read() == true)
            {
                if (textBoxEposta.Text == read["eposta"].ToString())
                {
                    flag = 1;
                }
            }

            conn.Close();

            return flag;    // 1 ise Eposta Zaten var , 0 ise Epsota yok
        }

        private int TCDogrula(string tcNo)
        {
            int flag = 0;

            conn.Open();
            OleDbCommand cmd = new OleDbCommand("Select * from kullanicilar where tc = '" + textBoxTC.Text+"'" , conn);
            OleDbDataReader read = cmd.ExecuteReader();
            if(read.Read() == true) 
            {
                if( textBoxTC.Text == read["tc"].ToString() )
                {
                    flag = 1;   
                }
            }

            conn.Close();

            return flag;    // 1 ise TC Zaten var , 0 ise TC yok
        }

        private void çıkışToolStripMenuItem_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void yardımToolStripMenuItem_Click(object sender, EventArgs e)
        {

        }

        private void yardımAlToolStripMenuItem_Click(object sender, EventArgs e)
        {
            MessageBox.Show("Boşlukları Doğru Şekilde Doldurarak İdari Kayıt İşleminizi Gerçekleştirebilirsiniz.","İnfo",MessageBoxButtons.OK,MessageBoxIcon.Information);
        }

        private void dateTimePicker1_ValueChanged(object sender, EventArgs e)
        {
            if(dateTimePicker1.Value != DateTime.Today)
            {
                DateTime now = DateTime.Today;
                DateTime dob = dateTimePicker1.Value;
                int age = now.Year - dob.Year;
                if (now < dob.AddYears(age)) age--;
                label14.Visible = true;

                if (age > 18 && age < 70 ) 
                {
                    label14.Text = $"Şu An ki Yaşınız: {age}";
                    flagForOld = 0;
                }
                else
                {
                    label14.Text = "Lütfen Yaşınızı Doğru Girin";
                }

                if (age < 18)
                {
                    flagForOld = 1;
                }
            } else
            {
                flagForOld = 1;
            }
            
        }

        private void textBoxTC_KeyPress(object sender, KeyPressEventArgs e)
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

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {

        }
    }
}
