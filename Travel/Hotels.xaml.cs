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
    /// Логика взаимодействия для Hotels.xaml
    /// </summary>
    public partial class Hotels : Page
    {
        public Hotels()
        {
            InitializeComponent();
            //ToursGrid.ItemsSource = TravelEntities.GetContext().Hotel.ToList();

        }

               
        private void BtnEdit_Click(object sender, RoutedEventArgs e)
        {
            Manager.MainFrame.Navigate(new AddHotels((sender as Button).DataContext as Hotel));
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            Manager.MainFrame.Navigate(new AddHotels(null));
        }

        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            var hotelsForRemoving = ToursGrid.SelectedItems.Cast<Hotel>().ToList();

            if (MessageBox.Show($"Вы точно хотите удалить следующие {hotelsForRemoving.Count()} строки ?", "Внимание", MessageBoxButton.YesNo, MessageBoxImage.Question) == MessageBoxResult.Yes)
            {
                try
                {
                    TravelEntities.GetContext().Hotel.RemoveRange(hotelsForRemoving);
                    TravelEntities.GetContext().SaveChanges();
                    MessageBox.Show("Данные удалены");

                    ToursGrid.ItemsSource = TravelEntities.GetContext().Hotel.ToList();
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message.ToString());
                }
            }
        }

        private void Page_IsVisibleChanged(object sender, DependencyPropertyChangedEventArgs e)
        {
            if(Visibility == Visibility.Visible)
            {
                TravelEntities.GetContext().ChangeTracker.Entries().ToList().ForEach(p =>p.Reload());
                ToursGrid.ItemsSource=TravelEntities.GetContext().Hotel.ToList();
            }
        }

        private void ToursGrid_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {

        }
    }
}
