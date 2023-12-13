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
            if (NewLoginTextBox.Text.Length < 3)
                MessageBox.Show("Login must be at least 3 characters long. Please try again.", "Error", MessageBoxButton.OK, MessageBoxImage.Error);

            else if (NewPasswordBox.Password.Length < 3)
                MessageBox.Show("Password must be at least 3 characters long. Please try again.", "Error", MessageBoxButton.OK, MessageBoxImage.Error);

            else if (!isPasswordsMatch(NewPasswordBox.Password, ConfirmPasswordBox.Password))
                MessageBox.Show("Passwords don't match. Please try again.", "Error", MessageBoxButton.OK, MessageBoxImage.Error);

            else if (DataBase.IsLoginExists(NewLoginTextBox.Text))
                MessageBox.Show("Login's busy. Please try again.", "Error", MessageBoxButton.OK, MessageBoxImage.Error);

            else
            {
                MessageBox.Show("Registration logic goes here");
                Model.User user = new Model.User()
                {
                    Name = NewUsernameTextBox.Text,
                    Login = NewLoginTextBox.Text,
                    Password = NewPasswordBox.Password
                };
                DataBase.SaveUser(user);
            }
        }
        private bool isPasswordsMatch(string password, string password2) => password == password2;

        private void HaveAccount_Click(object sender, RoutedEventArgs e)
        {
            NavigationService.Navigate(new AuthorizationPage());
        }

    }


}
