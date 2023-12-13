using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Data.SQLite;
namespace MealMaster.Model
{
    public static class DataBase
    {
        private const string ConnectionString = "Data Source=MealMaster.db;Version=3;";

        public static void InitializeDatabase()
        {
            using (SQLiteConnection connection = new SQLiteConnection(ConnectionString))
            {
                connection.Open();

                using (SQLiteCommand command = new SQLiteCommand(connection))
                {
                    command.CommandText = @"
                        CREATE TABLE IF NOT EXISTS Users (
                            UserId INTEGER PRIMARY KEY AUTOINCREMENT,
                            Name TEXT,
                            Login TEXT,
                            Password TEXT
                        );

                        CREATE TABLE IF NOT EXISTS Ingredients (
                            IngredientId INTEGER PRIMARY KEY AUTOINCREMENT,
                            Name TEXT,
                            Weight DECIMAL,
                            FatW DECIMAL,
                            CarbW DECIMAL,
                            ProteinW DECIMAL
                        );

                        CREATE TABLE IF NOT EXISTS Recipes (
                            RecipeId INTEGER PRIMARY KEY AUTOINCREMENT,
                            Name TEXT,
                            Description TEXT,
                            CreatorID INTEGER,
                            FOREIGN KEY (CreatorID) REFERENCES Users(UserId)
                        );

                        CREATE TABLE IF NOT EXISTS Days (
                            DayId INTEGER PRIMARY KEY AUTOINCREMENT,
                            DayName TEXT
                        );

                        CREATE TABLE IF NOT EXISTS WeekPlans (
                            WeekPlanId INTEGER PRIMARY KEY AUTOINCREMENT,
                            Name TEXT,
                            CreatorID INTEGER,
                            FOREIGN KEY (CreatorID) REFERENCES Users(UserId)
                        );

                        CREATE TABLE IF NOT EXISTS UserFavorites (
                            UserId INTEGER,
                            RecipeId INTEGER,
                            WeekPlanId INTEGER,
                            FOREIGN KEY (UserId) REFERENCES Users(UserId),
                            FOREIGN KEY (RecipeId) REFERENCES Recipes(RecipeId),
                            FOREIGN KEY (WeekPlanId) REFERENCES WeekPlans(WeekPlanId),
                            PRIMARY KEY (UserId, RecipeId, WeekPlanId)
                        );

                        CREATE TABLE IF NOT EXISTS RecipeIngredients (
                            RecipeId INTEGER,
                            IngredientId INTEGER,
                            FOREIGN KEY (RecipeId) REFERENCES Recipes(RecipeId),
                            FOREIGN KEY (IngredientId) REFERENCES Ingredients(IngredientId),
                            PRIMARY KEY (RecipeId, IngredientId)
                        );

                        CREATE TABLE IF NOT EXISTS DayRecipes (
                            DayId INTEGER,
                            RecipeId INTEGER,
                            FOREIGN KEY (DayId) REFERENCES Days(DayId),
                            FOREIGN KEY (RecipeId) REFERENCES Recipes(RecipeId),
                            PRIMARY KEY (DayId, RecipeId)
                        );

                        CREATE TABLE IF NOT EXISTS WeekPlanDays (
                            WeekPlanId INTEGER,
                            DayId INTEGER,
                            FOREIGN KEY (WeekPlanId) REFERENCES WeekPlans(WeekPlanId),
                            FOREIGN KEY (DayId) REFERENCES Days(DayId),
                            PRIMARY KEY (WeekPlanId, DayId)
                        );
                    ";
                    command.ExecuteNonQuery();
                }
            }
        }

        public static void SaveUser(User user)
        {
            using (SQLiteConnection connection = new SQLiteConnection(ConnectionString))
            {
                connection.Open();

                using (SQLiteCommand command = new SQLiteCommand(connection))
                {
                    command.CommandText = "INSERT INTO Users (Name, Login, Password) VALUES (@Name, @Login, @Password);";
                    command.Parameters.AddWithValue("@Name", user.Name);
                    command.Parameters.AddWithValue("@Login", user.Login);
                    command.Parameters.AddWithValue("@Password", user.Password);
                    command.ExecuteNonQuery();
                }
            }
        }

