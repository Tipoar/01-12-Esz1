
using System.Windows;

namespace Cukraszdawpf.wpf
{
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }

        private void Betoltes_Click(object sender, RoutedEventArgs e)
        {
            menuSutemenyek.IsEnabled = true;

            MessageBox.Show("A sütemények betöltése sikeres!");
        }

        private void Sutemenyek_Click(object sender, RoutedEventArgs e)
        {
            ArlistaForm ablak = new ArlistaForm();

            ablak.ShowDialog();
        }

        private void Nevjegy_Click(object sender, RoutedEventArgs e)
        {
            MessageBox.Show(
                "Cukrászda alkalmazás\nKészítette: Borók Flórián és Halász Tibor",
                "Névjegy"
            );
        }
    }
}
