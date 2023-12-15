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
        Bestellung Bestellung = null;
        public MainWindow()
        {
            InitializeComponent();
            UpdateArtikelList();
            UpdateBestellungList();
            UpdateKundeList();
            foreach (BestellStatus b in (BestellStatus[])Enum.GetValues(typeof(BestellStatus)))
            {
                cbBestStatus.Items.Add(b);
            }
        }

        private void BtnNeu_Click(object sender, RoutedEventArgs e)
        {
            ArtikelDetailDlg artikelDetailDlg = new(null);
            artikelDetailDlg.ShowDialog();
            UpdateArtikelList();
        }

        private void BtnBearbeiten_Click(object sender, RoutedEventArgs e)
        {
            if (lbArtikel.SelectedItem != null)
            {
                ArtikelDetailDlg artikelDetailDlg = new(lbArtikel.SelectedItem as Artikel);
                artikelDetailDlg.ShowDialog();
            }
            UpdateArtikelList();
        }

        private void BtnLoeschen_Click(object sender, RoutedEventArgs e)
        {
            MessageBoxResult mbr = MessageBox.Show($"Artikel {((Artikel)lbArtikel.SelectedItem).Bezeichnung} wirklich löschen?", "WebShop", MessageBoxButton.YesNo, MessageBoxImage.Question);
            if (mbr != MessageBoxResult.Yes)
            {
                return;
            }
            ((Artikel)lbArtikel.SelectedItem).Loeschen();
            UpdateArtikelList();
        }

        private void UpdateArtikelList()
        {
            lbArtikel.Items.Clear();
            foreach (Artikel a in Artikel.AlleLesen())
            {
                lbArtikel.Items.Add(a);
            }
        }

        private void UpdateBestellungList()
        {
            lbBestellung.Items.Clear();
            foreach (Bestellung b in Bestellung.AlleLesen())
            {
                lbBestellung.Items.Add(b);
            }
        }
        private void UpdateKundeList()
        {
            lbKunden.Items.Clear();
            foreach (Kunde k in Kunde.AlleLesen())
            {
                lbKunden.Items.Add(k);
            }
        }

        private void LbArtikel_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            btnBearbeiten.IsEnabled = btnLoeschen.IsEnabled = lbArtikel.SelectedItem != null;
        }

        private void LbBestellung_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if ((Bestellung)lbBestellung.SelectedItem != null)
            {
                Bestellung = (Bestellung)lbBestellung.SelectedItem;
                cbBestStatus.SelectedItem = Bestellung.BestellStatus;
                lbBestPos.Items.Clear();
                foreach(BestellPos bestellPos in Bestellung.LstBestPoss)
                {
                    lbBestPos.Items.Add(bestellPos);
                }
                if (lbBestPos.Items.Count > 0)
                {
                    lbBestPos.IsEnabled = true;
                }
                cbBestStatus.IsEnabled = true;
            }
        }

        private void CbBestStatus_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (Bestellung != null)
            {
                Bestellung.BestellStatus = (BestellStatus)cbBestStatus.SelectedItem;
                Bestellung.Aktuallesieren();
                UpdateBestellungList();
            }
        }

        private void Artikel_GotFocus(object sender, RoutedEventArgs e)
        {
            UpdateArtikelList();
        }

        private void Kunden_GotFocus(object sender, RoutedEventArgs e)
        {
            UpdateKundeList();
        }

        private void Bestellungen_GotFocus(object sender, RoutedEventArgs e)
        {
            UpdateBestellungList();
        }
    }
}