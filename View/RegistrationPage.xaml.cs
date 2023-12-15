using MealMaster.Model;
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
    public partial class RegistrationPage : Page
    {
        public RegistrationPage()
        {
            InitializeComponent();
        }


        private void Register_Click(object sender, RoutedEventArgs e)
        {
            if (NewLoginTextBox.Text.Length < 3)
                MessageBox.Show("Login have at least 3 characters", "Error", MessageBoxButton.OK, MessageBoxImage.Error);

            else if (NewPasswordBox.Password.Length < 3)
                MessageBox.Show("Password have at least 3 characters ", "Error", MessageBoxButton.OK, MessageBoxImage.Error);

            else if (!isPasswordsMatch(NewPasswordBox.Password, ConfirmPasswordBox.Password))
                MessageBox.Show("Passwords aren't same", "Error", MessageBoxButton.OK, MessageBoxImage.Error);

            else if (DataBase.IsLoginExists(NewLoginTextBox.Text))
                MessageBox.Show("Login's busy", "Error", MessageBoxButton.OK, MessageBoxImage.Error);

            else
            {
                MessageBox.Show("Validation complete");
                Model.User user = new Model.User()
                {
                    Name = NewUsernameTextBox.Text,
                    Login = NewLoginTextBox.Text,
                    Password = NewPasswordBox.Password
                };
                DataBase.SaveUser(user);
                NavigationService.Navigate(new AuthorizationPage());

            }
        }
        private bool isPasswordsMatch(string password, string password2) => password == password2;

        private void HaveAccount_Click(object sender, RoutedEventArgs e)
        {
            NavigationService.Navigate(new AuthorizationPage());
        }

    }


}
