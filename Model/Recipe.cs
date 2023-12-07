using System.Collections.Generic;

namespace MealMaster.Model
{
    internal class Recipe
    {
        public string Name { get; set; }
        public string Description { get; set; }
        public List<Ingredient> Ingredients { get; set; } = new List<Ingredient> { };


        public decimal ReturnRecipeCalories()
        {
            decimal total_calories = 0;

            if (Ingredients != null)
            {
                foreach (Ingredient i in Ingredients)
                {
                    total_calories += i.ReturnIngredientCalories();
                }
            }
            return total_calories;
        }

        public (decimal, decimal, decimal) ReturnRecipePFC()
        {
            (decimal, decimal, decimal) total_pfc = (0, 0, 0);

            if (Ingredients != null)
            {
                foreach (Ingredient i in Ingredients)
                {
                    (decimal, decimal, decimal) tmp = i.ReturnIngredientPFC();
                    total_pfc.Item1 += tmp.Item1;
                    total_pfc.Item2 += tmp.Item2;
                    total_pfc.Item3 += tmp.Item3;
                }
            }
            return total_pfc;

        }

        public void AddIngredient(Ingredient ingredient)
        {
            Ingredients.Add(ingredient);
        }

        public void RemoveIngredient(int id)
        {
            Ingredients.RemoveAt(id);
        }
    }
}
