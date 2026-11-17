
using System.Windows;
using System.Windows.Input;

namespace Cukraszdawpf.wpf
{
    public partial class ArlistaForm : Window
    {
        public ArlistaForm()
        {
            InitializeComponent();
        }

        private void Rendel_Click(object sender, RoutedEventArgs e)
        {
            if (chkSomloi.IsChecked != true &&
                chkRepa.IsChecked != true &&
                chkCsoki.IsChecked != true &&
                chkMarcipan.IsChecked != true)
            {
                MessageBox.Show("Valassz ki legalabb egy sutemenyt!");
                return;
            }

            MessageBox.Show("A rendeles sikeres!");
        }

        private void Rendel_Jobb_Click(object sender, MouseButtonEventArgs e)
        {
            int osszeg = 0;
            int db;

            if (chkSomloi.IsChecked == true)
            {
                if (!int.TryParse(txtSomloi.Text, out db) || db <= 0)
                {
                    MessageBox.Show("Hibas mennyiseg!");
                    e.Handled = true;
                    return;
                }

                osszeg += db * 800;
            }

            if (chkRepa.IsChecked == true)
            {
                if (!int.TryParse(txtRepa.Text, out db) || db <= 0)
                {
                    MessageBox.Show("Hibas mennyiseg!");
                    e.Handled = true;
                    return;
                }

                osszeg += db * 850;
            }

            if (chkCsoki.IsChecked == true)
            {
                if (!int.TryParse(txtCsoki.Text, out db) || db <= 0)
                {
                    MessageBox.Show("Hibas mennyiseg!");
                    e.Handled = true;
                    return;
                }

                osszeg += db * 750;
            }

            if (chkMarcipan.IsChecked == true)
            {
                if (!int.TryParse(txtMarcipan.Text, out db) || db <= 0)
                {
                    MessageBox.Show("Hibas mennyiseg!");
                    e.Handled = true;
                    return;
                }

                osszeg += db * 900;
            }

            if (osszeg == 0)
            {
                MessageBox.Show("Nincs kivalasztott sutemeny!");
            }
            else
            {
                MessageBox.Show("A rendeles vegosszege: " + osszeg + " Ft");
            }

            e.Handled = true;
        }
    }
}
