using MealMaster.Model;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Windows;

namespace MealMaster.View
{
    public partial class AllIngredientsOfWeek : Window
    {
        public List<Ingredient> IngredientsList { get; set; }

        public AllIngredientsOfWeek(List<Ingredient> ingredients)
        {
            InitializeComponent();
            IngredientsList = ingredients;
            IngredientsListBox.ItemsSource = IngredientsList;
        }

        private void ExportButton_Click(object sender, RoutedEventArgs e)
        {
            string desktopPath = Environment.GetFolderPath(Environment.SpecialFolder.Desktop);
            string filePath = Path.Combine(desktopPath, "IngredientsExport.txt");
            ExportIngredientsToTxt(filePath);
        }

        private void ExportIngredientsToTxt(string filePath)
        {
            try
            {
                using (StreamWriter writer = new StreamWriter(filePath))
                {
                    foreach (var ingredient in IngredientsList)
                    {
                        writer.WriteLine($"{ingredient.Name}, {ingredient.Weight}g");
                    }
                }

                MessageBox.Show($"Ingredients exported successfully to {filePath}!", "Export Success", MessageBoxButton.OK, MessageBoxImage.Information);
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error exporting ingredients: {ex.Message}", "Export Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }
    }
}
