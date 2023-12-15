using System;
using System.Windows;
using System.Windows.Controls;
using LiveCharts;
using LiveCharts.Defaults;
using MealMaster.Model;
using MealMaster.ViewModel;

namespace MealMaster.View
{
    public partial class AddIngredientWindow : Window
    {
        public AddIngredientWindow()
        {
            InitializeComponent();
        }

        private void TextBox_TextChanged(object sender, RoutedEventArgs e)
        {
/*            string result = "";
            char[] validChars = new char[] { '0', '1', '2', '3', '4', '5', '6', '7', '8', '9', ',', '.' }; // these are ok
            foreach (char c in ((TextBox)sender).Text) // check each character in the user's input
            {
                if (Array.IndexOf(validChars, c) != -1)
                result += c; // if this is ok, then add it to the result
            }

            ((TextBox)sender).Text = result; // change the text to the "clean" version where illegal chars have been removed.

            if (((TextBox)sender).Text != null && (TextBox)sender != WeightTextBox)
            {
*/                (this.DataContext as AdIngredientViewModel).UpdateChart();

        }

        private double GetDoubleValue(string text)
        {
            double result;
            return double.TryParse(text, out result) ? result : 0;
        }
        private decimal GetDecimalValue(string text)
        {
            decimal result;
            return decimal.TryParse(text, out result) ? result : 0;
        }

        private void AddButton_Click(object sender, RoutedEventArgs e)
        {
            if (NameTextBox.Text != null && WeightTextBox.Text != null && ProteinWTextBox.Text != null && FatWTextBox.Text != null && CarbWTextBox.Text != null)
            {
                Ingredient res = new Ingredient(
                    NameTextBox.Text,
                    GetDecimalValue(WeightTextBox.Text), 
                    GetDecimalValue(FatWTextBox.Text), 
                    GetDecimalValue(CarbWTextBox.Text), 
                    GetDecimalValue(ProteinWTextBox.Text));
                Session.CurrentRecipe.AddIngredient(res);
            }
        }
    }
}
