using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace Etlap
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();

            EtlapTable.ItemsSource = EtelService.GetEtlap();
            //EtlapTable.ItemsSource = EtlapService.GetProducts().Select(x => new { x.Nev, x.Kategoria, x.Ar });
        }

        private void AddNew_Click(object sender, RoutedEventArgs e)
        {
            Window addEtelWindow = new AddEtelWindow();
            addEtelWindow.Closed += (s, ev) =>
            {
                EtlapTable.ItemsSource = EtelService.GetEtlap();
            };
            addEtelWindow.ShowDialog();
            
        }

        private void Delete_Click(object sender, RoutedEventArgs e)
        {
            if (EtlapTable.SelectedItem == null)
            {
               MessageBox.Show("Nincs kiválasztva elem!", "Hiba", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }

            Etel etel = EtlapTable.SelectedItem as Etel;

            MessageBoxResult result = MessageBox.Show($"Biztosan törölni szeretnéd a(z) {etel.Nev} nevű ételt?", "Törlés", MessageBoxButton.YesNo, MessageBoxImage.Question);
            if (result == MessageBoxResult.Yes)
            {
                if (EtelService.DeleteEtel(etel.Id))
                {
                    MessageBox.Show("Sikeres törlés!", "Siker", MessageBoxButton.OK, MessageBoxImage.Information);
                    EtlapTable.ItemsSource = EtelService.GetEtlap();
                }
                else
                {
                    MessageBox.Show("Sikertelen törlés!", "Hiba", MessageBoxButton.OK, MessageBoxImage.Error);
                }
            }
        }

        private void EtlapTable_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (EtlapTable.SelectedItem != null)
            {
                Etel etel = EtlapTable.SelectedItem as Etel;
                EtelLeiras.Content = etel.ToString();
            }
        }

        private void SzazalekosEmeles_Click(object sender, RoutedEventArgs e)
        {
            if (String.IsNullOrEmpty(SzazalekosEmAdat.Text))
            {
                MessageBox.Show("Nincs kitöltve a százalék!", "Hiba", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }
            if (EtlapTable.SelectedItem != null)
            {
                Etel etel = EtlapTable.SelectedItem as Etel;
            }
            if (int.TryParse(SzazalekosEmAdat.Text, out int szazalek))
            {
                if (szazalek < 0)
                {
                    MessageBox.Show("A százalék nem lehet negatív!", "Hiba", MessageBoxButton.OK, MessageBoxImage.Error);
                    return;
                }
            }
            else
            {
                MessageBox.Show("A százalék nem szám!", "Hiba", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }



            if (EtlapTable.SelectedItem != null)
            {
                Etel etel = EtlapTable.SelectedItem as Etel;

                MessageBoxResult result = MessageBox.Show($"Biztosan {szazalek} százalékkal emelnéd az {etel.Nev} árát?", "Százalékos emelés", MessageBoxButton.YesNo, MessageBoxImage.Question);
                if (result == MessageBoxResult.No)
                {
                    return;
                }
                
                if (EtelService.SzazalekosEmeles(etel.Id, szazalek))
                {
                    MessageBox.Show("Sikeres százalékos emelés!", "Siker", MessageBoxButton.OK, MessageBoxImage.Information);
                    EtlapTable.ItemsSource = EtelService.GetEtlap();
                }
                else
                {
                    MessageBox.Show("Sikertelen százalékos emelés!", "Hiba", MessageBoxButton.OK, MessageBoxImage.Error);
                }
            }
            else
            {
                MessageBoxResult result = MessageBox.Show($"Biztosan {szazalek} százalékkal emelnéd az árakat?", "Százalékos emelés", MessageBoxButton.YesNo, MessageBoxImage.Question);
                if (result == MessageBoxResult.No)
                {
                    return;
                }

                if (EtelService.SzazalekosEmeles(szazalek))
                {
                    MessageBox.Show("Sikeres százalékos emelés!", "Siker", MessageBoxButton.OK, MessageBoxImage.Information);
                    EtlapTable.ItemsSource = EtelService.GetEtlap();
                }
                else
                {
                    MessageBox.Show("Sikertelen százalékos emelés!", "Hiba", MessageBoxButton.OK, MessageBoxImage.Error);
                }
            }
        }

        private void FixEmeles_Click(object sender, RoutedEventArgs e)
        {
            if (String.IsNullOrEmpty(FixEmAdat.Text))
            {
                MessageBox.Show("Nincs kitöltve a fix összeg!", "Hiba", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }
            if (int.TryParse(FixEmAdat.Text, out int fix))
            {
                if (fix < 0)
                {
                    MessageBox.Show("A fix összeg nem lehet negatív!", "Hiba", MessageBoxButton.OK, MessageBoxImage.Error);
                    return;
                }
            }
            else
            {
                MessageBox.Show("A fix összeg nem szám!", "Hiba", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }

            if (EtlapTable.SelectedItem != null)
            {
                Etel etel = EtlapTable.SelectedItem as Etel;

                MessageBoxResult result = MessageBox.Show($"Biztosan {fix} Ft.-al emelnéd az {etel.Nev} árát?", "Százalékos emelés", MessageBoxButton.YesNo, MessageBoxImage.Question);
                if (result == MessageBoxResult.No)
                {
                    return;
                }

                if (EtelService.FixEmeles(etel.Id, fix))
                {
                    MessageBox.Show("Sikeres fix emelés!", "Siker", MessageBoxButton.OK, MessageBoxImage.Information);
                    EtlapTable.ItemsSource = EtelService.GetEtlap();
                }
                else
                {
                    MessageBox.Show("Sikertelen fix emelés!", "Hiba", MessageBoxButton.OK, MessageBoxImage.Error);
                }
            }
            else
            {
                MessageBoxResult result = MessageBox.Show($"Biztosan {fix} Ft.-al emelnéd az árakat?", "Százalékos emelés", MessageBoxButton.YesNo, MessageBoxImage.Question);
                if (result == MessageBoxResult.No)
                {
                    return;
                }

                if (EtelService.FixEmeles(fix))
                {
                    MessageBox.Show("Sikeres fix emelés!", "Siker", MessageBoxButton.OK, MessageBoxImage.Information);
                    EtlapTable.ItemsSource = EtelService.GetEtlap();
                }
                else
                {
                    MessageBox.Show("Sikertelen fix emelés!", "Hiba", MessageBoxButton.OK, MessageBoxImage.Error);
                }
            }
        }
    }
}