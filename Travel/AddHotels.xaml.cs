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
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace Travel
{
    /// <summary>
    /// Логика взаимодействия для AddHotels.xaml
    /// </summary>
    public partial class AddHotels : Page
    {
        private Hotel _currentHotel = new Hotel();
        public AddHotels(Hotel selectedHotel)
        {
            InitializeComponent();

            if(selectedHotel != null)
            _currentHotel = selectedHotel;

            DataContext = _currentHotel;
            ComboCountries.ItemsSource = TravelEntities.GetContext().Country.ToList();

        }


        private void BtnSave_Click(object sender, RoutedEventArgs e)
        {
            StringBuilder errors = new StringBuilder();

            if (string.IsNullOrWhiteSpace(_currentHotel.Name))
                errors.AppendLine("Укажите отель");

            if (_currentHotel.CountOfStars < 1 || _currentHotel.CountOfStars > 5)
                errors.AppendLine("Введите число от 1 до 5");

            if (_currentHotel.Country == null)
                errors.AppendLine("Выберите страну");


            if(errors.Length > 0)
            {
                MessageBox.Show(errors.ToString());
                return;
            }

            if (_currentHotel.Id == 0)
                TravelEntities.GetContext().Hotel.Add(_currentHotel);

            try
            {
                TravelEntities.GetContext().SaveChanges();
                MessageBox.Show("Сохранено");
                Manager.MainFrame.GoBack();

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message.ToString());

            }

        }

        private void TextBox_TextChanged(object sender, TextChangedEventArgs e)
        {

        }

        private void TextBox_TextChanged_1(object sender, TextChangedEventArgs e)
        {

        }

        private void ComboCountries_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {

        }
    }
}
