using Microsoft.Win32;
using System.Drawing.Imaging;
using System.Windows;
using System.Windows.Media;

namespace DesktopApp
{
    /// <summary>
    /// Interaktionslogik für ArtikelDetailDlg.xaml
    /// </summary>
    public partial class ArtikelDetailDlg : Window
    {
        private readonly int id;
        private readonly string? bezeichnung, beschreibung, preis;
        private readonly bool aendern = false;

        public ArtikelDetailDlg(Artikel? artikel)
        {
            InitializeComponent();
            if (artikel != null)
            {
                aendern = true;
                id = artikel.Id;
                tbBez.Text = bezeichnung = artikel.Bezeichnung;
                tbBesch.Text = beschreibung = artikel.Beschreibung;
                tbPreis.Text = preis = artikel.Preis.ToString();
                Title = $"\"{bezeichnung}\" bearbeiten";
            }
            else
            {
                Title = "Neuen Artikel anlegen";
            }
            tbBez.Focus();
        }

        private void BtnBest_Click(object sender, RoutedEventArgs e)
        {
            Artikel artikel = new()
            {
                Beschreibung = tbBesch.Text.Trim(),
            };
            string bez = tbBez.Text.Trim();
            if (bez == string.Empty)
            {
                MessageBox.Show("Die Bezeichnung darf nicht Leer sein", "WebShop", MessageBoxButton.OK, MessageBoxImage.Error);
                tbBez.Focus();
                return;
            }
            if (!Double.TryParse(tbPreis.Text, out double preis))
            {
                MessageBox.Show("Im Feld Preis muss eine Zahl stehen", "WebShop", MessageBoxButton.OK, MessageBoxImage.Error);
                tbPreis.Focus();
                return;
            }
            artikel.Bezeichnung = bez;
            artikel.Preis = preis;
            if (aendern)
            {
                artikel.Id = id;
                artikel.Aendern();
            }
            else
            {
                artikel.Anlegen();
            }
            DialogResult = true;
        }

        private void BtnDatei_Click(object sender, RoutedEventArgs e)
        {
            OpenFileDialog dialog = new()
            {
                Multiselect = false,
                Filter = "Bilder | *.jpg;*.png;*.bmp;*.jpeg;*.webp"
            };
            dialog.ShowDialog();
            ShopImage shopImage = new(dialog.FileName);
            imgArtikel.Source = ImgUtil.ToImageSource(shopImage.GetImage(), ImageFormat.Jpeg);
        }

        private void BtnAbr_Click(object sender, RoutedEventArgs e)
        {
            MessageBoxResult result = MessageBox.Show("Wirklich Abbrechen?", "WebShop", MessageBoxButton.YesNo, MessageBoxImage.Question);
            if (result == MessageBoxResult.Yes)
            {
                DialogResult = false;
            }
        }

        private void BtnZur_Click(object sender, RoutedEventArgs e)
        {
            tbBez.Text = bezeichnung;
            tbBesch.Text = beschreibung;
            tbPreis.Text = preis;
        }
    }
}