using GrapeCity.DataVisualization.Chart;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.Common;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Windows.Forms.DataVisualization.Charting;
using ZedGraph;

using static Bulanik_Mantik.Enums;


namespace Bulanik_Mantik
{
    public partial class Form1 : Form
    {

        public Form1()
        {
            InitializeComponent();
        }
        SqlConnection baglanti;
        SqlCommand komut;
        SqlDataAdapter dataadpter;
        UyelikFonksiyonlari uyelikfonk = new UyelikFonksiyonlari();

        public void Form1_Load(object sender, EventArgs e)
        {
            ////TODO: Bu kod satırı 'bulanık_MantikDataSet.KuralTablosu' tablosuna veri yükler.Bunu gerektiği şekilde taşıyabilir, veya kaldırabilirsiniz.
            //this.kuralTablosuTableAdapter.Fill(this.bulanık_MantikDataSet.KuralTablosu);
            //baglanti = new SqlConnection("Data Source=DESKTOP-RELVKAK\\SQLEXPRESS;Initial Catalog=Bulanık_Mantik;Integrated Security=True;Encrypt=False");
            //baglanti.Open();
            //dataadpter = new SqlDataAdapter("SELECT * FROM KuralTablosu", baglanti);
            //DataTable tablo = new DataTable();
            //dataadpter.Fill(tablo);
            //KuralTablosu.DataSource = tablo;
            //baglanti.Close();
            //KuralTablosu.ClearSelection();

        }
        List<Enums.Hassaslık> hassasList = new List<Hassaslık>();
        List<Enums.Miktar> miktarList = new List<Miktar>();
        List<Enums.Kirlilik> kirlilikList = new List<Kirlilik>();
        List<double> hassasListd = new List<double>();
        List<double> miktarListd = new List<double>();
        List<double> kirlilikListd = new List<double>();
        List<double> donusListd = new List<double>();
        List<double> sureListd = new List<double>();
        List<double> deterjanListd = new List<double>();

        public void EnumTanimla()
        {
            double Hassasliktxt = Convert.ToDouble(ntxthassaslik.Text);

            double Miktartxt = Convert.ToDouble(ntxtmiktar.Text);

            double Kirliliktxt = Convert.ToDouble(ntxtkirlilik.Text);

            Enums.Hassaslık hassaslik;
            Enums.Miktar miktar;
            Enums.Kirlilik kirlilik;

            //HASSASLIK
            if (Hassasliktxt >= -1.5 && Hassasliktxt <= 4)
            {

                hassaslik = (Enums.Hassaslık.sağlam);
                hassasList.Add(hassaslik);

            }
            if (Hassasliktxt >= 3 && Hassasliktxt <= 7)
            {
                hassaslik = Enums.Hassaslık.orta;
                hassasList.Add(hassaslik);
            }
            if (Hassasliktxt >= 5.5 && Hassasliktxt <= 12)
            {
                hassaslik = Enums.Hassaslık.hassas;
                hassasList.Add(hassaslik);
            }
            else { hassaslik = Enums.Hassaslık.hassas; }


            // MİKTAR
            if (Miktartxt >= -1.5 && Miktartxt <= 4)
            {

                miktar = Enums.Miktar.küçük;
                miktarList.Add(miktar);

            }
            if (Miktartxt >= 3 && Miktartxt <= 7)
            {

                miktar = Enums.Miktar.orta;
                miktarList.Add(miktar);

            }
            if (Miktartxt >= 5.5 && Miktartxt <= 12)
            {
                miktar = Enums.Miktar.büyük;
                miktarList.Add(miktar);

            }
            else { miktar = Enums.Miktar.küçük; }

            //KİRLİLİK
            if (Kirliliktxt >= -1.5 && Kirliliktxt <= 4.5)
            {

                kirlilik = Enums.Kirlilik.küçük;
                kirlilikList.Add(kirlilik);

            }
            if (Kirliliktxt >= 3 && Kirliliktxt <= 7)
            {

                kirlilik = Enums.Kirlilik.orta;
                kirlilikList.Add(kirlilik);

            }
            if (Kirliliktxt >= 5.5 && Kirliliktxt <= 12)
            {

                kirlilik = Enums.Kirlilik.büyük;
                kirlilikList.Add(kirlilik);

            }
            else { kirlilik = Enums.Kirlilik.küçük; }

            foreach (var k in kirlilikList)
            {
                foreach (var m in miktarList)
                {
                    foreach (var h in hassasList)
                    {

                        KuralSec(h, m, k);

                    }

                }
            }

        }
        List<double> MinList = new List<double>();
        public void MinAl(double ha, double mi, double ki)
        {
            double MamdaniMin = Math.Min(Math.Min(ha, mi), ki);

            MinList.Add(MamdaniMin);

        }
        public void MaxAl(double ha, double mi, double ki)
        {


        }

        List<double> MaxDegerlerdonus = new List<double>();
        List<double> MaxDegerlersure = new List<double>();
        List<double> MaxDegerlerdeterjan = new List<double>();
        List<double> gecicidegerlerh = new List<double>();
        List<double> gecicidegerlernh = new List<double>();
        List<double> gecicidegerlero = new List<double>();
        List<double> gecicidegerlerng = new List<double>();
        List<double> gecicidegerlerg = new List<double>();
        List<double> surelerk = new List<double>();
        List<double> surelernk = new List<double>();
        List<double> surelero = new List<double>();
        List<double> surelernu = new List<double>();
        List<double> sureleru = new List<double>();
        List<double> deterjanca = new List<double>();
        List<double> deterjana = new List<double>();
        List<double> deterjano = new List<double>();
        List<double> deterjanf = new List<double>();
        List<double> deterjancf = new List<double>();

