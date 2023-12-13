using MealMaster.Model;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MealMaster.ViewModel
{
    public class RecipeViewModel
    {
        public ObservableCollection<Ingredient> Ingredients { get; set; } = new ObservableCollection<Ingredient>();

        public static void AddIngredient(Ingredient ingredient, int recipe_id)
        {

        }
    }
}
