using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MealMaster.Model
{
    public class Ingredient
    {
        public int IngredientId { get; set; }
        public string Name { get; set; }
        public decimal Weight { get; set; }
        public decimal FatW { get; set; }
        public decimal CarbW { get; set; }
        public decimal ProteinW { get; set; }

        public Ingredient() { }

        public Ingredient(int id, string name, decimal weight, decimal fatW, decimal carbW, decimal proteinW)
        {
            IngredientId = id;
            Name = name;
            Weight = weight;
            FatW = fatW;
            CarbW = carbW;
            ProteinW = proteinW;
        }
        public Ingredient(string name, decimal weight, decimal fatW, decimal carbW, decimal proteinW)
        {
            Name = name;
            Weight = weight;
            FatW = fatW;
            CarbW = carbW;
            ProteinW = proteinW;
        }
        public decimal ReturnIngredientCalories()
        {
            return (FatW * 9 + CarbW * 4 + ProteinW * 4) / 100 * Weight;
        }
        public (decimal, decimal, decimal) ReturnIngredientPFC()
        {
            return (ProteinW, FatW, CarbW);
        }
    }
}
