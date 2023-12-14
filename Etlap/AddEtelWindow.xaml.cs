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

namespace Etlap
{
    /// <summary>
    /// Interaction logic for AddEtelWindow.xaml
    /// </summary>
    public partial class AddEtelWindow : Window
    {

        public AddEtelWindow()
        {
            InitializeComponent();

            Kategoria.ItemsSource = EtelService.GetEtlap().Select(x => x.Kategoria).Distinct();
        }

        private Etel GetInputs()
        {
            Etel etel;

            if (String.IsNullOrEmpty(Nev.Text))
            {
                MessageBox.Show("Nem adtál meg nevet!", "Hiba", MessageBoxButton.OK, MessageBoxImage.Error);
                return null;
            }

            if (String.IsNullOrEmpty(Leiras.Text))
            {
                MessageBox.Show("Nem adtál meg leírást!", "Hiba", MessageBoxButton.OK, MessageBoxImage.Error);
                return null;
            }

            if (String.IsNullOrEmpty(Kategoria.Text))
            {
                MessageBox.Show("Nem adtál meg kategóriát!", "Hiba", MessageBoxButton.OK, MessageBoxImage.Error);
                return null;
            }

            if (String.IsNullOrEmpty(Ar.Text))
            {
                MessageBox.Show("Nem adtál meg árat!", "Hiba", MessageBoxButton.OK, MessageBoxImage.Error);
                return null;
            }
            if (!int.TryParse(Ar.Text, out int ar))
            {
                MessageBox.Show("Az ár nem szám!", "Hiba", MessageBoxButton.OK, MessageBoxImage.Error);
                return null;
            }
            
            etel = new Etel()
            {
                Nev = Nev.Text,
                Leiras = Leiras.Text,
                Kategoria = Kategoria.Text,
                Ar = ar
            };

            return etel;
        }

        private void Submit_Click(object sender, RoutedEventArgs e)
        {
            Etel etel = GetInputs();
            if (etel != null)
            {
                if (EtelService.CreateEtel(etel))
                {
                    MessageBox.Show("Sikeres hozzáadás!", "Siker", MessageBoxButton.OK, MessageBoxImage.Information);
                    this.Close();
                }
                else
                {
                    MessageBox.Show("Sikertelen hozzáadás!", "Hiba", MessageBoxButton.OK, MessageBoxImage.Error);
                }
            }
        }
    }
}