        public void Listclear()
        {

            MaxDegerlerdonus.Clear();
            MaxDegerlersure.Clear();
            MaxDegerlerdeterjan.Clear();
            gecicidegerlernh.Clear();
            gecicidegerlerh.Clear();
            gecicidegerlero.Clear();   
            gecicidegerlerg.Clear();
            gecicidegerlerng.Clear();
            surelerk.Clear();
            surelernk.Clear();
            surelero.Clear();
            surelernu.Clear();
            sureleru.Clear();
            deterjanca.Clear();
            deterjana.Clear();
            deterjano.Clear();
            deterjanf.Clear();
            deterjancf.Clear();
            MinList.Clear();
            hassasList.Clear();
            miktarList.Clear();
            kirlilikList.Clear();
            hassasListd.Clear();
            miktarListd.Clear();
            kirlilikListd.Clear();
            donusListd.Clear();
            sureListd.Clear();
            deterjanList.Clear();
            donusList.Clear();
            sureList.Clear();
            deterjanList.Clear(); 
            donusListd.Clear();
            sureListd.Clear();
            deterjanListd.Clear();



        }
        private void FormTemizlik(Control.ControlCollection controls)
        {
            foreach (Control control in controls)
            {
                if (control is TextBox)
                {
                    ((TextBox)control).Clear();
                }
                else if (control is ListBox)
                {
                    ((ListBox)control).Items.Clear();
                }
                else if (control is ComboBox)
                {
                    ((ComboBox)control).Items.Clear();
                }
                lblmam1.Text = "";
                lblmam2.Text = "";
                lblmam3.Text = "";
                lbldonusgo.Text = "";
                lblsurego.Text = "";
                lbldeterjango.Text = "";
                

                if (control.HasChildren)
                {
                    FormTemizlik(control.Controls);
                }
            }
        }


        public double donushesapla(List<double> hassas, List<double>normalhassas, List<double>orta, List<double>normalguclu, List<double>guclu)
        {

            double donushassas = -2.15;
            double donusnormalh = 2.75;
            double donusorta = 5;
            double donusnormalg = 7.25;
            double donusguclu = 11.85;

            double donusHassasMax = hassas != null && hassas.Any() ? hassas.Max() * donushassas : 0;
            double donusNormalHassasMax = normalhassas != null && normalhassas.Any() ? normalhassas.Max() * donusnormalh : 0;
            double donusOrtaMax = orta != null && orta.Any() ? orta.Max() * donusorta : 0;
            double donusNormalGucluMax = normalguclu != null && normalguclu.Any() ? normalguclu.Max() * donusnormalg : 0;
            double donusGucluMax = guclu != null && guclu.Any() ? guclu.Max() * donusguclu : 0;

            double pay = (donusHassasMax  + donusNormalHassasMax  + donusOrtaMax  + donusNormalGucluMax  +  donusGucluMax );
            double payda = (hassas != null && hassas.Any() ? hassas.Max() : 0) +
                           (normalhassas != null && normalhassas.Any() ? normalhassas.Max() : 0) +
                           (orta != null && orta.Any() ? orta.Max() : 0) +
                           (normalguclu != null && normalguclu.Any() ? normalguclu.Max() : 0) +
                           (guclu != null && guclu.Any() ? guclu.Max() : 0);

            double sonuc = payda != 0 ? pay / payda : 0;


            return sonuc;
        }

        public double surehesapla(List<double> kısa, List<double> normalksıa, List<double> orta, List<double> normaluzun, List<double> uzun)
        {

            double surekısa = -3.3;
            double surenormalk = 40;
            double sureo = 57.5;
            double surenormalu = 75;
            double sureu = 102.5;

            double surenkısaMax = kısa != null && kısa.Any() ? kısa.Max() * surekısa : 0;
            double surenormalkMax = normalksıa != null && normalksıa.Any() ? normalksıa.Max() * surenormalk : 0;
            double sureortaMax = orta != null && orta.Any() ? orta.Max() * sureo : 0;
            double surenormaluMax = normaluzun != null && normaluzun.Any() ? normaluzun.Max() * surenormalu : 0;
            double sureuzunMax = uzun != null && uzun.Any() ? uzun.Max() * sureu : 0;

            double pay = (surenkısaMax + surenormalkMax + sureortaMax + surenormaluMax + sureuzunMax);
            double payda = (kısa != null && kısa.Any() ? kısa.Max() : 0) +
                           (normalksıa != null && normalksıa.Any() ? normalksıa.Max() : 0) +
                           (orta != null && orta.Any() ? orta.Max() : 0) +
                           (normaluzun != null && normaluzun.Any() ? normaluzun.Max() : 0) +
                           (uzun != null && uzun.Any() ? uzun.Max() : 0);

            double sonuc = payda != 0 ? pay / payda : 0;


            return sonuc;
        }

        public double deterjanhesapla(List<double> cokaz, List<double> az, List<double> orta, List<double> fazla, List<double> cokfazla)
        {

            double deterjancokaz = 42.5;
            double deterjanaz = 85;
            double deterjanorta = 150;
            double deterjanfazla = 215;
            double deterjancokfazla = 257.5;

            double deterjancokazMax = cokaz != null && cokaz.Any() ? cokaz.Max() * deterjancokaz : 0;
            double deterjanazMax = az != null && az.Any() ? az.Max() * deterjanaz : 0;
            double deterjanortaMax = orta != null && orta.Any() ? orta.Max() * deterjanorta : 0;
            double deterjanfazlaMax = fazla != null && fazla.Any() ? fazla.Max() * deterjanfazla : 0;
            double deterjancokfazlaMax = cokfazla != null && cokfazla.Any() ? cokfazla.Max() * deterjancokfazla : 0;

            double pay = (deterjancokazMax + deterjanazMax + deterjanortaMax + deterjanfazlaMax + deterjancokfazlaMax);
            double payda = (cokaz != null && cokaz.Any() ? cokaz.Max() : 0) +
                           (az != null && az.Any() ? az.Max() : 0) +
                           (orta != null && orta.Any() ? orta.Max() : 0) +
                           (fazla != null &&    fazla.Any() ? fazla.Max() : 0) +
                           (cokfazla != null && cokfazla.Any() ? cokfazla.Max() : 0);

            double sonuc = payda != 0 ? pay / payda : 0;


            return sonuc;
        }

        double donus, deterjan, sure;


