// WeekPlansPage.xaml.cs
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using MealMaster.Model;

namespace MealMaster.View
{
    public partial class WeekPlansPage : Page
    {
        public ObservableCollection<WeekPlan> WeekPlans { get; set; }
        public ObservableCollection<WeekPlan> UserWeekPlans { get; set; }
        public ICommand MakeFavoriteCommand { get; set; }
        public ICommand OpenPlanCommand { get; set; }
        public ICommand NewWeekPlanCommand { get; set; }
        public ICommand EditPlanCommand { get; set; }
        public ICommand DeletePlanCommand { get; set; }

        public WeekPlansPage()
        {
            InitializeComponent();
            WeekPlans = DataBase.LoadAllWeekPlans();
            UserWeekPlans = DataBase.LoadUserWeekPlans();

            MakeFavoriteCommand = new RelayCommand(MakeFavorite);
            OpenPlanCommand = new RelayCommand(OpenPlan);
            NewWeekPlanCommand = new RelayCommand(NewWeekPlan);
            EditPlanCommand = new RelayCommand(EditPlan);
            DeletePlanCommand = new RelayCommand(DeletePlan);

            DataContext = this;
        }

        private void MakeFavorite(object parameter)
        {
            if (parameter is WeekPlan plan)
            {
            }
        }

        private void OpenPlan(object parameter)
        {
            MessageBox.Show($"{((WeekPlan)parameter).WeekPlanId}", "Error", MessageBoxButton.OK, MessageBoxImage.Error);

            if (parameter is WeekPlan plan)
            {

            }
        }
        private void EditPlan(object parameter)
        {
            MessageBox.Show($"{((WeekPlan)parameter).WeekPlanId}", "Error", MessageBoxButton.OK, MessageBoxImage.Error);

            if (parameter is WeekPlan plan)
            {

            }
        }
        private void DeletePlan(object parameter)
        {
            MessageBox.Show($"{((WeekPlan)parameter).WeekPlanId}", "Error", MessageBoxButton.OK, MessageBoxImage.Error);

            if (parameter is WeekPlan plan)
            {
                DataBase.RemoveUserWeekPlan(((WeekPlan)parameter).WeekPlanId, Session.CurrentUser);
                RefreshListboxes();
            }
        }

        private void NewWeekPlan(object parameter)
        {
            DataBase.CreateNewWeekPlan();
            RefreshListboxes();
            /*            WeekPlanWindow nnnn = new WeekPlanWindow();
                        nnnn.Show();
            */
        }


        private void RefreshListboxes()
        {
            ObservableCollection<WeekPlan> updatedWeekPlans = DataBase.LoadAllWeekPlans();

            WeekPlans.Clear();
            foreach (var plan in updatedWeekPlans)
            {
                WeekPlans.Add(plan);
            }

            WeekPlansListbox.Items.Refresh();

            updatedWeekPlans.Clear();
            updatedWeekPlans = DataBase.LoadUserWeekPlans();

            UserWeekPlans.Clear();
            foreach (var plan in updatedWeekPlans)
            {
                UserWeekPlans.Add(plan);
            }

            UserWeekPlansListbox.Items.Refresh();
        }
    }
}
