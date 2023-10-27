using DesktopApp;
using System.Windows;
using System.Windows.Controls;

namespace FWIWebShop
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
            UpdateList();
        }

        private void BtnNeu_Click(object sender, RoutedEventArgs e)
        {
            ArtikelDetailDlg artikelDetailDlg = new(null);
            artikelDetailDlg.ShowDialog();
            UpdateList();
        }

        private void BtnBearbeiten_Click(object sender, RoutedEventArgs e)
        {
            if (lbArtikel.SelectedItem != null)
            {
                ArtikelDetailDlg artikelDetailDlg = new(lbArtikel.SelectedItem as Artikel);
                artikelDetailDlg.ShowDialog();
            }
            UpdateList();
        }

        private void BtnLoeschen_Click(object sender, RoutedEventArgs e)
        {
            MessageBoxResult mbr = MessageBox.Show($"Artikel {((Artikel)lbArtikel.SelectedItem).Bezeichnung} wirklich löschen?", "WebShop", MessageBoxButton.YesNo, MessageBoxImage.Question);
            if (mbr != MessageBoxResult.Yes)
            {
                return;
            }
            ((Artikel)lbArtikel.SelectedItem).Loeschen();
            UpdateList();
        }

        private void UpdateList()
        {
            lbArtikel.Items.Clear();
            foreach (Artikel a in Artikel.AlleLesen())
            {
                lbArtikel.Items.Add(a);
            }
        }

        private void LbArtikel_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            btnBearbeiten.IsEnabled = btnLoeschen.IsEnabled = lbArtikel.SelectedItem != null;
        }
    }
}