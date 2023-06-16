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
    public partial class IdariEkibiGoruntule : Form
    {

        OleDbConnection conn = new OleDbConnection("Provider=Microsoft.ACE.Oledb.12.0;Data Source = C:\\Users\\yakup\\OneDrive\\Masaüstü\\OgrenciOtomation1.accdb");
        OleDbCommand cmd = new OleDbCommand();

        public IdariEkibiGoruntule()
        {
            InitializeComponent();
        }

        private void IdariEkibiGoruntule_Load(object sender, EventArgs e)
        {
            
            listView1.Items.Clear();
            conn.Open();

            OleDbCommand cmd = new OleDbCommand();
            cmd.Connection = conn;
            cmd.CommandText = ("Select *from kullanicilar");
            OleDbDataReader read = cmd.ExecuteReader();
            while(read.Read())
            {
                ListViewItem add = new ListViewItem();
                add.Text = read["ad"].ToString();
                add.SubItems.Add(read["soyad"].ToString());
                add.SubItems.Add(read["tc"].ToString());
                add.SubItems.Add(read["telefon"].ToString());
                add.SubItems.Add(read["eposta"].ToString());
                add.SubItems.Add(read["cinsiyet"].ToString());
                listView1.Items.Add(add);
            }
            conn.Close();

        }

        private void listView1_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
