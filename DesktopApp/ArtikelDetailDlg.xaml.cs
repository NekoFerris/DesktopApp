using Microsoft.Win32;
using System.Drawing.Imaging;
using System.Windows;

namespace DesktopApp
{
    /// <summary>
    /// Interaktionslogik für ArtikelDetailDlg.xaml
    /// </summary>
    public partial class ArtikelDetailDlg : Window
    {
        private readonly int id;
        private readonly string? bezeichnung, beschreibung, preis;
        private ShopImage? ShopImage;
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
                if(artikel.ShopImage != null)
                {
                    imgArtikel.Source = ImgUtil.ToImageSource(artikel.ShopImage.GetImage(), ImageFormat.Jpeg);
                }
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
            if (ShopImage != null)
            {
                artikel.ShopImage = ShopImage;
            }
            if (aendern)
            {
                try
                {
                    artikel.Id = id;
                    artikel.Aktualisieren();
                    DialogResult = true;
                }
                catch(MultiUserAccessException ex)
                {
                    MessageBox.Show(ex.Message);
                    artikel = Artikel.Lesen(artikel.Id);
                    tbBez.Text  = artikel.Bezeichnung;
                    tbBesch.Text  = artikel.Beschreibung;
                    tbPreis.Text = artikel.Preis.ToString();
                    Title = $"\"{bezeichnung}\" bearbeiten";
                    if (artikel.ShopImage != null)
                    {
                        imgArtikel.Source = ImgUtil.ToImageSource(artikel.ShopImage.GetImage(), ImageFormat.Jpeg);
                    }
                }
            }
            else
            {
                artikel.Anlegen();
                DialogResult = true;
            }
        }

        private void BtnDatei_Click(object sender, RoutedEventArgs e)
        {
            OpenFileDialog dialog = new()
            {
                Multiselect = false,
                Filter = "Bilder | *.jpg;*.png;*.bmp;*.jpeg;*.webp"
            };
            dialog.ShowDialog();
            if (dialog.FileName != null)
            {
                ShopImage shopImage = new(dialog.FileName);
                shopImage.Id = id;
                ShopImage = shopImage;
                imgArtikel.Source = ImgUtil.ToImageSource(shopImage.GetImage(), ImageFormat.Jpeg);
            }
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