        public static bool IsLoginExists(string login)
        {
            using (SQLiteConnection connection = new SQLiteConnection(ConnectionString))
            {
                connection.Open();

                using (SQLiteCommand command = new SQLiteCommand(connection))
                {
                    command.CommandText = "SELECT COUNT(*) FROM Users WHERE Login = @login";
                    command.Parameters.AddWithValue("@login", login);

                    int count = Convert.ToInt32(command.ExecuteScalar());

                    return count > 0;
                }
            }
        }

        public static bool IsLoginAndPasswordCorrect(string login, string password)
        {
            using (SQLiteConnection connection = new SQLiteConnection(ConnectionString))
            {
                connection.Open();

                using (SQLiteCommand command = new SQLiteCommand(connection))
                {
                    command.CommandText = "SELECT COUNT(*) FROM Users WHERE Login = @login AND Password = @password";
                    command.Parameters.AddWithValue("@login", login);
                    command.Parameters.AddWithValue("@password", password);

                    int count = Convert.ToInt32(command.ExecuteScalar());

                    return count > 0;
                }
            }

        }

        public static int GetUserID(string login)
        {
            using (SQLiteConnection connection = new SQLiteConnection(ConnectionString))
            {
                connection.Open();
                using (SQLiteCommand command = new SQLiteCommand(connection))
                {
                    command.CommandText = "SELECT UserId FROM Users WHERE Login = @login";
                    command.Parameters.AddWithValue("@login", login);
                    object result = command.ExecuteScalar();
                    if (result != null && result != DBNull.Value)
                    {
                        return Convert.ToInt32(result);
                    }
                    return -1;
                }
            }
        }









        public static ObservableCollection<WeekPlan> LoadAllWeekPlans()
        {
            ObservableCollection<WeekPlan> weekPlans = new ObservableCollection<WeekPlan>();

            using (SQLiteConnection connection = new SQLiteConnection(ConnectionString))
            {
                connection.Open();

                using (SQLiteCommand command = new SQLiteCommand(connection))
                {
                    command.CommandText = "SELECT * FROM WeekPlans;";

                    using (SQLiteDataReader reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            WeekPlan weekPlan = new WeekPlan
                            {
                                WeekPlanId = Convert.ToInt32(reader["WeekPlanId"]),
                                Name = Convert.ToString(reader["Name"]),
                                CreatorID = Convert.ToInt32(reader["CreatorID"]),
                            };

                            weekPlan.Days = LoadDaysForWeekPlan(weekPlan.WeekPlanId);

                            weekPlans.Add(weekPlan);
                        }
                    }
                }
            }

            return weekPlans;
        }

        public static ObservableCollection<WeekPlan> LoadUserWeekPlans()
        {
            ObservableCollection<WeekPlan> userWeekPlans = new ObservableCollection<WeekPlan>();

            using (SQLiteConnection connection = new SQLiteConnection(ConnectionString))
            {
                connection.Open();

                using (SQLiteCommand command = new SQLiteCommand(connection))
                {
                    command.CommandText = @"
                    SELECT WeekPlans.* 
                    FROM WeekPlans
                    WHERE WeekPlans.CreatorID = @CreatorID;";

                    command.Parameters.AddWithValue("@CreatorID", Session.CurrentUser);

                    using (SQLiteDataReader reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            WeekPlan weekPlan = new WeekPlan
                            {
                                WeekPlanId = Convert.ToInt32(reader["WeekPlanId"]),
                                Name = Convert.ToString(reader["Name"]),
                                CreatorID = Convert.ToInt32(reader["CreatorID"]),
                            };

                            weekPlan.Days = LoadDaysForWeekPlan(weekPlan.WeekPlanId);

                            userWeekPlans.Add(weekPlan);
                        }
                    }
                }
            }

