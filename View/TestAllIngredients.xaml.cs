using MealMaster.Model;
using System;
using System.Collections.Generic;
using System.Linq;
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
    /// Логика взаимодействия для TestAllIngredients.xaml
    /// </summary>
    public partial class TestAllIngredients : Window
    {

        public TestAllIngredients()
        {
            InitializeComponent();

            // Initialize your WeekPlan with sample data
            WeekPlan ww = new WeekPlan();
            
            // ... add days, recipes, and ingredients as needed ...

            
            DataContext = this;
        }
    }
}