        public void MamdaniHesapla()
        {

            
            for (int i = 0; i < donusList.Count; i++)
            {
                var currentValue = donusList[i];

                if (donusList[i] == DonusHizi.orta)
                {
                    gecicidegerlero.Add(MinList[i]);
                    
                    
                }
                if (donusList[i] == DonusHizi.hassas)
                {
                    gecicidegerlerh.Add(MinList[i]);
                }
                if (donusList[i] == DonusHizi.normal_hassas)
                {
                    gecicidegerlernh.Add(MinList[i]);
                }
                if (donusList[i] == DonusHizi.normal_güclü)
                {
                    gecicidegerlerng.Add(MinList[i]);
                }
                if (donusList[i] == DonusHizi.güçlü)
                {
                    gecicidegerlerg.Add(MinList[i]);
                }

            }
            if (gecicidegerlero.Any())
            MaxDegerlerdonus.Add(gecicidegerlero.Max());
            if(gecicidegerlerh.Any())
            MaxDegerlerdonus.Add(gecicidegerlerh.Max());
            if(gecicidegerlernh.Any())
            MaxDegerlerdonus.Add(gecicidegerlernh.Max());
            if (gecicidegerlerng.Any())
            MaxDegerlerdonus.Add(gecicidegerlerng.Max());
            if(gecicidegerlerg.Any())
            MaxDegerlerdonus.Add(gecicidegerlerg.Max());


            
           



            ////////////////////////////////////////////////////////
            ///

            for (int i = 0; i < sureList.Count; i++)
            {
                var currentValue = sureList[i];

                if (sureList[i] == Sure.orta)
                {
                    surelero.Add(MinList[i]);


                }
                if (sureList[i] == Sure.kısa)
                {
                    surelerk.Add(MinList[i]);
                }
                if (sureList[i] == Sure.normal_kisa)
                {
                    surelernk.Add(MinList[i]);
                }
                if (sureList[i] == Sure.normal_uzun)
                {
                    surelernu.Add(MinList[i]);
                }
                if (sureList[i] == Sure.uzun)
                {
                    sureleru.Add(MinList[i]);
                }

            }
            if (surelero.Any())
                MaxDegerlersure.Add(surelero.Max());
            if (surelerk.Any())
                MaxDegerlersure.Add(surelerk.Max());
            if (surelernk.Any())
                MaxDegerlersure.Add(surelernk.Max());
            if (surelernu.Any())
                MaxDegerlersure.Add(surelernu.Max());
            if (sureleru.Any())
                MaxDegerlersure.Add(sureleru.Max());

            

            ////////////////////////////////////////////////////////////////////////////////////////////
            ///

            

            for (int i = 0; i < deterjanList.Count; i++)
            {
                var currentValue = deterjanList[i];

                if (deterjanList[i] == Deterjan.cok_az)
                {
                    deterjanca.Add(MinList[i]);


                }
                if (deterjanList[i] == Deterjan.az)
                {
                    deterjana.Add(MinList[i]);
                }
                if (deterjanList[i] == Deterjan.orta)
                {
                    deterjano.Add(MinList[i]);
                }
                if (deterjanList[i] == Deterjan.fazla)
                {
                    deterjanf.Add(MinList[i]);
                }
                if (deterjanList[i] == Deterjan.cok_fazla)
                {
                    deterjancf.Add(MinList[i]);
                }

            }
            if (deterjanca.Any())
                MaxDegerlerdeterjan.Add(deterjanca.Max());
            if (deterjana.Any())
                MaxDegerlerdeterjan.Add(deterjana.Max());
            if (deterjano.Any())
                MaxDegerlerdeterjan.Add(deterjano.Max());
            if (deterjanf.Any())
                MaxDegerlerdeterjan.Add(deterjanf.Max());
            if (deterjancf.Any())
                MaxDegerlerdeterjan.Add(deterjancf.Max());

          donus =  donushesapla(gecicidegerlerh, gecicidegerlerh, gecicidegerlero, gecicidegerlernh, gecicidegerlerg);
           sure = surehesapla(surelerk, surelernk, surelero, surelernu, sureleru);
           deterjan = deterjanhesapla(deterjanca,deterjana,deterjano,deterjanf,deterjancf);
           
        }

       
        public void lbldoldur()
        {
            HashSet<Hassaslık> uniqueValues = new HashSet<Hassaslık>();
            HashSet<Miktar> uniqueValues2 = new HashSet<Miktar>();
            HashSet<Kirlilik> uniqueValues3 = new HashSet<Kirlilik>();

            StringBuilder hb = new StringBuilder();
            StringBuilder mb = new StringBuilder();
            StringBuilder kb = new StringBuilder();

            foreach (Hassaslık item in hassasList)
            {
                if (uniqueValues.Add(item)) 
                {
                    hb.AppendLine(item.ToString());
                }
            }

            lblmam1.Text = hb.ToString();



            foreach (Miktar item in miktarList)
            {
                if (uniqueValues2.Add(item)) 
                {
                    mb.AppendLine(item.ToString());
                }
            }

            lblmam2.Text = mb.ToString();

            foreach (Kirlilik item in kirlilikList)
            {
                if (uniqueValues3.Add(item)) 
                {
                    kb.AppendLine(item.ToString());
                }
            }

            lblmam3.Text = kb.ToString();


        }

        List<Enums.DonusHizi> donusList = new List<DonusHizi>();
        List<Enums.Sure> sureList = new List<Sure>();
        List<Enums.Deterjan> deterjanList = new List<Deterjan>();



