using ShopBase.Persistenz;
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
            dpGebDat.Text = k.GebDat.ToString();
            cbGeschlecht.SelectedItem = k.Geschlecht;
            tbAdresse.Text = k.Adresse;
            tbEmail.Text = k.Email;
        }

        private void BtAbbrechen_Click(object sender, RoutedEventArgs e)
        {
            MessageBoxResult result = MessageBox.Show("Wirklich Abbrechen?", "WebShop", MessageBoxButton.YesNo, MessageBoxImage.Question);
            if (result == MessageBoxResult.Yes)
            {
                DialogResult = false;
            }
        }

        private void BtBestaetigen_Click(object sender, RoutedEventArgs e)
        {
            kunde.Name = !string.IsNullOrWhiteSpace(tbName.Text) ? tbName.Text : kunde.Name;
            kunde.Vorname = !string.IsNullOrWhiteSpace(tbVorname.Text) ? tbVorname.Text : kunde.Vorname;
            kunde.Adresse = !string.IsNullOrWhiteSpace(tbAdresse.Text) ? tbAdresse.Text : kunde.Adresse;
            kunde.GebDat = dpGebDat.DisplayDate;
            kunde.Geschlecht = (Geschlecht)cbGeschlecht.SelectedItem;
            kunde.Aktuallesieren();
            DialogResult = true;
        }

        private void btPasswort_Click(object sender, RoutedEventArgs e)
        {
            string password = DBTools.RandomPassword();
            MessageBox.Show($"Das Passwort wird beim Ändern auf {password} gesetzt");
            kunde.Pw = DBTools.HashPassword(password);
        }
    }
}
