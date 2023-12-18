using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace DesktopApp
{
    /// <summary>
    /// Interaktionslogik für KundeDetailDlg.xaml
    /// </summary>
    public partial class KundeDetailDlg : Window
    {
        Kunde kunde;
        public KundeDetailDlg(Kunde k)
        {
            InitializeComponent();
            foreach(Geschlecht b in (Geschlecht[])Enum.GetValues(typeof(Geschlecht)))
            {
                cbGeschlecht.Items.Add(b);
            }
            kunde = k;
            tbName.Text = k.Name;
            tbVorname.Text = k.Vorname;
            dpGebDat.DisplayDate = (DateTime)k.GebDat;
            cbGeschlecht.SelectedItem = k.Geschlecht;
            tbAdresse.Text = k.Adresse;
            tbEmail.Text = k.Email;
        }

        private void btAbbrechen_Click(object sender, RoutedEventArgs e)
        {
            MessageBoxResult result = MessageBox.Show("Wirklich Abbrechen?", "WebShop", MessageBoxButton.YesNo, MessageBoxImage.Question);
            if (result == MessageBoxResult.Yes)
            {
                DialogResult = false;
            }
        }
    }
}