        public void KuralSec(Enums.Hassaslık hassaslik, Enums.Miktar miktar, Enums.Kirlilik kirlilik)
            {
                // Kural - 1
                if (hassaslik == Enums.Hassaslık.hassas && miktar == Enums.Miktar.küçük && kirlilik == Enums.Kirlilik.küçük)
                {
     
                double Hassaslik_ = uyelikfonk.HassaslikHassas(Convert.ToDouble(ntxthassaslik.Text));
                    double Miktar_ = uyelikfonk.MiktarKüçük(Convert.ToDouble(ntxtmiktar.Text));
                    double Kirlilik_ = uyelikfonk.KirlilikKüçük(Convert.ToDouble(ntxtkirlilik.Text));

                    Enums.DonusHizi donusHizi = Enums.DonusHizi.hassas;
                    Enums.Sure sure = Enums.Sure.kısa;
                    Enums.Deterjan deterjan = Enums.Deterjan.cok_az;

                hassasListd.Add(Hassaslik_);
                miktarListd.Add(Miktar_);
                kirlilikListd.Add(Kirlilik_);
                donusList.Add(donusHizi);
                sureList.Add(sure);
                deterjanList.Add(deterjan);
                
                MinAl(Hassaslik_,Miktar_,Kirlilik_);

                    

                }
                // Kural - 2
                else if (hassaslik == Enums.Hassaslık.hassas && miktar == Enums.Miktar.küçük && kirlilik == Enums.Kirlilik.orta)
                {

                double Hassaslik_ = uyelikfonk.HassaslikHassas(Convert.ToDouble(ntxthassaslik.Text));
                    double Miktar_ = uyelikfonk.MiktarKüçük(Convert.ToDouble(ntxtmiktar.Text));
                    double Kirlilik_ = uyelikfonk.KirlilikOrta(Convert.ToDouble(ntxtkirlilik.Text));

                    Enums.DonusHizi donusHizi = Enums.DonusHizi.normal_hassas;
                    Enums.Sure sure = Enums.Sure.kısa;
                    Enums.Deterjan deterjan = Enums.Deterjan.az;

                hassasListd.Add(Hassaslik_);
                miktarListd.Add(Miktar_);
                kirlilikListd.Add(Kirlilik_);
                donusList.Add(donusHizi);
                sureList.Add(sure);
                deterjanList.Add(deterjan);

                MinAl(Hassaslik_, Miktar_, Kirlilik_);
            }
                // Kural - 3 
                else if (hassaslik == Enums.Hassaslık.hassas && miktar == Enums.Miktar.küçük && kirlilik == Enums.Kirlilik.büyük)
                {


                double Hassaslik_ = uyelikfonk.HassaslikHassas(Convert.ToDouble(ntxthassaslik.Text));
                    double Miktar_ = uyelikfonk.MiktarKüçük(Convert.ToDouble(ntxtmiktar.Text));
                    double Kirlilik_ = uyelikfonk.KirlilikBüyük(Convert.ToDouble(ntxtkirlilik.Text));

                    Enums.DonusHizi donusHizi = Enums.DonusHizi.orta;
                    Enums.Sure sure = Enums.Sure.normal_kisa;
                    Enums.Deterjan deterjan = Enums.Deterjan.orta;

                hassasListd.Add(Hassaslik_);
                miktarListd.Add(Miktar_);
                kirlilikListd.Add(Kirlilik_);
                donusList.Add(donusHizi);
                sureList.Add(sure);
                deterjanList.Add(deterjan);

                MinAl(Hassaslik_, Miktar_, Kirlilik_);
            }
                // Kural - 4 
                else if (hassaslik == Enums.Hassaslık.hassas && miktar == Enums.Miktar.orta && kirlilik == Enums.Kirlilik.küçük)
                {
 

                double Hassaslik_ = uyelikfonk.HassaslikHassas(Convert.ToDouble(ntxthassaslik.Text));
                    double Miktar_ = uyelikfonk.MiktarOrta(Convert.ToDouble(ntxtmiktar.Text));
                    double Kirlilik_ = uyelikfonk.KirlilikKüçük(Convert.ToDouble(ntxtkirlilik.Text));

                    Enums.DonusHizi donusHizi = Enums.DonusHizi.hassas;
                    Enums.Sure sure = Enums.Sure.kısa;
                    Enums.Deterjan deterjan = Enums.Deterjan.orta;

                hassasListd.Add(Hassaslik_);
                miktarListd.Add(Miktar_);
                kirlilikListd.Add(Kirlilik_);
                donusList.Add(donusHizi);
                sureList.Add(sure);
                deterjanList.Add(deterjan);

                MinAl(Hassaslik_, Miktar_, Kirlilik_);
            }
                // Kural - 5 
                else if (hassaslik == Enums.Hassaslık.hassas && miktar == Enums.Miktar.orta && kirlilik == Enums.Kirlilik.orta)
                {

                double Hassaslik_ = uyelikfonk.HassaslikHassas(Convert.ToDouble(ntxthassaslik.Text));
                    double Miktar_ = uyelikfonk.MiktarOrta(Convert.ToDouble(ntxtmiktar.Text));
                    double Kirlilik_ = uyelikfonk.KirlilikOrta(Convert.ToDouble(ntxtkirlilik.Text));

                    Enums.DonusHizi donusHizi = Enums.DonusHizi.normal_hassas;
                    Enums.Sure sure = Enums.Sure.normal_kisa;
                    Enums.Deterjan deterjan = Enums.Deterjan.orta;

                hassasListd.Add(Hassaslik_);
                miktarListd.Add(Miktar_);
                kirlilikListd.Add(Kirlilik_);
                donusList.Add(donusHizi);
                sureList.Add(sure);
                deterjanList.Add(deterjan);

                MinAl(Hassaslik_, Miktar_, Kirlilik_);
            }
                // Kural - 6
                else if (hassaslik == Enums.Hassaslık.hassas && miktar == Enums.Miktar.orta && kirlilik == Enums.Kirlilik.büyük)
                {


                double Hassaslik_ = uyelikfonk.HassaslikHassas(Convert.ToDouble(ntxthassaslik.Text));
                    double Miktar_ = uyelikfonk.MiktarOrta(Convert.ToDouble(ntxtmiktar.Text));
                    double Kirlilik_ = uyelikfonk.KirlilikBüyük(Convert.ToDouble(ntxtkirlilik.Text));

                    Enums.DonusHizi donusHizi = Enums.DonusHizi.orta;
                    Enums.Sure sure = Enums.Sure.orta;
                    Enums.Deterjan deterjan = Enums.Deterjan.fazla;

                hassasListd.Add(Hassaslik_);
                miktarListd.Add(Miktar_);
                kirlilikListd.Add(Kirlilik_);
                donusList.Add(donusHizi);
                sureList.Add(sure);
                deterjanList.Add(deterjan);

                MinAl(Hassaslik_, Miktar_, Kirlilik_);
            }
                // Kural - 7
                else if (hassaslik == Enums.Hassaslık.hassas && miktar == Enums.Miktar.büyük && kirlilik == Enums.Kirlilik.küçük)
                {

                    double Hassaslik_ = uyelikfonk.HassaslikHassas(Convert.ToDouble(ntxthassaslik.Text));
                    double Miktar_ = uyelikfonk.MiktarBüyük(Convert.ToDouble(ntxtmiktar.Text));
                    double Kirlilik_ = uyelikfonk.KirlilikKüçük(Convert.ToDouble(ntxtkirlilik.Text));

                    Enums.DonusHizi donusHizi = Enums.DonusHizi.normal_hassas;
                    Enums.Sure sure = Enums.Sure.normal_kisa;
                    Enums.Deterjan deterjan = Enums.Deterjan.orta;

                hassasListd.Add(Hassaslik_);
                miktarListd.Add(Miktar_);
                kirlilikListd.Add(Kirlilik_);
                donusList.Add(donusHizi);
                sureList.Add(sure);
                deterjanList.Add(deterjan);

                MinAl(Hassaslik_, Miktar_, Kirlilik_);
            }
                //Kural - 8
                else if (hassaslik == Enums.Hassaslık.hassas && miktar == Enums.Miktar.büyük && kirlilik == Enums.Kirlilik.orta)
                {
                    double Hassaslik_ = uyelikfonk.HassaslikHassas(Convert.ToDouble(ntxthassaslik.Text));
                    double Miktar_ = uyelikfonk.MiktarBüyük(Convert.ToDouble(ntxtmiktar.Text));
                    double Kirlilik_ = uyelikfonk.KirlilikOrta(Convert.ToDouble(ntxtkirlilik.Text));

                    Enums.DonusHizi donusHizi = Enums.DonusHizi.normal_hassas;
                    Enums.Sure sure = Enums.Sure.orta;
                    Enums.Deterjan deterjan = Enums.Deterjan.fazla;

                hassasListd.Add(Hassaslik_);
                miktarListd.Add(Miktar_);
                kirlilikListd.Add(Kirlilik_);
                donusList.Add(donusHizi);
                sureList.Add(sure);
                deterjanList.Add(deterjan);

                MinAl(Hassaslik_, Miktar_, Kirlilik_);
            }
                //kural - 9
                else if (hassaslik == Enums.Hassaslık.hassas && miktar == Enums.Miktar.büyük && kirlilik == Enums.Kirlilik.büyük)
                {
                    double Hassaslik_ = uyelikfonk.HassaslikHassas(Convert.ToDouble(ntxthassaslik.Text));
                    double Miktar_ = uyelikfonk.MiktarBüyük(Convert.ToDouble(ntxtmiktar.Text));
                    double Kirlilik_ = uyelikfonk.KirlilikBüyük(Convert.ToDouble(ntxtkirlilik.Text));

                    Enums.DonusHizi donusHizi = Enums.DonusHizi.orta;
                    Enums.Sure sure = Enums.Sure.normal_uzun;
                    Enums.Deterjan deterjan = Enums.Deterjan.fazla;

                hassasListd.Add(Hassaslik_);
                miktarListd.Add(Miktar_);
                kirlilikListd.Add(Kirlilik_);
                donusList.Add(donusHizi);
                sureList.Add(sure);
                deterjanList.Add(deterjan);

                MinAl(Hassaslik_, Miktar_, Kirlilik_);
            }
                // Kural - 10
                else if (hassaslik == Enums.Hassaslık.orta && miktar == Enums.Miktar.küçük && kirlilik == Enums.Kirlilik.küçük)
                {
                    double Hassaslik_ = uyelikfonk.HassaslikOrta(Convert.ToDouble(ntxthassaslik.Text));
                    double Miktar_ = uyelikfonk.MiktarKüçük(Convert.ToDouble(ntxtmiktar.Text));
                    double Kirlilik_ = uyelikfonk.KirlilikKüçük(Convert.ToDouble(ntxtkirlilik.Text));

                    Enums.DonusHizi donusHizi = Enums.DonusHizi.normal_hassas;
                    Enums.Sure sure = Enums.Sure.normal_kisa;
                    Enums.Deterjan deterjan = Enums.Deterjan.az;

                hassasListd.Add(Hassaslik_);
                miktarListd.Add(Miktar_);
                kirlilikListd.Add(Kirlilik_);
                donusList.Add(donusHizi);
                sureList.Add(sure);
                deterjanList.Add(deterjan);

                MinAl(Hassaslik_, Miktar_, Kirlilik_);

            }
                // Kural - 11
                else if (hassaslik == Enums.Hassaslık.orta && miktar == Enums.Miktar.küçük && kirlilik == Enums.Kirlilik.orta)
                {
                    double Hassaslik_ = uyelikfonk.HassaslikOrta(Convert.ToDouble(ntxthassaslik.Text));
                    double Miktar_ = uyelikfonk.MiktarKüçük(Convert.ToDouble(ntxtmiktar.Text));
                    double Kirlilik_ = uyelikfonk.KirlilikOrta(Convert.ToDouble(ntxtkirlilik.Text));

                    Enums.DonusHizi donusHizi = Enums.DonusHizi.orta;
                    Enums.Sure sure = Enums.Sure.kısa;
                    Enums.Deterjan deterjan = Enums.Deterjan.orta;

                hassasListd.Add(Hassaslik_);
                miktarListd.Add(Miktar_);
                kirlilikListd.Add(Kirlilik_);
                donusList.Add(donusHizi);
                sureList.Add(sure);
                deterjanList.Add(deterjan);

                MinAl(Hassaslik_, Miktar_, Kirlilik_);
            }
                // Kural - 12 
                else if (hassaslik == Enums.Hassaslık.orta && miktar == Enums.Miktar.küçük && kirlilik == Enums.Kirlilik.büyük)
                {
                    double Hassaslik_ = uyelikfonk.HassaslikOrta(Convert.ToDouble(ntxthassaslik.Text));
                    double Miktar_ = uyelikfonk.MiktarKüçük(Convert.ToDouble(ntxtmiktar.Text));
                    double Kirlilik_ = uyelikfonk.KirlilikBüyük(Convert.ToDouble(ntxtkirlilik.Text));

                    Enums.DonusHizi donusHizi = Enums.DonusHizi.normal_güclü;
                    Enums.Sure sure = Enums.Sure.orta;
                    Enums.Deterjan deterjan = Enums.Deterjan.fazla;

                hassasListd.Add(Hassaslik_);
                miktarListd.Add(Miktar_);
                kirlilikListd.Add(Kirlilik_);
                donusList.Add(donusHizi);
                sureList.Add(sure);
                deterjanList.Add(deterjan);

                MinAl(Hassaslik_, Miktar_, Kirlilik_);
            }
                // Kural - 13
                else if (hassaslik == Enums.Hassaslık.orta && miktar == Enums.Miktar.orta && kirlilik == Enums.Kirlilik.küçük)
                {
                    double Hassaslik_ = uyelikfonk.HassaslikOrta(Convert.ToDouble(ntxthassaslik.Text));
                    double Miktar_ = uyelikfonk.MiktarOrta(Convert.ToDouble(ntxtmiktar.Text));
                    double Kirlilik_ = uyelikfonk.KirlilikKüçük(Convert.ToDouble(ntxtkirlilik.Text));

                    Enums.DonusHizi donusHizi = Enums.DonusHizi.normal_hassas;
                    Enums.Sure sure = Enums.Sure.normal_kisa;
                    Enums.Deterjan deterjan = Enums.Deterjan.orta;

                hassasListd.Add(Hassaslik_);
                miktarListd.Add(Miktar_);
                kirlilikListd.Add(Kirlilik_);
                donusList.Add(donusHizi);
                sureList.Add(sure);
                deterjanList.Add(deterjan);

                MinAl(Hassaslik_, Miktar_, Kirlilik_);
            }
                // Kural - 14
                else if (hassaslik == Enums.Hassaslık.orta && miktar == Enums.Miktar.orta && kirlilik == Enums.Kirlilik.orta)
                {
                    double Hassaslik_ = uyelikfonk.HassaslikOrta(Convert.ToDouble(ntxthassaslik.Text));
                    double Miktar_ = uyelikfonk.MiktarOrta(Convert.ToDouble(ntxtmiktar.Text));
                    double Kirlilik_ = uyelikfonk.KirlilikOrta(Convert.ToDouble(ntxtkirlilik.Text));

                    Enums.DonusHizi donusHizi = Enums.DonusHizi.orta;
                    Enums.Sure sure = Enums.Sure.orta;
                    Enums.Deterjan deterjan = Enums.Deterjan.orta;

                hassasListd.Add(Hassaslik_);
                miktarListd.Add(Miktar_);
                kirlilikListd.Add(Kirlilik_);
                donusList.Add(donusHizi);
                sureList.Add(sure);
                deterjanList.Add(deterjan);

                MinAl(Hassaslik_, Miktar_, Kirlilik_);
            }
                // Kural - 15 
                else if (hassaslik == Enums.Hassaslık.orta && miktar == Enums.Miktar.orta && kirlilik == Enums.Kirlilik.büyük)
                {
                    double Hassaslik_ = uyelikfonk.HassaslikOrta(Convert.ToDouble(ntxthassaslik.Text));
                    double Miktar_ = uyelikfonk.MiktarKüçük(Convert.ToDouble(ntxtmiktar.Text));
                    double Kirlilik_ = uyelikfonk.KirlilikKüçük(Convert.ToDouble(ntxtkirlilik.Text));

                    Enums.DonusHizi donusHizi = Enums.DonusHizi.hassas;
                    Enums.Sure sure = Enums.Sure.uzun;
                    Enums.Deterjan deterjan = Enums.Deterjan.fazla;

                hassasListd.Add(Hassaslik_);
                miktarListd.Add(Miktar_);
                kirlilikListd.Add(Kirlilik_);
                donusList.Add(donusHizi);
                sureList.Add(sure);
                deterjanList.Add(deterjan);

                MinAl(Hassaslik_, Miktar_, Kirlilik_);
            }
                // Kural -16
                else if (hassaslik == Enums.Hassaslık.orta && miktar == Enums.Miktar.büyük && kirlilik == Enums.Kirlilik.küçük)
                {
                    double Hassaslik_ = uyelikfonk.HassaslikOrta(Convert.ToDouble(ntxthassaslik.Text));
                    double Miktar_ = uyelikfonk.MiktarBüyük(Convert.ToDouble(ntxtmiktar.Text));
                    double Kirlilik_ = uyelikfonk.KirlilikKüçük(Convert.ToDouble(ntxtkirlilik.Text));

                    Enums.DonusHizi donusHizi = Enums.DonusHizi.hassas;
                    Enums.Sure sure = Enums.Sure.orta;
                    Enums.Deterjan deterjan = Enums.Deterjan.orta;

                hassasListd.Add(Hassaslik_);
                miktarListd.Add(Miktar_);
                kirlilikListd.Add(Kirlilik_);
                donusList.Add(donusHizi);
                sureList.Add(sure);
                deterjanList.Add(deterjan);

                MinAl(Hassaslik_, Miktar_, Kirlilik_);
            }
                //Kural - 17
                else if (hassaslik == Enums.Hassaslık.orta && miktar == Enums.Miktar.büyük && kirlilik == Enums.Kirlilik.orta)
                {
                    double Hassaslik_ = uyelikfonk.HassaslikOrta(Convert.ToDouble(ntxthassaslik.Text));
                    double Miktar_ = uyelikfonk.MiktarBüyük(Convert.ToDouble(ntxtmiktar.Text));
                    double Kirlilik_ = uyelikfonk.KirlilikOrta(Convert.ToDouble(ntxtkirlilik.Text));

                    Enums.DonusHizi donusHizi = Enums.DonusHizi.hassas;
                    Enums.Sure sure = Enums.Sure.normal_uzun;
                    Enums.Deterjan deterjan = Enums.Deterjan.fazla;

                hassasListd.Add(Hassaslik_);
                miktarListd.Add(Miktar_);
                kirlilikListd.Add(Kirlilik_);
                donusList.Add(donusHizi);
                sureList.Add(sure);
                deterjanList.Add(deterjan);

                MinAl(Hassaslik_, Miktar_, Kirlilik_);

            }
                //Kural - 18
                else if (hassaslik == Enums.Hassaslık.orta && miktar == Enums.Miktar.büyük && kirlilik == Enums.Kirlilik.büyük)
                {
                    double Hassaslik_ = uyelikfonk.HassaslikOrta(Convert.ToDouble(ntxthassaslik.Text));
                    double Miktar_ = uyelikfonk.MiktarBüyük(Convert.ToDouble(ntxtmiktar.Text));
                    double Kirlilik_ = uyelikfonk.KirlilikBüyük(Convert.ToDouble(ntxtkirlilik.Text));

                    Enums.DonusHizi donusHizi = Enums.DonusHizi.hassas;
                    Enums.Sure sure = Enums.Sure.uzun;
                    Enums.Deterjan deterjan = Enums.Deterjan.cok_fazla;

                hassasListd.Add(Hassaslik_);
                miktarListd.Add(Miktar_);
                kirlilikListd.Add(Kirlilik_);
                donusList.Add(donusHizi);
                sureList.Add(sure);
                deterjanList.Add(deterjan);

                MinAl(Hassaslik_, Miktar_, Kirlilik_);
            }
                // Kurak - 19
                else if (hassaslik == Enums.Hassaslık.sağlam && miktar == Enums.Miktar.küçük && kirlilik == Enums.Kirlilik.küçük)
                {
                    double Hassaslik_ = uyelikfonk.HassaslikSağlam(Convert.ToDouble(ntxthassaslik.Text));
                    double Miktar_ = uyelikfonk.MiktarKüçük(Convert.ToDouble(ntxtmiktar.Text));
                    double Kirlilik_ = uyelikfonk.KirlilikKüçük(Convert.ToDouble(ntxtkirlilik.Text));

                    Enums.DonusHizi donusHizi = Enums.DonusHizi.orta;
                    Enums.Sure sure = Enums.Sure.orta;
                    Enums.Deterjan deterjan = Enums.Deterjan.az;

                hassasListd.Add(Hassaslik_);
                miktarListd.Add(Miktar_);
                kirlilikListd.Add(Kirlilik_);
                donusList.Add(donusHizi);
                sureList.Add(sure);
                deterjanList.Add(deterjan);

                MinAl(Hassaslik_, Miktar_, Kirlilik_);
            }
                // Kural - 20 
                else if (hassaslik == Enums.Hassaslık.sağlam && miktar == Enums.Miktar.küçük && kirlilik == Enums.Kirlilik.orta)
                {
                    double Hassaslik_ = uyelikfonk.HassaslikSağlam(Convert.ToDouble(ntxthassaslik.Text));
                    double Miktar_ = uyelikfonk.MiktarKüçük(Convert.ToDouble(ntxtmiktar.Text));
                    double Kirlilik_ = uyelikfonk.KirlilikOrta(Convert.ToDouble(ntxtkirlilik.Text));

                    Enums.DonusHizi donusHizi = Enums.DonusHizi.normal_güclü;
                    Enums.Sure sure = Enums.Sure.orta;
                    Enums.Deterjan deterjan = Enums.Deterjan.orta;

                hassasListd.Add(Hassaslik_);
                miktarListd.Add(Miktar_);
                kirlilikListd.Add(Kirlilik_);
                donusList.Add(donusHizi);
                sureList.Add(sure);
                deterjanList.Add(deterjan);

                MinAl(Hassaslik_, Miktar_, Kirlilik_);
            }
                // Kural - 21 
                else if (hassaslik == Enums.Hassaslık.sağlam && miktar == Enums.Miktar.küçük && kirlilik == Enums.Kirlilik.büyük)
                {
                
                    double Hassaslik_ = uyelikfonk.HassaslikSağlam(Convert.ToDouble(ntxthassaslik.Text));
                    double Miktar_ = uyelikfonk.MiktarKüçük(Convert.ToDouble(ntxtmiktar.Text));
                    double Kirlilik_ = uyelikfonk.KirlilikBüyük(Convert.ToDouble(ntxtkirlilik.Text));

                    Enums.DonusHizi donusHizi = Enums.DonusHizi.güçlü;
                    Enums.Sure sure = Enums.Sure.normal_uzun;
                    Enums.Deterjan deterjan = Enums.Deterjan.fazla;

                hassasListd.Add(Hassaslik_);
                miktarListd.Add(Miktar_);
                kirlilikListd.Add(Kirlilik_);
                donusList.Add(donusHizi);
                sureList.Add(sure);
                deterjanList.Add(deterjan);

                MinAl(Hassaslik_, Miktar_, Kirlilik_);
            }
                // Kural - 22
                else if (hassaslik == Enums.Hassaslık.sağlam && miktar == Enums.Miktar.orta && kirlilik == Enums.Kirlilik.küçük)
                {
                    double Hassaslik_ = uyelikfonk.HassaslikSağlam(Convert.ToDouble(ntxthassaslik.Text));
                    double Miktar_ = uyelikfonk.MiktarOrta(Convert.ToDouble(ntxtmiktar.Text));
                    double Kirlilik_ = uyelikfonk.KirlilikKüçük(Convert.ToDouble(ntxtkirlilik.Text));

                    Enums.DonusHizi donusHizi = Enums.DonusHizi.orta;
                    Enums.Sure sure = Enums.Sure.orta;
                    Enums.Deterjan deterjan = Enums.Deterjan.orta;

                hassasListd.Add(Hassaslik_);
                miktarListd.Add(Miktar_);
                kirlilikListd.Add(Kirlilik_);
                donusList.Add(donusHizi);
                sureList.Add(sure);
                deterjanList.Add(deterjan);

                MinAl(Hassaslik_, Miktar_, Kirlilik_);
            }
                // Kural - 23 
                else if (hassaslik == Enums.Hassaslık.sağlam && miktar == Enums.Miktar.orta && kirlilik == Enums.Kirlilik.orta)
                {
                    double Hassaslik_ = uyelikfonk.HassaslikSağlam(Convert.ToDouble(ntxthassaslik.Text));
                    double Miktar_ = uyelikfonk.MiktarOrta(Convert.ToDouble(ntxtmiktar.Text));
                    double Kirlilik_ = uyelikfonk.KirlilikOrta(Convert.ToDouble(ntxtkirlilik.Text));

                    Enums.DonusHizi donusHizi = Enums.DonusHizi.normal_güclü;
                    Enums.Sure sure = Enums.Sure.normal_uzun;
                    Enums.Deterjan deterjan = Enums.Deterjan.orta;

                hassasListd.Add(Hassaslik_);
                miktarListd.Add(Miktar_);
                kirlilikListd.Add(Kirlilik_);
                donusList.Add(donusHizi);
                sureList.Add(sure);
                deterjanList.Add(deterjan);

                MinAl(Hassaslik_, Miktar_, Kirlilik_);
            }
                //Kural - 24
                else if (hassaslik == Enums.Hassaslık.sağlam && miktar == Enums.Miktar.orta && kirlilik == Enums.Kirlilik.büyük)
                {
                    double Hassaslik_ = uyelikfonk.HassaslikSağlam(Convert.ToDouble(ntxthassaslik.Text));
                    double Miktar_ = uyelikfonk.MiktarOrta(Convert.ToDouble(ntxtmiktar.Text));
                    double Kirlilik_ = uyelikfonk.KirlilikBüyük(Convert.ToDouble(ntxtkirlilik.Text));

                    Enums.DonusHizi donusHizi = Enums.DonusHizi.güçlü;
                    Enums.Sure sure = Enums.Sure.orta;
                    Enums.Deterjan deterjan = Enums.Deterjan.cok_fazla;

                hassasListd.Add(Hassaslik_);
                miktarListd.Add(Miktar_);
                kirlilikListd.Add(Kirlilik_);
                donusList.Add(donusHizi);
                sureList.Add(sure);
                deterjanList.Add(deterjan);

                MinAl(Hassaslik_, Miktar_, Kirlilik_);
            }
                // Kural - 25 
                else if (hassaslik == Enums.Hassaslık.sağlam && miktar == Enums.Miktar.büyük && kirlilik == Enums.Kirlilik.küçük)
                {
                    double Hassaslik_ = uyelikfonk.HassaslikSağlam(Convert.ToDouble(ntxthassaslik.Text));
                    double Miktar_ = uyelikfonk.MiktarBüyük(Convert.ToDouble(ntxtmiktar.Text));
                    double Kirlilik_ = uyelikfonk.KirlilikKüçük(Convert.ToDouble(ntxtkirlilik.Text));

                    Enums.DonusHizi donusHizi = Enums.DonusHizi.normal_güclü;
                    Enums.Sure sure = Enums.Sure.normal_uzun;
                    Enums.Deterjan deterjan = Enums.Deterjan.fazla;

                hassasListd.Add(Hassaslik_);
                miktarListd.Add(Miktar_);
                kirlilikListd.Add(Kirlilik_);
                donusList.Add(donusHizi);
                sureList.Add(sure);
                deterjanList.Add(deterjan);

                MinAl(Hassaslik_, Miktar_, Kirlilik_);
            }
                // Kural - 26
                else if (hassaslik == Enums.Hassaslık.sağlam && miktar == Enums.Miktar.büyük && kirlilik == Enums.Kirlilik.orta)
                {
                    double Hassaslik_ = uyelikfonk.HassaslikSağlam(Convert.ToDouble(ntxthassaslik.Text));
                    double Miktar_ = uyelikfonk.MiktarBüyük(Convert.ToDouble(ntxtmiktar.Text));
                    double Kirlilik_ = uyelikfonk.KirlilikOrta(Convert.ToDouble(ntxtkirlilik.Text));

                    Enums.DonusHizi donusHizi = Enums.DonusHizi.normal_güclü;
                    Enums.Sure sure = Enums.Sure.uzun;
                    Enums.Deterjan deterjan = Enums.Deterjan.fazla;

                hassasListd.Add(Hassaslik_);
                miktarListd.Add(Miktar_);
                kirlilikListd.Add(Kirlilik_);
                donusList.Add(donusHizi);
                sureList.Add(sure);
                deterjanList.Add(deterjan);

                MinAl(Hassaslik_, Miktar_, Kirlilik_);
            }
                // Kural - 27
                else if (hassaslik == Enums.Hassaslık.sağlam && miktar == Enums.Miktar.büyük && kirlilik == Enums.Kirlilik.büyük)
                {
                    double Hassaslik_ = uyelikfonk.HassaslikSağlam(Convert.ToDouble(ntxthassaslik.Text));
                    double Miktar_ = uyelikfonk.MiktarBüyük(Convert.ToDouble(ntxtmiktar.Text));
                    double Kirlilik_ = uyelikfonk.KirlilikBüyük(Convert.ToDouble(ntxtkirlilik.Text));

                    Enums.DonusHizi donusHizi = Enums.DonusHizi.güçlü;
                    Enums.Sure sure = Enums.Sure.uzun;
                    Enums.Deterjan deterjan = Enums.Deterjan.cok_fazla;

                hassasListd.Add(Hassaslik_);
                miktarListd.Add(Miktar_);
                kirlilikListd.Add(Kirlilik_);
                donusList.Add(donusHizi);
                sureList.Add(sure);
                deterjanList.Add(deterjan);

                MinAl(Hassaslik_, Miktar_, Kirlilik_);
            }

            }

