using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;


namespace hannoi
{
    public partial class Form1 : Form
    {
        /*
         * 221030910008
         * Bekir Burak SAKA
         * 
         * Zaman Kompleksi : O(2^n)
         * 
         * Kaba Kod:
         * 
         *   //çubukları oluştur
         *   //diskleri oluştur
         *   //oluşturduğun diskin büyüklüğünü kontrol et
         *   //sayaç ata (hamle sayısı için)
         *   //diskleri hareket ettir DiskiTaşı();
         *   //ilk başta kaç disk kaldığını kontrol et     if (diskSayısı == 1){DiskiTaşı(çubuklar, kaynak, hedef);}
         *   //tek disk kalana kadar tekrar et
         *   else{
                HanoiÇözümü(diskSayısı - 1, kaynak, hedef, ara);
                DiskiTaşı(çubuklar, kaynak, hedef);
                HanoiÇözümü(diskSayısı - 1, ara, kaynak, hedef);
            }
         */

        private const int ÇubukSayısı = 3; //Hanoi Kulelerimiz

        private List<Label>[] çubuklar;
        private int hamleSayısı;


        public Form1()
        {
            InitializeComponent();
            ÇubuklarıHazırla();

            // ComboBox'a eleman ekle
            for (int i = 3; i <= 9; i++)
            {
                comboBox1.Items.Add(i);
            }
            comboBox1.SelectedIndex = 0; // Varsayılan olarak 3 disk seç
        }

        private void ÇubuklarıHazırla()
        {
            // Seçilen disk sayısına göre çubukları hazırla
            int diskSayısı = Convert.ToInt32(comboBox1.SelectedItem);
            çubuklar = new List<Label>[ÇubukSayısı];
            for (int i = 0; i < ÇubukSayısı; i++)
            {
                çubuklar[i] = new List<Label>();
            }

            // İlk olarak ilk çubuğa diskleri ekle
            for (int i = diskSayısı; i >= 1; i--)
            {
                Label diskEtiketi = new Label();
                diskEtiketi.Text = "Disk" + i;
                diskEtiketi.TextAlign = ContentAlignment.MiddleCenter;
                diskEtiketi.AutoSize = false;
                diskEtiketi.Size = new Size(80 + i * 20, 20);
                diskEtiketi.BackColor = Color.PowderBlue; // Buradan başlangıç disk renklerini değiştirebilirim
                çubuklar[0].Add(diskEtiketi);
            }

            // Diskleri konumlandır
            DiskleriKonumlandır();
        }

        private void DiskleriKonumlandır()
        {
            const int başlangıçX = 50;
            const int çubukBoşluğu = 175;

            for (int çubukİndex = 0; çubukİndex < ÇubukSayısı; çubukİndex++)
            {
                int x = başlangıçX + çubukİndex * çubukBoşluğu;
                int y = 300;

                for (int i = 0; i < çubuklar[çubukİndex].Count; i++)
                {
                    Label diskEtiketi = çubuklar[çubukİndex][i];
                    diskEtiketi.Location = new Point(x - diskEtiketi.Width / 2, y);
                    y -= diskEtiketi.Height + 5;
                    Controls.Add(diskEtiketi);
                }
            }
        }

        private void DiskiTaşı(List<Label>[] çubuklar, int kaynak, int hedef)
        {
            if (çubuklar[kaynak].Count == 0)
                return;

            Label disk = çubuklar[kaynak][çubuklar[kaynak].Count - 1];
            çubuklar[kaynak].Remove(disk);
            çubuklar[hedef].Add(disk);

            DiskleriKonumlandır();
            hamleSayısı++;
            listBox1.Items.Add("Hamle " + hamleSayısı + ": Diski " + (kaynak + 1) + ". Çubuktan " + (hedef + 1) + ". Çubuğa taşı");
            disk.BackColor = Color.Gainsboro;  //oynanan disklerin rengini değiştir.

            System.Threading.Thread.Sleep(1000); // 1sn bekleyerek yap.
        }

       



        private void HanoiÇözümü(int diskSayısı, int kaynak, int ara, int hedef)
        {
            if (diskSayısı == 1)
            {
                DiskiTaşı(çubuklar, kaynak, hedef);
            }
            else
            {
                HanoiÇözümü(diskSayısı - 1, kaynak, hedef, ara);
                DiskiTaşı(çubuklar, kaynak, hedef);
                HanoiÇözümü(diskSayısı - 1, ara, kaynak, hedef);
            }
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            MessageBox.Show("! Hanoi Kuleleri !");
            MessageBox.Show("1-Her Hamlede bir disk hareket ettirilebilir                                             " +
                "2-Bir disk kendisinden küçük bir diskin üzerine yerleştirilemez", "Oyun Kuralları");
        }

        private void button1_Click(object sender, EventArgs e)
        {
            listBox1.Items.Clear();
            hamleSayısı = 0;
            ÇubuklarıHazırla(); // Disk sayısı combobox'tan değiştirildiyse tekrar hazırla
            Task.Run(() => HanoiÇözümü(Convert.ToInt32(comboBox1.SelectedItem), 0, 1, 2));
        }

        private void button2_Click(object sender, EventArgs e)
        {
            Close();
        }
    }
}