            return userWeekPlans;
        }

        private static List<Day> LoadDaysForWeekPlan(int weekPlanId)
        {
            List<Day> days = new List<Day>();

            using (SQLiteConnection connection = new SQLiteConnection(ConnectionString))
            {
                connection.Open();

                using (SQLiteCommand command = new SQLiteCommand(connection))
                {
                    command.CommandText = @"
                    SELECT Days.* 
                    FROM Days
                    JOIN WeekPlanDays ON Days.DayId = WeekPlanDays.DayId
                    WHERE WeekPlanDays.WeekPlanId = @WeekPlanId;";

                    command.Parameters.AddWithValue("@WeekPlanId", weekPlanId);

                    using (SQLiteDataReader reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            Day day = new Day
                            {
                                DayId = Convert.ToInt32(reader["DayId"]),
                                DayName = Convert.ToString(reader["DayName"]),
                            };

                            day.Recipes = LoadRecipesForDay(day.DayId);

                            days.Add(day);
                        }
                    }
                }
            }

            return days;
        }

        private static List<Recipe> LoadRecipesForDay(int dayId)
        {
            List<Recipe> recipes = new List<Recipe>();

            using (SQLiteConnection connection = new SQLiteConnection(ConnectionString))
            {
                connection.Open();

                using (SQLiteCommand command = new SQLiteCommand(connection))
                {
                    command.CommandText = @"
                    SELECT Recipes.* 
                    FROM Recipes
                    JOIN DayRecipes ON Recipes.RecipeId = DayRecipes.RecipeId
                    WHERE DayRecipes.DayId = @DayId;";

                    command.Parameters.AddWithValue("@DayId", dayId);

                    using (SQLiteDataReader reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            Recipe recipe = new Recipe
                            {
                                RecipeId = Convert.ToInt32(reader["RecipeId"]),
                                Name = Convert.ToString(reader["Name"]),
                                Description = Convert.ToString(reader["Description"]),
                                CreatorID = Convert.ToInt32(reader["CreatorID"]),
                            };

                            // Load Ingredients for the Recipe
                            recipe.Ingredients = LoadIngredientsForRecipe(recipe.RecipeId);

                            recipes.Add(recipe);
                        }
                    }
                }
            }

            return recipes;
        }

        private static List<Ingredient> LoadIngredientsForRecipe(int recipeId)
        {
            List<Ingredient> ingredients = new List<Ingredient>();

            using (SQLiteConnection connection = new SQLiteConnection(ConnectionString))
            {
                connection.Open();

                using (SQLiteCommand command = new SQLiteCommand(connection))
                {
                    command.CommandText = @"
                    SELECT Ingredients.* 
                    FROM Ingredients
                    JOIN RecipeIngredients ON Ingredients.IngredientId = RecipeIngredients.IngredientId
                    WHERE RecipeIngredients.RecipeId = @RecipeId;";

                    command.Parameters.AddWithValue("@RecipeId", recipeId);

                    using (SQLiteDataReader reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            Ingredient ingredient = new Ingredient
                            {
                                IngredientId = Convert.ToInt32(reader["IngredientId"]),
                                Name = Convert.ToString(reader["Name"]),
                                Weight = Convert.ToDecimal(reader["Weight"]),
                                FatW = Convert.ToDecimal(reader["FatW"]),
                                CarbW = Convert.ToDecimal(reader["CarbW"]),
                                ProteinW = Convert.ToDecimal(reader["ProteinW"]),
                            };

                            ingredients.Add(ingredient);
                        }
                    }
                }
            }

            return ingredients;
        }


        public static void CreateNewWeekPlan()
        {
            using (SQLiteConnection connection = new SQLiteConnection(ConnectionString))
            {
                connection.Open();

                using (SQLiteTransaction transaction = connection.BeginTransaction())
                {
                    try
                    {
                        using (SQLiteCommand command = new SQLiteCommand(connection))
                        {
                            command.CommandText = @"
                            INSERT INTO WeekPlans (Name, CreatorID)
                            VALUES (@Name, @CreatorID);";

                            command.Parameters.AddWithValue("@Name", "New Week PLan");
                            command.Parameters.AddWithValue("@CreatorID", Session.CurrentUser);

                            command.ExecuteNonQuery();
                        }

                        int weekPlanId;
                        using (SQLiteCommand command = new SQLiteCommand(connection))
                        {
                            command.CommandText = "SELECT last_insert_rowid();";
                            weekPlanId = Convert.ToInt32(command.ExecuteScalar());
                        }

                        for (int i = 0; i < 7; i++)
                        {
                            using (SQLiteCommand command = new SQLiteCommand(connection))
                            {
                                command.CommandText = @"
                                INSERT INTO Days (DayName)
                                VALUES (@DayName);";

                                command.Parameters.AddWithValue("@DayName", Enum.GetName(typeof(DayOfWeek), i));

                                command.ExecuteNonQuery();
                            }

                            int dayId;
                            using (SQLiteCommand command = new SQLiteCommand(connection))
                            {
                                command.CommandText = "SELECT last_insert_rowid();";
                                dayId = Convert.ToInt32(command.ExecuteScalar());
                            }

                            using (SQLiteCommand command = new SQLiteCommand(connection))
                            {
                                command.CommandText = @"
                                INSERT INTO WeekPlanDays (WeekPlanId, DayId)
                                VALUES (@WeekPlanId, @DayId);";

                                command.Parameters.AddWithValue("@WeekPlanId", weekPlanId);
                                command.Parameters.AddWithValue("@DayId", dayId);

                                command.ExecuteNonQuery();
                            }
                        }

                        transaction.Commit();
                    }
                    catch (Exception)
                    {
                        transaction.Rollback();
                        throw;
                    }
                }
            }
        }


        public static void RemoveUserWeekPlan(int weekPlanId, int currentUserId)
        {
            using (SQLiteConnection connection = new SQLiteConnection(ConnectionString))
            {
                connection.Open();

                using (SQLiteTransaction transaction = connection.BeginTransaction())
                {
                    try
                    {
                        if (!IsUserWeekPlan(weekPlanId, currentUserId))
                        {
                            throw new UnauthorizedAccessException("The user does not have permission to delete this WeekPlan.");
                        }

                        using (SQLiteCommand command = new SQLiteCommand(connection))
                        {
                            command.CommandText = "DELETE FROM WeekPlanDays WHERE WeekPlanId = @WeekPlanId;";
                            command.Parameters.AddWithValue("@WeekPlanId", weekPlanId);
                            command.ExecuteNonQuery();
                        }

                        using (SQLiteCommand command = new SQLiteCommand(connection))
                        {
                            command.CommandText = "DELETE FROM WeekPlans WHERE WeekPlanId = @WeekPlanId;";
                            command.Parameters.AddWithValue("@WeekPlanId", weekPlanId);
                            command.ExecuteNonQuery();
                        }

                        transaction.Commit();
                    }
                    catch (Exception)
                    {
                        transaction.Rollback();
                        throw;
                    }
                }
            }
        }

        private static bool IsUserWeekPlan(int weekPlanId, int currentUserId)
        {
            using (SQLiteConnection connection = new SQLiteConnection(ConnectionString))
            {
                connection.Open();

                using (SQLiteCommand command = new SQLiteCommand(connection))
                {
                    command.CommandText = @"
                    SELECT COUNT(*) 
                    FROM WeekPlans
                    WHERE WeekPlans.WeekPlanId = @WeekPlanId
                    AND WeekPlans.CreatorID = @CreatorID;";

                    command.Parameters.AddWithValue("@WeekPlanId", weekPlanId);
                    command.Parameters.AddWithValue("@CreatorID", currentUserId);

                    int count = Convert.ToInt32(command.ExecuteScalar());

                    return count > 0;
                }
            }
        }

    }
}
