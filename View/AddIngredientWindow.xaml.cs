using System;
using System.ComponentModel;
using System.Windows;
using System.Windows.Controls;
using System.Xaml.Permissions;
using LiveCharts;
using LiveCharts.Defaults;
using LiveCharts.Wpf;
using MealMaster.Model;

namespace MealMaster.View
{
    public partial class AddIngredientWindow : Window
    {
        public ChartValues<ObservableValue> FatValues { get; set; }
        public ChartValues<ObservableValue> CarbValues { get; set; }
        public ChartValues<ObservableValue> ProtValues { get; set; }

        private int recipeid;
        public AddIngredientWindow(int recipeid)
        {
            InitializeComponent();
            recipeid = this.recipeid;
            FatValues = new ChartValues<ObservableValue> { new ObservableValue(3) };
            CarbValues = new ChartValues<ObservableValue> { new ObservableValue(4) };
            ProtValues = new ChartValues<ObservableValue> { new ObservableValue(6) };
            DataContext = this;
        }
        public AddIngredientWindow(int recipeid, Ingredient ingredient)
        {
            InitializeComponent();
            recipeid = this.recipeid;
            NameTextBox.Text = ingredient.Name;
            WeightTextBox.Text = ingredient.Weight.ToString();
            FatValues = new ChartValues<ObservableValue> { new ObservableValue((double)ingredient.FatW) };
            CarbValues = new ChartValues<ObservableValue> { new ObservableValue((double)ingredient.CarbW) };
            ProtValues = new ChartValues<ObservableValue> { new ObservableValue((double)ingredient.ProteinW) };
            DataContext = this;
        }

        private void TextBox_TextChanged(object sender, RoutedEventArgs e)
        {
            string result = "";
            char[] validChars = new char[] { '0', '1', '2', '3', '4', '5', '6', '7', '8', '9', ',', '.' }; // these are ok
            foreach (char c in ((TextBox)sender).Text) // check each character in the user's input
            {
                if (Array.IndexOf(validChars, c) != -1)
                    result += c; // if this is ok, then add it to the result
            }

            ((TextBox)sender).Text = result; // change the text to the "clean" version where illegal chars have been removed.

            if (((TextBox)sender).Text != null && (TextBox)sender != WeightTextBox)
            {
                UpdateChart();
            }
        }

        private void UpdateChart()
        {
            FatValues[0].Value = GetDoubleValue(FatWTextBox.Text.Replace(".", ","));
            CarbValues[0].Value = GetDoubleValue(CarbWTextBox.Text.Replace(".", ","));
            ProtValues[0].Value = GetDoubleValue(ProteinWTextBox.Text.Replace(".", ","));
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
                Ingredient res = new Ingredient(NameTextBox.Text,
                    GetDecimalValue(WeightTextBox.Text), 
                    GetDecimalValue(FatWTextBox.Text), 
                    GetDecimalValue(CarbWTextBox.Text), 
                    GetDecimalValue(ProteinWTextBox.Text));
                ViewModel.RecipeViewModel.AddIngredient(res, recipeid);
            }
        }
    }
}
