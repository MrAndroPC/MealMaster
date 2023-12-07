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

namespace MealMaster.Auth
{
    /// <summary>
    /// Логика взаимодействия для RegistrationPage.xaml
    /// </summary>
    public partial class RegistrationPage : Page
    {
        public RegistrationPage()
        {
            InitializeComponent();
        }


        private void Register_Click(object sender, RoutedEventArgs e)
        {
            // Implement your registration logic here
            MessageBox.Show("Registration logic goes here");
        }

        private void HaveAccount_Click(object sender, RoutedEventArgs e)
        {
            // Switch to the authorization page
            NavigationService.Navigate(new AuthorizationPage());
        }

    }


}
