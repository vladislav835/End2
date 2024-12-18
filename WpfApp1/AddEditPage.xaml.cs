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

namespace WpfApp1
{
    /// <summary>
    /// Логика взаимодействия для AddEditPage.xaml
    /// </summary>
    public partial class AddEditPage : Page
    {
        private Hotel _CurrentHotel = new Hotel();
        public AddEditPage( Hotel selectedHotel)
        {
            InitializeComponent();

            if (selectedHotel != null)
            {
                _CurrentHotel  = selectedHotel;
            }

            DataContext = _CurrentHotel;
            ComboCountries.ItemsSource = Tour1Entities.GetContext().Country.ToList();
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {

        }

        private void BtnSave_Click(object sender, RoutedEventArgs e)
        {
                StringBuilder errors = new StringBuilder();

                if (string.IsNullOrWhiteSpace(_CurrentHotel.Name))
                    errors.AppendLine("Укажите название отеля");
                if (_CurrentHotel.CountOfStars < 1 || _CurrentHotel.CountOfStars > 5) 
                    errors.AppendLine("Количество звёзд - число от 1 до 5");
                if (_CurrentHotel.Country == null)
                    errors.AppendLine("Выберите страну");
                if (errors.Length > 0)
                {
                MessageBox.Show(errors.ToString());
                    return;
                }
                if (_CurrentHotel.id == 0)
                    Tour1Entities.GetContext().Hotel.Add(_CurrentHotel);
            try
            {
                Tour1Entities.GetContext().SaveChanges();
                MessageBox.Show("Информация сохранена!");
                Manager.MainFrame.GoBack();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message.ToString());
            }
        }
    }
}
