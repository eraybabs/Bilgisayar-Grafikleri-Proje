using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace BGProjeOdevi
{
    public partial class Form1 : Form
    {

        Double[,] XcisimNoktaYuzeyKontrol = new Double[20, 4]
        {                                        { 0.3, 0.8, 1.0, 1 },
                                                 { 0.3, 1.6, 0.3, 1 },
                                                 { 0.3, 1.2, 0.4, 1 },
                                                 { 0.3, 1.8, 0.3, 1 },
                                                 { 0.8, 0.8, 1.1, 1 },
                                                 { 0.8, 1.6, 1.0, 1 },
                                                 { 0.8, 1.2, 0.8, 1 },
                                                 { 0.8, 1.5, 0.3, 1 },
                                                 { 1.2, 0.7, 0.1, 1 },
                                                 { 1.2, 0.4, 1.5, 1 },
                                                 { 1.2, 1.1, 1.8, 1 },
                                                 { 1.2, 0.3, 0.9, 1 },
                                                 { 1.3, 0.8, 0.1, 1 },
                                                 { 1.3, 1.6, 0.7, 1 },
                                                 { 1.3, 1.2, 1.2, 1 },
                                                 { 1.3, 0.7, 0.8, 1 },
                                                 { 1.8, 0.7, 1.4, 1 },
                                                 { 1.8, 0.8, 1.0, 1 },
                                                 { 1.8, 1.6, 1.7, 1 },
                                                 { 1.8, 1.2, 0.4, 1 }};


        Double[,] XcisimNoktaBezierYuzeyKontrolNizometrik = new Double[20, 4];
        Double[,] Tizometrik = new Double[4, 4]
       {                                         {0.707, -0.408, 0, 0 },
                                                 {0,      0.816, 0, 0 },
                                                 {-0.707, -0.408, 0, 0 },
                                                 {0,      0,     0, 1 }};
        Double[,] BezierNoktaBulutuizometrik = new Double[121, 4];
        Double[,] BezierYuzeyNoktaBulutu = new Double[121, 4];
        

        public Form1()
        {
            InitializeComponent();
        }
        //buton 1 kısmı
        private void button1_Click(object sender, EventArgs e)
        {
            listBox1.Items.Add("#\t" + "x\t" + "y\t" + "z\t" + "k\t");
            for (int i = 0; i < 20; i++)
            {
                if (i % 4 == 0) listBox1.Items.Add("");
                listBox1.Items.Add(i + "\t" + XcisimNoktaYuzeyKontrol[i, 0] + "\t" + XcisimNoktaYuzeyKontrol[i, 1] + "\t" + XcisimNoktaYuzeyKontrol[i, 2] + "\t" + XcisimNoktaYuzeyKontrol[i, 3]);
            }
        }
        //buton 2 kısmı
        private void button2_Click(object sender, EventArgs e)
        {
            for ( int i = 0; i < 20; i++)
            {
                for(int j = 0; j < 4; j++)
                {
                    for(int k = 0; k < 4; k++)
                    {
                        XcisimNoktaBezierYuzeyKontrolNizometrik[i, j] += XcisimNoktaYuzeyKontrol[i, k] * Tizometrik[k, j];
                    }
                    XcisimNoktaBezierYuzeyKontrolNizometrik[i, j] = Math.Round(XcisimNoktaBezierYuzeyKontrolNizometrik[i, j], 4);
                }
            }
            listBox2.Items.Add("#\t" + "x\t" + "y\t" + "z\t");
            for (int i = 0; i < 20; i++)
            {
                if (i % 3 == 0) listBox2.Items.Add("");
                listBox2.Items.Add(i + "\t" + XcisimNoktaBezierYuzeyKontrolNizometrik[i, 0] + "\t" + XcisimNoktaBezierYuzeyKontrolNizometrik[i, 1] + "\t" + XcisimNoktaBezierYuzeyKontrolNizometrik[i, 2] + "\t" + XcisimNoktaBezierYuzeyKontrolNizometrik[i, 2]);
            }

            //string fileName = @"C:\text\1160505027_Mehmet_Emre_İçel.txt";
            //Kendi bilgisayarım için
            //string fileName = @"C:\Users\Mehmet\Desktop\text\1160505027_Mehmet_Emre_İçel.txt";
            //FileStream fs = new FileStream(fileName, FileMode.OpenOrCreate, FileAccess.Write);
            //fs.Close();
            /*
            File.AppendAllText(fileName,"\nKontrol Noktaları İZOMETRİK\n"+ "#\t" + "x\t" + "y\t" + "z\t" + "\n");
            for (int i = 0; i < 20; i++)
            {
                File.AppendAllText(fileName, i + "\t" + XcisimNoktaBezierYuzeyKontrolNizometrik[i, 0] + "\t" + XcisimNoktaBezierYuzeyKontrolNizometrik[i, 1] + "\t" + XcisimNoktaBezierYuzeyKontrolNizometrik[i, 2] + "\n");
            }
            */

        }
        //buton 3 kısmı
        private void button3_Click(object sender, EventArgs e)
        {

            Double hassasiyet = 0.1, u = 0, w;

            listBox3.Items.Add("#\t"+ "u\t"+ "w\t" + "x\t" + "y\t" + "z\t");
            for(int i = 0; i < 11; i++)
            {
                w = 0;
                for (int j = 0; j < 11; j++)
                {
                    BezierYuzeyNoktaBulutu[i * 11 + j, 0] = Math.Round(BezierHesapla(u, w, 0), 4);
                    BezierYuzeyNoktaBulutu[i * 11 + j, 1] = Math.Round(BezierHesapla(u, w, 1), 4);
                    BezierYuzeyNoktaBulutu[i * 11 + j, 2] = Math.Round(BezierHesapla(u, w, 2), 4);
                    BezierYuzeyNoktaBulutu[i * 11 + j, 3] = Math.Round(BezierHesapla(u, w, 3), 4);
                    if ((i * 11 + j) % 11 == 0) listBox3.Items.Add(""); 
                    listBox3.Items.Add((i * 11 + j) + "\t" + u + "\t" + w + "\t" + BezierYuzeyNoktaBulutu[i, 0] + "\t" + BezierYuzeyNoktaBulutu[i, 1] + "\t" + BezierYuzeyNoktaBulutu[i, 2]);

                    w += hassasiyet;
                }
                u += hassasiyet;
            }


            //4.listbox yazdırma
            for (int i = 0; i < 121; i++)
            {
                for (int j = 0; j < 4; j++)
                {
                    for (int k = 0; k < 4; k++)
                    {
                        BezierNoktaBulutuizometrik[i, j] += BezierYuzeyNoktaBulutu[i, k] * Tizometrik[k, j];
                    }
                    BezierNoktaBulutuizometrik[i, j] = Math.Round(BezierNoktaBulutuizometrik[i, j], 4);
                }
            }
            listBox4.Items.Add("#\t" + "x\t" + "y\t" + "z\t");
            for(int i = 0; i < 121; i++)
            {
                if (i % 11 == 0) listBox4.Items.Add("");
                listBox4.Items.Add(i + "\t" + BezierNoktaBulutuizometrik[i, 0] + "\t" + BezierNoktaBulutuizometrik[i, 1] + "\t" + BezierNoktaBulutuizometrik[i, 2]);
            }

            //string fileName = @"C:\text\1160505027_Mehmet_Emre_İçel.txt";
            //Kendi bilgisayarım için
            //string fileName = @"C:\Users\Mehmet\Desktop\text\1160505027_Mehmet_Emre_İçel.txt";
            /*FileStream fs = new FileStream(fileName, FileMode.OpenOrCreate, FileAccess.Write);
            fs.Close();

            File.AppendAllText(fileName, "\nBezier Y. N. BULUTU İZOMETRİK\n" + "#\t" + "x\t" + "y\t" + "z\t"+"\n");
            for (int i = 0; i < 121; i++)
            {
                File.AppendAllText(fileName,i+"\t"+ BezierNoktaBulutuizometrik[i, 0] + "\t" + BezierNoktaBulutuizometrik[i, 1] + "\t" + BezierNoktaBulutuizometrik[i, 2]+"\n");
            }
            */
        }



        private double BezierHesapla(Double uu,Double ww,int xyz)
            {
                Double sonuc = 0, T = 0, K = 0;
                int n = 4, m = 3;

                for(int i = 0; i < 5; i++)
                {
                    for(int j = 0; j < 4; j++)
                    {
                    T = (Faktor(n) / (Faktor(i) * Faktor((n - i)))) * Math.Pow(uu, i) * Math.Pow((1 - uu), (n - i));
                    K = (Faktor(m) / (Faktor(j) * Faktor((m - j)))) * Math.Pow(ww, j) * Math.Pow((1 - ww), (m - j));

                    sonuc += XcisimNoktaYuzeyKontrol[i * 4 +j, xyz] * T * K;        }
                }
                return sonuc;
            }
        private double Faktor(double sayi)
            {
                Double sonuc = 1;
                for (int i = 1; i <= sayi; i++) sonuc *= (Double)i;
                return sonuc;
            }

        
    }
}
