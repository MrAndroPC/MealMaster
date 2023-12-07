using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MealMaster.Model
{
    internal class Day
    {
        public List<Recipe> Recipes { get; set; } = new List<Recipe>();
        public string DayName { get; set; }


        public (decimal, decimal, decimal) ReturnDayPFC()
        {
            (decimal, decimal, decimal) total_pfc = (0, 0, 0);

            if (Recipes != null)
            {
                foreach (Recipe i in Recipes)
                {
                    (decimal, decimal, decimal) tmp = i.ReturnRecipePFC();
                    total_pfc.Item1 += tmp.Item1;
                    total_pfc.Item2 += tmp.Item2;
                    total_pfc.Item3 += tmp.Item3;
                }
            }
            return total_pfc;

        }

        public decimal ReturnDayCalories()
        {
            decimal total_calories = 0;

            foreach (Recipe i in Recipes)
            {
                total_calories += i.ReturnRecipeCalories();
            }
            return total_calories;
        }

        public void AddRecipe(Recipe i)
        {
            Recipes.Add(i);
        }
        public void RemoveRecipe(int id)
        {
            Recipes.RemoveAt(id);
        }
    }
}
