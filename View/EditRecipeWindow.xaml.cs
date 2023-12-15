using MealMaster.Model;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
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
using System.Windows.Shapes;

namespace MealMaster.View
{
    public partial class EditRecipeWindow : Window
    {
        public EditRecipeWindow()
        {
            InitializeComponent();
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
/*            if (DataContext is ViewModel.RecipeViewModel viewModel)
            {
                viewModel.Initialize();
            }
*/        }
    }
}
