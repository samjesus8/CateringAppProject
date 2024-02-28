using Npgsql;

namespace App.Database
{
    public class AppDatabaseEngine
    {
        private string ConnectionString = "Host=byhjoscxxfeyki7vts5z-postgresql.services.clever-cloud.com:50013;Username=u8vonutic9opav9ir8zc;Password=PY8P8MwvMrYCy6YWTX7D3tb85limQR;Database=byhjoscxxfeyki7vts5z";

        public bool CreateUser(User user)
        {
            using (var conn = new NpgsqlConnection(ConnectionString))
            {
                conn.Open();

                using (var cmd = new NpgsqlCommand())
                {
                    cmd.Connection = conn;
                    cmd.CommandText = "INSERT INTO cateringapp.user (userid, username, password) VALUES (@userid, @username, @password)";
                    cmd.Parameters.AddWithValue("userid", user.UserID);
                    cmd.Parameters.AddWithValue("username", user.Username);
                    cmd.Parameters.AddWithValue("password", user.Password);

                    try
                    {
                        cmd.ExecuteNonQuery();
                        return true;
                    }
                    catch (Exception ex)
                    {
                        Console.WriteLine($"Error: {ex.Message}");
                        return false;
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
                        cmd.CommandText = "SELECT userid, username, password FROM cateringapp.user WHERE username = @username";
                        cmd.Parameters.AddWithValue("username", username);

                        using (var reader = cmd.ExecuteReader())
                        {
                            if (reader.Read())
                            {
                                user = new User()
                                {
                                    Username = reader.GetString(reader.GetOrdinal("username")),
                                    Password = reader.GetString(reader.GetOrdinal("password")),
                                    UserID = reader.GetInt64(reader.GetOrdinal("userid"))
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
    }
}
