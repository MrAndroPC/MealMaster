using System;
using System.ComponentModel;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using LiveCharts;
using LiveCharts.Defaults;
using MealMaster.Model;
using MealMaster.View;

namespace MealMaster.ViewModel
{
    public class AdIngredientViewModel
    {
        public Ingredient NewIng { get; set; } = new Ingredient();
        public ChartValues<ObservableValue> FatValues { get; set; }
        public ChartValues<ObservableValue> CarbValues { get; set; }
        public ChartValues<ObservableValue> ProtValues { get; set; }

        public ICommand AddCommand { get; set; }


        public AdIngredientViewModel()
        {
            AddCommand = new RelayCommand(Add);


            FatValues = new ChartValues<ObservableValue> { new ObservableValue(3) };
            CarbValues = new ChartValues<ObservableValue> { new ObservableValue(4) };
            ProtValues = new ChartValues<ObservableValue> { new ObservableValue(6) };
        }

        private void Add(object parameter)
        {
            Session.CurrentRecipe.AddIngredient(NewIng);
        }

        /*        public void UpdateChart(string fat, string carb, string prot)
                {
                    FatValues[0].Value = GetDoubleValue(fat.Replace(".", ","));
                    CarbValues[0].Value = GetDoubleValue(carb.Replace(".", ","));
                    ProtValues[0].Value = GetDoubleValue(prot.Replace(".", ","));
                }
        */
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


        public void UpdateChart()
        {
            FatValues[0].Value = GetDoubleValue(NewIng.FatW.ToString());
            CarbValues[0].Value = GetDoubleValue(NewIng.CarbW.ToString());
            ProtValues[0].Value = GetDoubleValue(NewIng.ProteinW.ToString());
        }

        public event PropertyChangedEventHandler PropertyChanged;

        protected virtual void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
