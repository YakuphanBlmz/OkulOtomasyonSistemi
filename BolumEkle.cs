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
using System.Linq.Expressions;

namespace OkulOtomasyonSitemi
{
    public partial class BolumEkle : Form
    {

        OleDbConnection conn = new OleDbConnection("Provider=Microsoft.ACE.Oledb.12.0;Data Source = C:\\Users\\yakup\\OneDrive\\Masaüstü\\OgrenciOtomation1.accdb");
        public BolumEkle()
        {
            InitializeComponent();
        }

        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {
            conn.Open();
            OleDbCommand cmd = new OleDbCommand("Insert Into bolumler (bolumkodu,bolumadi,fakulteadi,bolumbaskani,bolumbaskaniyard) Values ('" + textBox1.Text + "','" + textBox2.Text + "' ,'" + textBox3.Text + "', '" + textBox4.Text + "' , '" + textBox1.Text + "')", conn);
            try
            {
                cmd.ExecuteNonQuery();
                conn.Close();
                MessageBox.Show("Yeni Bölümünüz Eklendi!", "Hayırlı Olsun.");
                textBox1.Clear();
                textBox2.Clear();
                textBox3.Clear();
                textBox4.Clear();
                textBox5.Clear();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Bölüm Kodundan Zaten Var!", "Başarısız!");
                conn.Close();
            }
            finally 
            {
                conn.Close();
            }

            
        
        }

        private void BolumEkle_Load(object sender, EventArgs e)
        {

        }
    }
}
