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
    /// <summary>
    /// Логика взаимодействия для EditRecipeWindow.xaml
    /// </summary>
    public partial class EditRecipeWindow : Window
    {
        public EditRecipeWindow()
        {
            InitializeComponent();
            DataContext = new ViewModel.RecipeViewModel();
        }

        private void EditIngredient_Click(object sender, RoutedEventArgs e)
        {
        }
        private void RemoveIngredient_Click(object sender, RoutedEventArgs e)
        {
        }

        private void AddIngredient_Click(object sender, RoutedEventArgs e)
        {
            ViewModel.RecipeViewModel viewModel = (ViewModel.RecipeViewModel)DataContext;
            viewModel.Ingredients.Add(new Ingredient("New Ingredient", 0, 0, 0, 0));
        }

        private void SaveRecipe_Click(object sender, RoutedEventArgs e)
        {
        }
    }
}
