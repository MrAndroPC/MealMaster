// WeekPlansPage.xaml.cs
using System.Collections.ObjectModel;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using MealMaster.Model;

namespace MealMaster.View
{
    public partial class WeekPlansPage : Page
    {
        public ObservableCollection<WeekPlan> WeekPlans { get; set; }
        public ICommand MakeFavoriteCommand { get; set; }
        public ICommand OpenPlanCommand { get; set; }
        public ICommand NewWeekPlanCommand { get; set; }

        public WeekPlansPage()
        {
            InitializeComponent();

            WeekPlans = new ObservableCollection<WeekPlan>
            {
                new WeekPlan { WeekPlanId = 1, Name = "Plan 1" },
                new WeekPlan { WeekPlanId = 2, Name = "Plan 2" },
            };

            MakeFavoriteCommand = new RelayCommand(MakeFavorite);
            OpenPlanCommand = new RelayCommand(OpenPlan);
            NewWeekPlanCommand = new RelayCommand(NewWeekPlan);

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
            if (parameter is WeekPlan plan)
            {
            }
        }

        private void NewWeekPlan(object parameter)
        {
            WeekPlanWindow nnnn = new WeekPlanWindow();
            nnnn.Show();
        }
    }
}
