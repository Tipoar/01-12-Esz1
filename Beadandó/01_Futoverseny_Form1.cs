
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Windows.Forms;

namespace Futoverseny
{
    public partial class Form1 : Form
    {
        List<Versenyzo> versenyzok = new List<Versenyzo>();

        ListBox lstVersenyzok;

        TextBox txtRajtszam;
        TextBox txtOrszag;
        TextBox txtIdo;
        TextBox txtEletkor;

        bool fajlBeolvasva = false;

        public Form1()
        {
            InitializeComponent();
            AblakLetrehozasa();
        }

        void AblakLetrehozasa()
        {
            this.Text = "Futoverseny";
            this.Size = new Size(600, 450);
            this.StartPosition = FormStartPosition.CenterScreen;

            // MENU
            MenuStrip menu = new MenuStrip();

            ToolStripMenuItem fajlMenu =
                new ToolStripMenuItem("Fájl");

            ToolStripMenuItem megnyitas =
                new ToolStripMenuItem("Megnyitás");

            megnyitas.Click += Megnyitas_Click;

            fajlMenu.DropDownItems.Add(megnyitas);

            ToolStripMenuItem eredmenyMenu =
                new ToolStripMenuItem("Eredménylista");

            eredmenyMenu.Click += EredmenyMenu_Click;

            menu.Items.Add(fajlMenu);
            menu.Items.Add(eredmenyMenu);

            this.MainMenuStrip = menu;
            this.Controls.Add(menu);

            // CIM
            Label lblCim = new Label();

            lblCim.Text = "Maratoni futóverseny";
            lblCim.Location = new Point(180, 35);
            lblCim.Size = new Size(250, 30);
            lblCim.Font = new Font("Arial", 14, FontStyle.Bold);
            lblCim.ForeColor = Color.DarkRed;

            this.Controls.Add(lblCim);

            // VERSENYZOK
            Label lblVersenyzok = new Label();

            lblVersenyzok.Text = "RÉSZTVEVŐK";
            lblVersenyzok.Location = new Point(30, 85);
            lblVersenyzok.Size = new Size(120, 25);

            this.Controls.Add(lblVersenyzok);

            lstVersenyzok = new ListBox();

            lstVersenyzok.Location = new Point(30, 110);
            lstVersenyzok.Size = new Size(140, 220);

            lstVersenyzok.SelectedIndexChanged +=
                LstVersenyzok_SelectedIndexChanged;

            this.Controls.Add(lstVersenyzok);

            // ADATMEZOK
            txtRajtszam = AdatMezo("Rajtszám", 190, 110);
            txtOrszag = AdatMezo("Ország", 190, 165);
            txtIdo = AdatMezo("Időeredmény", 190, 220);
            txtEletkor = AdatMezo("Életkor", 190, 275);

            // BEZARAS
            Button btnBezar = new Button();

            btnBezar.Text = "Bezár";
            btnBezar.Location = new Point(350, 350);
            btnBezar.Size = new Size(100, 30);

            btnBezar.Click += (s, e) => this.Close();

            this.Controls.Add(btnBezar);
        }

        // ADATMEZO LETREHOZASA
        TextBox AdatMezo(string felirat, int x, int y)
        {
            Label lbl = new Label();

            lbl.Text = felirat;
            lbl.Location = new Point(x, y);
            lbl.Size = new Size(100, 25);

            this.Controls.Add(lbl);

            TextBox txt = new TextBox();

            txt.Location = new Point(x + 110, y);
            txt.Size = new Size(130, 25);
            txt.ReadOnly = true;

            this.Controls.Add(txt);

            return txt;
        }

        // TXT BEOLVASASA
        void Beolvasas(string fajl)
        {
            try
            {
                List<Versenyzo> ujLista = new List<Versenyzo>();

                string[] sorok = File.ReadAllLines(fajl);

                foreach (string sor in sorok)
                {
                    if (string.IsNullOrWhiteSpace(sor))
                        continue;

                    string[] adatok = sor.Split(';');

                    if (adatok.Length != 5)
                        throw new Exception("Hibás adatsor: " + sor);

                    Versenyzo v = new Versenyzo();

                    v.Rajtszam = adatok[0].Trim();
                    v.Nev = adatok[1].Trim();

                    v.SzuletesiDatum = DateTime.ParseExact(
                        adatok[2].Trim(),
                        "yyyy.MM.dd",
                        CultureInfo.InvariantCulture);

                    v.Orszag = adatok[3].Trim();

                    v.Idoeredmeny = TimeSpan.ParseExact(
                        adatok[4].Trim(),
                        @"mm\:ss\.ff",
                        CultureInfo.InvariantCulture);

                    ujLista.Add(v);
                }

                if (ujLista.Count == 0)
                {
                    MessageBox.Show("A fájl nem tartalmaz versenyzőt!");
                    return;
                }

                versenyzok = ujLista;

                lstVersenyzok.Items.Clear();

                foreach (Versenyzo v in versenyzok)
                {
                    lstVersenyzok.Items.Add(v.Nev);
                }

                txtRajtszam.Clear();
                txtOrszag.Clear();
                txtIdo.Clear();
                txtEletkor.Clear();

                fajlBeolvasva = true;

                MessageBox.Show(
                    versenyzok.Count +
                    " versenyző sikeresen beolvasva!");
            }
            catch (Exception ex)
            {
                MessageBox.Show(
                    "Hiba a beolvasás során: " + ex.Message);
            }
        }

        // FAJL MEGNYITASA
        void Megnyitas_Click(object sender, EventArgs e)
        {
            if (fajlBeolvasva)
            {
                MessageBox.Show("A fájl már be lett olvasva!");
                return;
            }

            OpenFileDialog ofd = new OpenFileDialog();

            ofd.Title = "Válaszd ki a futok.txt fájlt";
            ofd.Filter = "Szövegfájl (*.txt)|*.txt";
            ofd.FileName = "futok.txt";

            if (ofd.ShowDialog() == DialogResult.OK)
            {
                Beolvasas(ofd.FileName);
            }
        }

        // VERSENYZO KIVALASZTASA
        private void LstVersenyzok_SelectedIndexChanged(
            object sender, EventArgs e)
        {
            int index = lstVersenyzok.SelectedIndex;

            if (index < 0 || index >= versenyzok.Count)
                return;

            Versenyzo v = versenyzok[index];

            txtRajtszam.Text = v.Rajtszam;
            txtOrszag.Text = v.Orszag;
            txtIdo.Text = v.Idoeredmeny.ToString(@"mm\:ss\.ff");
            txtEletkor.Text = v.Eletkor.ToString();
        }

        // MASODIK FORM MEGNYITASA
        void EredmenyMenu_Click(object sender, EventArgs e)
        {
            if (versenyzok.Count == 0)
            {
                MessageBox.Show("Először olvasd be a versenyzőket!");
                return;
            }

            Form2 eredmenyAblak = new Form2(versenyzok);

            eredmenyAblak.ShowDialog();
        }
    }
}
