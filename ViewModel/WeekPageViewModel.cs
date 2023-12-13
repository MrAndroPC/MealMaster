using System;
using System.Windows.Input;
using MealMaster.Model;
using MealMaster.View;

namespace MealMaster.View
{
    public class WeekPageViewModel
    {
        private WeekPlan weekPlan;

        public WeekPageViewModel(WeekPlan wweekPlan)
        {
            weekPlan = wweekPlan;
            OpenDayCommand = new RelayCommand(OpenDay);
        }

        public ICommand OpenDayCommand { get; set; }

        private void OpenDay(object parameter)
        {
            if (parameter is int dayNumber)
            {
                // Handle opening the selected day, using _weekPlan and dayNumber
                // You may open a new window or navigate to a specific page for the selected day
                Console.WriteLine($"Opening Day {dayNumber} of {weekPlan.Name}");
            }
        }
    }
}
