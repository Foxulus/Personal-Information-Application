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
    public partial class peristatistik : Form
    {
        //Sql connection bağlama komutu tekrar girilir. 

        SqlConnection baglanti = new SqlConnection("Data Source=Atilla\\SQLEXPRESS;Initial Catalog=PersonelVeriTabani;Integrated Security=True");

        public peristatistik()
        {
            InitializeComponent();
        }

        private void peristatistik_Load(object sender, EventArgs e)
        {
            //sqlcommand ve ayrıca sqldatareader açılıp çalıştırması sağlanır. Kod satırları aşağıdaki gibidir. * parantez içine alınmalı.
            baglanti.Open();
            SqlCommand komut1 = new SqlCommand("select count (*) from tbl_personel",baglanti);
            //Diğer formdaki komutguncelle.ExecuteNonQuery(); in buradaki hali bu şekilde olup sql command ile ilişkilendirmesi sağlanır.
            SqlDataReader dr1 = komut1.ExecuteReader();
            //While komutu ile personel sayısına kadarki süreci yazdırmak için kullanılır.
            while (dr1.Read())
            {
                lblpersayi.Text = dr1[0].ToString();
            }

            baglanti.Close();

            //Evli Personel Sayısı Hesaplama Kısmı
            baglanti.Open();
            SqlCommand komut2 = new SqlCommand("select count (*) from tbl_personel where perdurum = 1", baglanti);
            SqlDataReader dr2 = komut2.ExecuteReader();
            while (dr2.Read())
            {
                lblevli.Text = dr2[0].ToString();
            }

            baglanti.Close();

            //Bekar Personel Sayısı Hesaplama Kısmı
            baglanti.Open();
            SqlCommand komut3 = new SqlCommand("select count (*) from tbl_personel where perdurum = 0", baglanti);
            SqlDataReader dr3 = komut3.ExecuteReader();
            while (dr3.Read())
            {
                lblbekar.Text = dr3[0].ToString();
            }

            baglanti.Close();

            //Şehir Sayısı Hesaplama Kısmı
            baglanti.Open();
            SqlCommand komut4 = new SqlCommand("select count (distinct(persehir)) from tbl_personel",baglanti);
            SqlDataReader dr4 = komut4.ExecuteReader();
            while (dr4.Read())
            {
                lblsehir.Text = dr4[0].ToString();
            }

            baglanti.Close();

            //Toplam Maaş Hesaplama Kısmı
            baglanti.Open();
            SqlCommand komut5 = new SqlCommand("select sum (permaas) from tbl_personel",baglanti);
            SqlDataReader dr5 = komut5.ExecuteReader();
            while(dr5.Read())
            {
                lbltoplammaas.Text = dr5[0].ToString();
            }

            baglanti.Close();

            //Ortalama MaAŞ Hesaplama Kısmı
            baglanti.Open();
            SqlCommand komut6 = new SqlCommand("select avg (permaas) from tbl_personel", baglanti);
            SqlDataReader dr6 = komut6.ExecuteReader();
            while (dr6.Read())
            {
                lblortmaas.Text = dr6[0].ToString();
            }
            baglanti.Close();
        }
    }
}
