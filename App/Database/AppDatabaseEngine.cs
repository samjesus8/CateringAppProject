using Npgsql;

namespace App.Database
{
    public class AppDatabaseEngine
    {
        private string ConnectionString = "Host=byhjoscxxfeyki7vts5z-postgresql.services.clever-cloud.com:50013;Username=u8vonutic9opav9ir8zc;Password=PY8P8MwvMrYCy6YWTX7D3tb85limQR;Database=byhjoscxxfeyki7vts5z";

        public (bool, string) CreateUser(User user)
        {
            using (var conn = new NpgsqlConnection(ConnectionString))
            {
                conn.Open();

                using (var cmd = new NpgsqlCommand())
                {
                    cmd.Connection = conn;
                    cmd.CommandText = "INSERT INTO cateringapp.user (userid, username, password, usertype) VALUES (@userid, @username, @password, @usertype)";

                    //SQL Parameters
                    cmd.Parameters.AddWithValue("userid", user.UserID);
                    cmd.Parameters.AddWithValue("username", user.Username);
                    cmd.Parameters.AddWithValue("password", user.Password);
                    cmd.Parameters.AddWithValue("usertype", "normal");

                    try
                    {
                        cmd.ExecuteNonQuery();
                        return (true, string.Empty);
                    }
                    catch (Exception ex)
                    {
                        Console.WriteLine($"Error: {ex.Message}");
                        return (false, ex.ToString());
                    }
                }
            }
        }

        public (bool, User) GetUser(string username)
        {
            User user = null;
            try
            {
                using (var conn = new NpgsqlConnection(ConnectionString))
                {
                    conn.Open();

                    using (var cmd = new NpgsqlCommand())
                    {
                        cmd.Connection = conn;
                        cmd.CommandText = "SELECT userid, username, password, usertype FROM cateringapp.user WHERE username = @username";

                        //SQL Parameters
                        cmd.Parameters.AddWithValue("username", username);

                        using (var reader = cmd.ExecuteReader())
                        {
                            if (reader.Read())
                            {
                                user = new User()
                                {
                                    Username = reader.GetString(reader.GetOrdinal("username")),
                                    Password = reader.GetString(reader.GetOrdinal("password")),
                                    UserID = reader.GetInt64(reader.GetOrdinal("userid")),
                                    UserType = reader.GetString(reader.GetOrdinal("usertype"))
                                };
                            }
                        }
                    }
                }

                return (true, user);
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error: {ex.Message}");
                return (false, null);
            }
        }

        public (bool, string) StoreFoodItem(FoodItem item)
        {
            using (var conn = new NpgsqlConnection(ConnectionString))
            {
                conn.Open();

                using (var cmd = new NpgsqlCommand())
                {
                    cmd.Connection = conn;
                    cmd.CommandText = "INSERT INTO cateringapp.fooditems (itemid, name, description, price, imageurl) VALUES (@itemid, @name, @description, @price, @imageurl)";

                    //SQL Parameters
                    cmd.Parameters.AddWithValue("itemid", item.ItemID);
                    cmd.Parameters.AddWithValue("name", item.Name);
                    cmd.Parameters.AddWithValue("description", item.Description);
                    cmd.Parameters.AddWithValue("price", item.Price);
                    cmd.Parameters.AddWithValue("imageurl", item.FoodItemImageURL);

                    try
                    {
                        cmd.ExecuteNonQuery();
                        return (true, string.Empty);
                    }
                    catch (Exception ex)
                    {
                        Console.WriteLine($"Error: {ex.Message}");
                        return (false, ex.ToString());
                    }
                }
            }
        }

        public (bool, List<FoodItem>) GetAllFoodItems()
        {
            List<FoodItem> foodItems = new List<FoodItem>();

            try
            {
                using (var conn = new NpgsqlConnection(ConnectionString))
                {
                    conn.Open();

                    using (var cmd = new NpgsqlCommand())
                    {
                        cmd.Connection = conn;
                        cmd.CommandText = "SELECT itemid, name, description, price, imageurl FROM cateringapp.fooditems";

                        using (var reader = cmd.ExecuteReader())
                        {
                            while (reader.Read())
                            {
                                FoodItem foodItem = new()
                                {
                                    ItemID = reader.GetInt64(reader.GetOrdinal("itemid")),
                                    Name = reader.GetString(reader.GetOrdinal("name")),
                                    Description = reader.GetString(reader.GetOrdinal("description")),
                                    Price = reader.GetDouble(reader.GetOrdinal("price")),
                                    FoodItemImageURL = reader.GetString(reader.GetOrdinal("imageurl"))
                                };

                                foodItems.Add(foodItem);
                            }
                        }
                    }
                }

                return (true, foodItems);
            }

            catch (Exception ex)
            {
                Console.WriteLine($"Error: {ex.Message}");
                return (false, null);
            }
        }

        public (bool, string) ModifyItem(FoodItem item)
        {
            using (var conn = new NpgsqlConnection(ConnectionString))
            {
                conn.Open();

                using (var cmd = new NpgsqlCommand())
                {
                    cmd.Connection = conn;
                    cmd.CommandText = "UPDATE cateringapp.fooditems SET name = @name, description = @description, price = @price, imageurl = @imageurl WHERE itemid = @itemid";

                    //SQL Parameters
                    cmd.Parameters.AddWithValue("name", item.Name);
                    cmd.Parameters.AddWithValue("description", item.Description);
                    cmd.Parameters.AddWithValue("price", item.Price);
                    cmd.Parameters.AddWithValue("imageurl", item.FoodItemImageURL);
                    cmd.Parameters.AddWithValue("itemid", item.ItemID);

                    try
                    {
                        cmd.ExecuteNonQuery();
                        return (true, string.Empty);
                    }
                    catch (Exception ex)
                    {
                        Console.WriteLine($"Error: {ex.Message}");
                        return (false, ex.ToString());
                    }
                }
            }
        }
    }
}
