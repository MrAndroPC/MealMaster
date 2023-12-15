using MealMaster.Model;
using MealMaster.View;
using System.Collections.ObjectModel;

namespace MealMaster.ViewModel
{
    public class RecipeViewModel
    {
        public Recipe CurrentRecipe { get; set; }

        public RelayCommand AddIngredientCommand { get; private set; }
        public RelayCommand EditIngredientCommand { get; private set; }
        public RelayCommand RemoveIngredientCommand { get; private set; }
        public RelayCommand SaveRecipeCommand { get; private set; }

        public RecipeViewModel()
        {
            CurrentRecipe = Session.CurrentRecipe;

            AddIngredientCommand = new RelayCommand(AddIngredient);
            EditIngredientCommand = new RelayCommand(EditIngredient);
            RemoveIngredientCommand = new RelayCommand(RemoveIngredient);
            SaveRecipeCommand = new RelayCommand(SaveRecipe);
        }


        private void AddIngredient(object parameter)
        {
            AddIngredientWindow nnn = new AddIngredientWindow();
            nnn.Show();
        }

        private void EditIngredient(object parameter)
        {
            AddIngredientWindow nnn = new AddIngredientWindow();
            ((AdIngredientViewModel)nnn.DataContext).NewIng = (parameter as Ingredient);
            nnn.Show();

        }

        private void RemoveIngredient(object parameter)
        {
        }

        private void SaveRecipe(object parameter)
        {
        }
    }
}
