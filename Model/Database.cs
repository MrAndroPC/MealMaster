using System.Data.SQLite;
namespace MealMaster.Model
{
    public static class Database
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
    }
}