            private void label6_Click(object sender, EventArgs e)
            {

            }
        private double previousXValue = 0.0;
       
        
        private void trackBar1_Scroll(object sender, EventArgs e)
        {
            double xk = trackBar1.Value;
            double normaldeger = (double)xk / 100.0;
            ntxthassaslik.Text = normaldeger.ToString();


            Series series = chart1.Series["Series4"];


            double deltaX = normaldeger - previousXValue;


            foreach (System.Windows.Forms.DataVisualization.Charting.DataPoint point in series.Points)
            {
                if (normaldeger == 0)
                {
                    point.XValue = 0.0000001;
                }
                else
                    point.XValue = normaldeger;

            }


            previousXValue = normaldeger;


            chart1.Invalidate();
        }

            private void trackBar2_Scroll(object sender, EventArgs e)
            {

            double xk = trackBar2.Value;
            double normaldeger = (double)xk / 100.0;
            ntxtmiktar.Text = normaldeger.ToString();


            Series series = chart2.Series["Series4"];


            double deltaX = normaldeger - previousXValue;


            foreach (System.Windows.Forms.DataVisualization.Charting.DataPoint point in series.Points)
            {
                
                    if (normaldeger == 0)
                    {
                        point.XValue = 0.0000001;
                    }
                    else
                        point.XValue = normaldeger;

                

            }


            previousXValue = normaldeger;


            chart2.Invalidate();
        }
        
