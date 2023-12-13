﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MealMaster.Model
{
    public class User
    {
        public int UserId { get; set; }
        public string Name { get; set; }
        public string Login { get; set; }
        public string Password { get; set; }
        public List<WeekPlan> FavouriteWeekPlans { get; set; }
        public List<Recipe> FavouriteRecipes { get; set; }
        public List<WeekPlan> CreatedPlans { get; set; }
        public List<Recipe> CreatedRecipes { get; set; } 
    }
}