using System;
using System.Collections.ObjectModel;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Xml.Linq;
using MealMaster.Model;
using Microsoft.VisualBasic;

namespace MealMaster.View
{
    public class DayViewModel
    {
        public ObservableCollection<Recipe> Recipes { get; set; }
        public ICommand EditRecipeCommand { get; set; }
        public ICommand DeleteRecipeCommand { get; set; }
        public ICommand AddNewRecipeCommand { get; set; }

        private decimal _dayCalories;
        public decimal DayCalories
        {
            get { return _dayCalories; }
            set
            {
                _dayCalories = value;
            }
        }

        public DayViewModel()
        {
            Recipes = new ObservableCollection<Recipe>()
            {
                new Recipe { Name = "Recipe 1" },
                new Recipe { Name = "Recipe 2" },
            };

            EditRecipeCommand = new RelayCommand(EditRecipe);
            DeleteRecipeCommand = new RelayCommand(DeleteRecipe);
            AddNewRecipeCommand = new RelayCommand(AddNewRecipe);

            DayCalories = CalculateDayCalories();
        }

        private void EditRecipe(object parameter)
        {
        }

        private void DeleteRecipe(object parameter)
        {
            MessageBox.Show("Authorization logic goes here");
            MessageBox.Show(parameter.GetType().ToString());
            


        }

        private void AddNewRecipe(object parameter)
        {
        }

        private decimal CalculateDayCalories()
        {
            decimal totalCalories = 0;
            foreach (var recipe in Recipes)
            {
                totalCalories += recipe.ReturnRecipeCalories();
            }
            return totalCalories;
        }
    }
}
