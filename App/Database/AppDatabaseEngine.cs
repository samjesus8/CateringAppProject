using Npgsql;

namespace App.Database
{
    public class AppDatabaseEngine
    {
        private string ConnectionString = "Host=byhjoscxxfeyki7vts5z-postgresql.services.clever-cloud.com:50013;Username=u8vonutic9opav9ir8zc;Password=PY8P8MwvMrYCy6YWTX7D3tb85limQR;Database=byhjoscxxfeyki7vts5z";

        public async Task<(bool, string)> CreateUserAsync(User user, bool isAdmin)
        {
            using (var conn = new NpgsqlConnection(ConnectionString))
            {
                await conn.OpenAsync();

                using (var cmd = new NpgsqlCommand())
                {
                    cmd.Connection = conn;
                    cmd.CommandText = "INSERT INTO cateringapp.user (userid, username, password, usertype) VALUES (@userid, @username, @password, @usertype)";

                    //SQL Parameters
                    cmd.Parameters.AddWithValue("userid", user.UserID);
                    cmd.Parameters.AddWithValue("username", user.Username);
                    cmd.Parameters.AddWithValue("password", user.Password);

                    if (isAdmin == true)
                    {
                        cmd.Parameters.AddWithValue("usertype", "admin");
                    }
                    else
                    {
                        cmd.Parameters.AddWithValue("usertype", "normal");
                    }

                    try
                    {
                        await cmd.ExecuteNonQueryAsync();
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

        public async Task<(bool, string)> CheckUserExistsAsync(string username)
        {
            try
            {
                using (var conn = new NpgsqlConnection(ConnectionString))
                {
                    await conn.OpenAsync();

                    using (var cmd = new NpgsqlCommand())
                    {
                        cmd.Connection = conn;
                        cmd.CommandText = "SELECT EXISTS (SELECT 1 FROM cateringapp.user WHERE username = @username LIMIT 1);";
                        cmd.Parameters.AddWithValue("username", username);

                        bool userExists = (bool)await cmd.ExecuteScalarAsync();

                        if (userExists)
                        {
                            return (true, String.Empty);
                        }
                        else
                        {
                            return (false, String.Empty);
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                return (false, ex.ToString());
            }
        }

        public async Task<(bool, User)> GetUserAsync(string username)
        {
            User user = null;
            try
            {
                using (var conn = new NpgsqlConnection(ConnectionString))
                {
                    await conn.OpenAsync();

                    using (var cmd = new NpgsqlCommand())
                    {
                        cmd.Connection = conn;
                        cmd.CommandText = "SELECT userid, username, password, usertype FROM cateringapp.user WHERE username = @username";

                        //SQL Parameters
                        cmd.Parameters.AddWithValue("username", username);

                        using (var reader = await cmd.ExecuteReaderAsync())
                        {
                            if (await reader.ReadAsync())
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

        public async Task<(bool, List<User>)> GetAllAdminUsersAsync()
        {
            List<User> adminUsers = new List<User>();

            try
            {
                using (var conn = new NpgsqlConnection(ConnectionString))
                {
                    await conn.OpenAsync();

                    using (var cmd = new NpgsqlCommand())
                    {
                        cmd.Connection = conn;
                        cmd.CommandText = "SELECT userid, username, password, usertype FROM cateringapp.user WHERE usertype = 'admin'";

                        using (var reader = await cmd.ExecuteReaderAsync())
                        {
                            while (await reader.ReadAsync())
                            {
                                adminUsers.Add(new User()
                                {
                                    Username = reader.GetString(reader.GetOrdinal("username")),
                                    Password = reader.GetString(reader.GetOrdinal("password")),
                                    UserID = reader.GetInt64(reader.GetOrdinal("userid")),
                                    UserType = reader.GetString(reader.GetOrdinal("usertype"))
                                });
                            }
                        }
                    }
                }

                return (true, adminUsers);
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error: {ex.Message}");
                return (false, null);
            }
        }

        public async Task<(bool, string)> StoreFoodItemAsync(FoodItem item)
        {
            using (var conn = new NpgsqlConnection(ConnectionString))
            {
                await conn.OpenAsync();

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
                        await cmd.ExecuteNonQueryAsync();
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

        public async Task<(bool, List<FoodItem>)> GetAllFoodItemsAsync()
        {
            List<FoodItem> foodItems = new List<FoodItem>();

            try
            {
                using (var conn = new NpgsqlConnection(ConnectionString))
                {
                    await conn.OpenAsync();

                    using (var cmd = new NpgsqlCommand())
                    {
                        cmd.Connection = conn;
                        cmd.CommandText = "SELECT itemid, name, description, price, imageurl FROM cateringapp.fooditems";

                        using (var reader = await cmd.ExecuteReaderAsync())
                        {
                            while (await reader.ReadAsync())
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

        public async Task<(bool, string)> ModifyItemAsync(FoodItem item)
        {
            using (var conn = new NpgsqlConnection(ConnectionString))
            {
                await conn.OpenAsync();

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
                        await cmd.ExecuteNonQueryAsync();
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

        public async Task<(bool, string)> ModifyUserAsync(User user)
        {
            try
            {
                using (var conn = new NpgsqlConnection(ConnectionString))
                {
                    await conn.OpenAsync();

                    using (var cmd = new NpgsqlCommand())
                    {
                        cmd.Connection = conn;
                        cmd.CommandText = "UPDATE cateringapp.user SET username = @username, password = @password WHERE userid = @userid";

                        //SQL Parameters
                        cmd.Parameters.AddWithValue("username", user.Username);
                        cmd.Parameters.AddWithValue("password", user.Password);
                        cmd.Parameters.AddWithValue("userid", user.UserID);

                        await cmd.ExecuteNonQueryAsync();
                        return (true, string.Empty);
                    }
                }
            }
            catch (Exception ex)
            {
                return (false, ex.ToString());
            }
        }
    }
}
