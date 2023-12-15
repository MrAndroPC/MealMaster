using MealMaster.Model;
using MealMaster.View;
using System;
using System.Collections.Generic;
using System.Data.Entity;
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
    public partial class AuthorizationPage : Page
    {
        public AuthorizationPage()
        {
            InitializeComponent();
            DataBase.InitializeDatabase();
        }

        private void Authorize_Click(object sender, RoutedEventArgs e)
        {
            if (DataBase.IsLoginAndPasswordCorrect(UsernameTextBox.Text, PasswordBox.Password))
            {
                Session.ActivateUser(UsernameTextBox.Text);
                NavigationService.Navigate(new WeekPlansPage());
/*                TestDayWindow nnnn = new TestDayWindow();
                nnnn.Show();
                Window.GetWindow(this).Close();
*/            }
            else
            {
                MessageBox.Show("Invalid login or password. Please try again.", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private void HaveNoAccount_Click(object sender, RoutedEventArgs e)
        {
            NavigationService.Navigate(new RegistrationPage());
        }

    }
}
