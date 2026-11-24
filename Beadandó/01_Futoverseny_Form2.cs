
using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace Futoverseny
{
    public partial class Form2 : Form
    {
        private List<Futoverseny.Versenyzo> versenyzok;
        private ListBox lstEredmenyek;

        public Form2(List<Futoverseny.Versenyzo> lista)
        {
            InitializeComponent();

            versenyzok = lista;

            AblakLetrehozasa();
            ListaFeltoltese();
        }

        private void AblakLetrehozasa()
        {
            this.Text = "Eredménylista";
            this.Size = new Size(450, 400);
            this.StartPosition = FormStartPosition.CenterScreen;

            Label lblCim = new Label();
            lblCim.Text = "Verseny eredménylista";
            lblCim.Location = new Point(100, 25);
            lblCim.Size = new Size(250, 30);
            lblCim.Font = new Font("Arial", 12, FontStyle.Bold);
            this.Controls.Add(lblCim);

            lstEredmenyek = new ListBox();
            lstEredmenyek.Location = new Point(50, 70);
            lstEredmenyek.Size = new Size(330, 230);
            lstEredmenyek.Font = new Font("Arial", 10);
            this.Controls.Add(lstEredmenyek);

            // JOBB KLIKKES HELYI MENÜ
            ContextMenuStrip helyiMenu = new ContextMenuStrip();

            ToolStripMenuItem mentesMenu =
                new ToolStripMenuItem("Eredménylista mentése");

            mentesMenu.Click += Mentes_Click;

            helyiMenu.Items.Add(mentesMenu);

            lstEredmenyek.ContextMenuStrip = helyiMenu;

            Button btnBezar = new Button();
            btnBezar.Text = "Bezár";
            btnBezar.Location = new Point(160, 320);
            btnBezar.Size = new Size(100, 30);
            btnBezar.Click += (s, e) => this.Close();

            this.Controls.Add(btnBezar);
        }

        private void ListaFeltoltese()
        {
            lstEredmenyek.Items.Clear();

            List<Futoverseny.Versenyzo> rendezett =
                versenyzok.OrderBy(x => x.Idoeredmeny).ToList();

            int helyezes = 1;

            foreach (Futoverseny.Versenyzo v in rendezett)
            {
                lstEredmenyek.Items.Add(
                    helyezes + ". " +
                    v.Nev + " - " +
                    v.Idoeredmeny.ToString(@"mm\:ss\.ff"));

                helyezes++;
            }
        }

        // MENTÉS
        private void Mentes_Click(object sender, EventArgs e)
        {
            using (SaveFileDialog sfd = new SaveFileDialog())
            {
                sfd.Filter = "Szövegfájl (*.txt)|*.txt";
                sfd.FileName = "EREDMENYEK.txt";
                sfd.Title = "Eredménylista mentése";

                if (sfd.ShowDialog() == DialogResult.OK)
                {
                    try
                    {
                        List<Futoverseny.Versenyzo> rendezett =
                            versenyzok.OrderBy(x => x.Idoeredmeny).ToList();

                        using (StreamWriter sw = new StreamWriter(
                            sfd.FileName, false, Encoding.UTF8))
                        {
                            int helyezes = 1;

                            foreach (Futoverseny.Versenyzo v in rendezett)
                            {
                                sw.WriteLine(
                                    helyezes + ". " +
                                    v.Rajtszam + ";" +
                                    v.Nev + ";" +
                                    v.Orszag + ";" +
                                    v.Idoeredmeny.ToString(@"mm\:ss\.ff"));

                                helyezes++;
                            }
                        }

                        MessageBox.Show(
                            "Sikeres mentés!",
                            "Mentés",
                            MessageBoxButtons.OK,
                            MessageBoxIcon.Information);
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show("Hiba: " + ex.Message);
                    }
                }
            }
        }
    }
}