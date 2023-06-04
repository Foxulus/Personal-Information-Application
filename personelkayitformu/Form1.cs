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
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }
        //Burada ilk olarak bunu yapmamız gerekiyor yukarıda kütüphane ekledikten sonra.
        SqlConnection baglanti = new SqlConnection("Data Source=Atilla\\SQLEXPRESS;Initial Catalog=PersonelVeriTabani;Integrated Security=True");

        private void Form1_Load(object sender, EventArgs e)
        {           
            label1.Visible = false;
        }

        void temizle()
        {
            Txtpersonelid.Text = " ";
            TxtAd.Text = " ";
            TxtSoyad.Text = " ";
            comboBox1.Text = " ";
            maskedTextBox1.Text = " ";
            radioButton1.Checked = false;
            radioButton2.Checked = false;
            TxtAd.Focus();
        }

        private void BtnListele_Click(object sender, EventArgs e)
        {
            //Listele tuşuna basıldığında çalışacak komut.
            this.tbl_personelTableAdapter3.Fill(this.personelVeriTabaniDataSet3.tbl_personel);
        }

        private void BtnKaydet_Click(object sender, EventArgs e)
        {   
            //Bağlantıyı açıp kapatmamız ve yapılacak işlemleri de arasına yazmamız gerekiyor.
            baglanti.Open();
            //SQLCOMMAND diyerek yeni bir yer açıp içine insert komutunu ve parametreleri yazıyoruz daha sonra da parametreleri çağırıyoruz.
            SqlCommand komut = new SqlCommand("insert into Tbl_Personel(PerAd,PerSoyad,PerSehir,PerMaas,PerMeslek,PerDurum)values (@p1,@p2,@p3,@p4,@p5,@p6)",baglanti);
            komut.Parameters.AddWithValue("@p1", TxtAd.Text);
            komut.Parameters.AddWithValue("@p2", TxtSoyad.Text);
            komut.Parameters.AddWithValue("@p3", comboBox1.Text);
            komut.Parameters.AddWithValue("@p4", maskedTextBox1.Text);
            komut.Parameters.AddWithValue("@p5",TxtMeslek.Text);
            komut.Parameters.AddWithValue("@p6", label1.Text);
            //İşlemleri onaylamak ve çalışır duruma getirmek için bu kod yazılmalı.
            komut.ExecuteNonQuery();
            baglanti.Close();
            MessageBox.Show("Personel Kaydı Alınmıştır");
        }

        private void radioButton1_CheckedChanged(object sender, EventArgs e)
        {
            if(radioButton1.Checked==true)
            {
                label1.Text = "True";
            }
            
        }

        private void radioButton2_CheckedChanged(object sender, EventArgs e)
        {
            if (radioButton2.Checked==true)
            {
                label1.Text = "False";
            }
            
        }

        private void BtnTemizle_Click(object sender, EventArgs e)
        {
            temizle();
        }

        private void dataGridView1_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            //Datagridview a çift tıklandığında alttaki datadaki verileri yukarıdaki boş yerlere eklemek için gerekli olan kodlar.
            int secilen = dataGridView1.SelectedCells[0].RowIndex;
            Txtpersonelid.Text = dataGridView1.Rows[secilen].Cells[0].Value.ToString();
            TxtAd.Text = dataGridView1.Rows[secilen].Cells[1].Value.ToString();
            TxtSoyad.Text = dataGridView1.Rows[secilen].Cells[2].Value.ToString();
            comboBox1.Text = dataGridView1.Rows[secilen].Cells[3].Value.ToString();
            maskedTextBox1.Text = dataGridView1.Rows[secilen].Cells[4].Value.ToString();
            label1.Text = dataGridView1.Rows[secilen].Cells[5].Value.ToString();
            TxtMeslek.Text = dataGridView1.Rows[secilen].Cells[6].Value.ToString();
        }

        private void label1_TextChanged(object sender, EventArgs e)
        {
            //Radio buttonlarının da alttan data seçildikten sonra yukarıda gözükmesi için bu kodlar gereklidir.
            //Aynı kodları radiobutton metotlarına da yazılması gerekiyor.
            if(label1.Text == "True")
            {
                radioButton1.Checked = true;
            }
            else if (label1.Text == "False") 
            {
                radioButton2.Checked = true;
            }
        }

        private void BtnSil_Click(object sender, EventArgs e)
        {
            //Sil butonuna bastığımızda seçili olan datanın silinmesi sağlanır. Eklemedeki gibi parametre kullanılır.
            baglanti.Open();
            SqlCommand komutsil = new SqlCommand("Delete from tbl_personel where perid=@k1",baglanti);
            komutsil.Parameters.AddWithValue("@k1", Txtpersonelid.Text);
            komutsil.ExecuteNonQuery();
            baglanti.Close();
            MessageBox.Show("Personel Kaydı Silinmiştir");
        }

        private void BtnGüncelle_Click(object sender, EventArgs e)
        {
            //Güncelleye basıldığında personelin bilgilerini güncellemeye yarayan komut dizinleri bunlardır.
            baglanti.Open();
            SqlCommand komutguncelle = new SqlCommand("Update tbl_personel set perad=@a1,persoyad=@a2,persehir = @a3,permaas = @a4,perdurum=@a5,permeslek=@a6 where perid=@a7",baglanti);
            komutguncelle.Parameters.AddWithValue("@a1", TxtAd.Text);
            komutguncelle.Parameters.AddWithValue("@a2", TxtSoyad.Text);
            komutguncelle.Parameters.AddWithValue("@a3", comboBox1.Text);
            komutguncelle.Parameters.AddWithValue("@a4", maskedTextBox1.Text);
            komutguncelle.Parameters.AddWithValue("@a5", label1.Text);
            komutguncelle.Parameters.AddWithValue("@a6", TxtMeslek.Text);
            komutguncelle.Parameters.AddWithValue("@a7",Txtpersonelid.Text);
            komutguncelle.ExecuteNonQuery();
            baglanti.Close();
            MessageBox.Show("Personel Kaydı Güncellenmiştir");
        }

        private void BtnStatistik_Click(object sender, EventArgs e)
        {
            peristatistik peristatistik = new peristatistik();
            peristatistik.Show();
        }

        private void BtnGrafik_Click(object sender, EventArgs e)
        {
            pergrafik grafik = new pergrafik();
            grafik.Show();
        }
    }
}