        private void trackBar3_Scroll(object sender, EventArgs e)
            {

            double xk = trackBar3.Value;
            double normaldeger = (double)xk / 100.0;
            ntxtkirlilik.Text = normaldeger.ToString();


            Series series = chart3.Series["Series4"];


            //double deltaX = normaldeger - previousXValue;



            foreach (System.Windows.Forms.DataVisualization.Charting.DataPoint point in series.Points)
            {
                if (normaldeger == 0)
                {
                    point.XValue = 0.0000001;
                }else
                point.XValue = normaldeger;

            }


            //previousXValue = normaldeger;


            chart3.Invalidate();
        }

           

        private void button1_Click(object sender, EventArgs e)
        {
           
            EnumTanimla();
            MamdaniHesapla();
            lbldonusgo.Text = Convert.ToString(donus);
            lblsurego.Text = Convert.ToString(sure);
            lbldeterjango.Text = Convert.ToString(deterjan);
            lbldoldur();
            listBox1.Items.Clear();

           
            foreach (var item in MinList)
            {
                listBox1.Items.Add(item.ToString());
            }

        }

        private void lbldonusgo_Click(object sender, EventArgs e)
        {

        }

        private void label6_Click_1(object sender, EventArgs e)
        {

        }

        private void lblmam1_Click(object sender, EventArgs e)
        {

        }

        private void button2_Click(object sender, EventArgs e)
        {
            Listclear();
            FormTemizlik(this.Controls);
        }

        private void button3_Click(object sender, EventArgs e)
        {


            
        }

        private void lblsurego_Click(object sender, EventArgs e)
        {

        }

        private void lbldeterjango_Click(object sender, EventArgs e)
        {

        }
    }
    } 
