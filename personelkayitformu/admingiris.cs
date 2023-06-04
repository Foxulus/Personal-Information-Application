using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Data.SqlClient;

namespace personelkayitformu
{
    public partial class admingiris : Form
    {
        public admingiris()
        {
            InitializeComponent();
        }

        SqlConnection baglanti = new SqlConnection("Data Source=Atilla\\SQLEXPRESS;Initial Catalog=PersonelVeriTabani;Integrated Security=True");

        private void button1_Click(object sender, EventArgs e)
        {
            baglanti.Open();
            SqlCommand komut = new SqlCommand("select * from tbl_yonetici where kullaniciadi=@p1 and kullanicisifre = @p2",baglanti);
            komut.Parameters.AddWithValue("@p1", txtkullanici.Text);
            komut.Parameters.AddWithValue("@p2", txtsifre.Text);
            SqlDataReader dr1 = komut.ExecuteReader();
            if (dr1.Read())
            {
                Form1 form1 = new Form1();
                form1.Show();
                this.Hide();
            }
            else
            {
                MessageBox.Show("Hatalı Kullanıcı Adı veya Şifre");
            }
            baglanti.Close();
        }
    }
}
