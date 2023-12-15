using LiveCharts.Defaults;
using LiveCharts;
using MealMaster.Model;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Reflection.Metadata;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace MealMaster.View
{
    /// <summary>
    /// Логика взаимодействия для WeekPlanWindow.xaml
    /// </summary>
    public partial class WeekPlanWindow : Window
    {
        public WeekPlan CurrentPlan { get; set; }
        public ObservableCollection<Recipe> Recipes1 { get; set; }
        public ObservableCollection<Recipe> Recipes2 { get; set; }
        public ObservableCollection<Recipe> Recipes3 { get; set; }
        public ObservableCollection<Recipe> Recipes4 { get; set; }
        public ObservableCollection<Recipe> Recipes5 { get; set; }
        public ObservableCollection<Recipe> Recipes6 { get; set; }
        public ObservableCollection<Recipe> Recipes7 { get; set; }

        public ChartValues<ObservableValue> FatValues { get; set; }
        public ChartValues<ObservableValue> CarbValues { get; set; }
        public ChartValues<ObservableValue> ProtValues { get; set; }

        public decimal CurrentCalories { get; set; }
        public ICommand RemCommand { get; set; }
        public ICommand OpCommand { get; set; }
        public bool IsUserMade { get; set; }

        private string originalText;
        public WeekPlanWindow()
        {
            InitializeComponent();
            CurrentPlan = Session.CurrentWeekPlan;
            originalText = CurrentPlan.Name;
            LoadRecipes();
            DataContext = this;


            RemCommand = new RelayCommand(Rem);
            OpCommand = new RelayCommand(Op);

            FatValues = new ChartValues<ObservableValue> { new ObservableValue((double)CurrentPlan.ReturnWeekPFC().Item2) };
            CarbValues = new ChartValues<ObservableValue> { new ObservableValue((double)CurrentPlan.ReturnWeekPFC().Item3) };
            ProtValues = new ChartValues<ObservableValue> { new ObservableValue((double)CurrentPlan.ReturnWeekPFC().Item1) };

            IsUserMade = (Session.CurrentUser == Session.CurrentWeekPlan.CreatorID);
        }


        private void LoadRecipes()
        {
            CurrentCalories = CurrentPlan.ReturnRecipeCalories();
            List<Day> days= DataBase.LoadDaysForWeekPlan(CurrentPlan.WeekPlanId);
            for (int i = 0; i < 7; i++) 
            {
                switch (i)
                {
                    case 0: Recipes1 = new ObservableCollection<Recipe>(DataBase.LoadRecipesForDay(days[i].DayId)); break;
                    case 1: Recipes2 = new ObservableCollection<Recipe>(DataBase.LoadRecipesForDay(days[i].DayId)); break;
                    case 2: Recipes3 = new ObservableCollection<Recipe>(DataBase.LoadRecipesForDay(days[i].DayId)); break;
                    case 3: Recipes4 = new ObservableCollection<Recipe>(DataBase.LoadRecipesForDay(days[i].DayId)); break;
                    case 4: Recipes5 = new ObservableCollection<Recipe>(DataBase.LoadRecipesForDay(days[i].DayId)); break;
                    case 5: Recipes6 = new ObservableCollection<Recipe>(DataBase.LoadRecipesForDay(days[i].DayId)); break;
                    case 6: Recipes7 = new ObservableCollection<Recipe>(DataBase.LoadRecipesForDay(days[i].DayId)); break;
                }
            }
        }
        private void TextBox_TextChanged(object sender, TextChangedEventArgs e)
        {
            string newText = (sender as TextBox)?.Text;
            ApplyWeekPlanName.Visibility = newText != originalText ? Visibility.Visible : Visibility.Collapsed;
        }


        public void RemoveRecipe_Click(object sender, RoutedEventArgs e) 
        {
            MessageBox.Show($"{(sender).GetType().ToString}", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
        }

        private void ApplyWPName_Click(object sender, RoutedEventArgs e)
        {
            DataBase.ChangeWeekPlanName(Session.CurrentWeekPlan.WeekPlanId, CurrentPlan.Name, Session.CurrentUser);
            WeekPlanNameChanged?.Invoke(this, EventArgs.Empty);
        }

        private void IngList_Click(object sender, RoutedEventArgs e)
        {
            AllIngredientsOfWeek nnn = new AllIngredientsOfWeek(CurrentPlan.ReturnWeekIngredients());
            nnn.Show();
        }

        public event EventHandler WeekPlanNameChanged;



        private void Op(object parameter)
        {
            //MessageBox.Show($"{(parameter).GetType()}", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            Session.CurrentRecipe = (parameter as Recipe);
            EditRecipeWindow nnn = new EditRecipeWindow();
            nnn.ShowDialog();
        }
        private void Rem(object parameter)
        {
            MessageBox.Show($"{(parameter as Recipe).Name}", "Error", MessageBoxButton.OK, MessageBoxImage.Error);

            if (parameter is WeekPlan plan)
            {

            }
        }

    }
}
