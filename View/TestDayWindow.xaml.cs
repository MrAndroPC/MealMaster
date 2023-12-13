using System;
using System.Windows;
using System.Windows.Controls;
using System.Xml.Linq;

namespace MealMaster.View
{
    /// <summary>
    /// Interaction logic for DayWindow.xaml
    /// </summary>
    public partial class TestDayWindow : Window
    {
        public TestDayWindow()
        {
            InitializeComponent();

            DataContext = new DayViewModel(); 
        }

        private void XX(object sender, RoutedEventArgs e)
        {
            AddIngredientWindow aadad = new AddIngredientWindow(1);
            aadad.Show();
        }

    }
}
