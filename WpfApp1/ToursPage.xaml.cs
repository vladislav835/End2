﻿using System;
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
    /// Логика взаимодействия для ToursPage.xaml
    /// </summary>
    public partial class ToursPage : Page
    {
        public ToursPage()
        {
            InitializeComponent();

            var allTypes = Tour1Entities.GetContext().Type.ToList();
            allTypes.Insert(0, new Type
            {
                Name = "Все типы"
            });
            ComboType.ItemsSource = allTypes;

            CheckActual.IsChecked = true;
            ComboType.SelectedIndex = 0;

            UpdateTours();
        }

        private void UpdateTours()
        {
            var currentTours = Tour1Entities.GetContext().Tour.ToList();

            if (ComboType.SelectedIndex > 0)
                currentTours = currentTours.Where(p => p.Type.Contains(ComboType.SelectedItem as Type)).ToList();

            currentTours = currentTours.Where(p => p.Name.ToLower().Contains(TBoxSearch.Text.ToLower())).ToList();

            if (CheckActual.IsChecked.Value)
                currentTours = currentTours.Where(p => p.isActual).ToList();

            LViewTours.ItemsSource = currentTours.OrderBy(p => p.TicketCount).ToList();
        }

        private void ComboType_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            UpdateTours();
        }

        private void CheckActual_Checked(object sender, RoutedEventArgs e)
        {
            UpdateTours();
        }

        private void TBoxSearch_TextChanged(object sender, TextChangedEventArgs e)
        {
            UpdateTours();
        }
    }
}

