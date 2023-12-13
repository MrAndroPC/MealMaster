using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MealMaster.Model
{
    public class WeekPlan
    {
        public int WeekPlanId { get; set; }
        public List<Day> Days = new List<Day>();
        public string Name { get; set; }
        public int CreatorID { get; set; }

        public WeekPlan() 
        { 
            Days.Add(new Day());
            Ingredient ogurets = new Ingredient(
                id: 1,
                name: "Огурец",
                weight: 123m,
                fatW: 0.1m,
                carbW: 2.5m,
                proteinW: 0.8m
                );
            Recipe a = new Recipe();
            a.Name = "ddd";
            a.AddIngredient(ogurets);
            Days[0].DayName = "XXX";
            Days[0].AddRecipe(a);
        }

        public (decimal, decimal, decimal) ReturnWeekPFC()
        {
            (decimal, decimal, decimal) total_pfc = (0, 0, 0);

                foreach (Day i in Days)
                {
                    (decimal, decimal, decimal) tmp = i.ReturnDayPFC();
                    total_pfc.Item1 += tmp.Item1;
                    total_pfc.Item2 += tmp.Item2;
                    total_pfc.Item3 += tmp.Item3;
                }
            return total_pfc;

        }

        public decimal ReturnRecipeCalories()
        {
            decimal total_calories = 0;

            if (Days != null)
            {
                foreach (Day i in Days)
                {
                    total_calories += i.ReturnDayCalories();
                }
            }
            return total_calories;
        }

        public List<Ingredient> ReturnWeekIngredients()
        {
            
            var allIngredients = Days.SelectMany(day => day.Recipes.SelectMany(recipe => recipe.Ingredients));


            var aggregatedIngredients = allIngredients
                .GroupBy(ingredient => ingredient.Name)
                .Select(group => new Ingredient(
                    group.Key,
                    group.Sum(ingredient => ingredient.Weight),
                    group.Sum(ingredient => ingredient.FatW),
                    group.Sum(ingredient => ingredient.CarbW),
                    group.Sum(ingredient => ingredient.ProteinW)
                )); ;

            List<Ingredient> week_ing = aggregatedIngredients.ToList();

            return week_ing;
        }

        public bool IsCreator(int id) => CreatorID == id;
    }


}
