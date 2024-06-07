using backend_assignment.Interfaces;
using backend_assignment.Models;
using Microsoft.Data.SqlClient;
using Microsoft.IdentityModel.Tokens;
using Npgsql;

namespace backend_assignment.Repositories
{
    public class UserRepository : IUserRepository
    {
        private string connectionString = "Host=localhost;Port=5432;Database=postgres;Username=postgres;Password=abc;";
        public async Task<string> SignUp(UserModel user)
        {
            using (NpgsqlConnection con = new NpgsqlConnection(connectionString))
            {
                con.Open();
                string query = "INSERT INTO user_details (name, email, mobile, password) VALUES (@val1, @val2, @val3, @val4)";
                using (NpgsqlCommand command = new NpgsqlCommand(query, con))
                {
                    command.Parameters.AddWithValue("@val1", user.Name);
                    command.Parameters.AddWithValue("@val2", user.EmailId);
                    command.Parameters.AddWithValue("@val3", user.MobileNo);
                    command.Parameters.AddWithValue("@val4", user.Secret);

                    int rowsAffected = await command.ExecuteNonQueryAsync();
                }
            }
            return "user added successfully";
        }

        public async Task<string> Login(UserModel user)
        {
            using (NpgsqlConnection con = new NpgsqlConnection(connectionString))
            {
                con.Open();
                string query = "SELECT count(*) from user_details where email=@val1 and password=@val2";
                using (NpgsqlCommand command = new NpgsqlCommand(query, con))
                {
                    
                    command.Parameters.AddWithValue("@val1", user.EmailId);
                    command.Parameters.AddWithValue("@val2", user.Secret);
                    var reader = command.ExecuteReader();
                    int count = 0;
                    while (reader.Read())
                    {
                        count = reader.GetInt32(0);
                    }
                    if (count == 1)
                    {
                        return "Login Successful";
                    }
                    else
                    {
                        return "Invalid login";
                    }
                }
            }
        }

        public async Task<string> DeleteUser(string email)
        {
            using (NpgsqlConnection con = new NpgsqlConnection(connectionString))
            {
                con.Open();
                string query = "DELETE from user_details where email=@val1";
                using (NpgsqlCommand command = new NpgsqlCommand(query, con))
                {
                    
                    command.Parameters.AddWithValue("@val1", email);


                    int rowsAffected = command.ExecuteNonQuery();
                }
            }
            return "Delete Successful";
        }

        public async Task<List<UserModel>> SearchUser(string prompt, bool isGetAll)
        {
            if (prompt.IsNullOrEmpty() && !isGetAll)
            {
                return new List<UserModel>();
            }
            using (NpgsqlConnection con = new NpgsqlConnection(connectionString))
            {
                con.Open();
                string selectQuery = "SELECT * from user_details where name like @val1";
                using (NpgsqlCommand command = new NpgsqlCommand(selectQuery, con))
                {
                    string param = "%" + prompt + "%";
                    command.Parameters.AddWithValue("@val1", param);
                    var reader = command.ExecuteReader();
                    var result = new List<UserModel>();
                    while (reader.Read())
                    {
                        var userLogin = new UserModel
                        {
                            Name = reader.GetString(1),
                            EmailId = reader.GetString(2),
                            MobileNo = reader.GetString(3)
                        };
                        result.Add(userLogin);
                    }
                    return result;
                }
            }
        }

        public async Task<string> AddFollower(string account, string follower)
        {
            using (NpgsqlConnection con = new NpgsqlConnection(connectionString))
            {
                con.Open();
                string query = "SELECT * from user_details where email like @val1";
                long acc = 0;
                long foll = 0;
                using (NpgsqlCommand command = new NpgsqlCommand(query, con))
                {
                    
                    command.Parameters.AddWithValue("@val1", account);
                    var reader = command.ExecuteReader();
                    reader.Read();
                    acc = reader.GetInt64(0);
                    reader.Close();
                }

                using (NpgsqlCommand command = new NpgsqlCommand(query, con))
                {
                    
                    command.Parameters.AddWithValue("@val1", follower);
                    var reader = command.ExecuteReader();
                    reader.Read();
                    foll = reader.GetInt64(0);
                    reader.Close();
                }

                query = "INSERT into following (account, follower) values (@val1, @val2)";
                using (NpgsqlCommand command = new NpgsqlCommand(query, con))
                {
                    
                    command.Parameters.AddWithValue("@val1", acc);
                    command.Parameters.AddWithValue("@val2", foll);
                    await command.ExecuteNonQueryAsync();
                }
            }
            return "Followed successfully";
        }

        public async Task<string> UpdateUser(UserModel user)
        {
            using (NpgsqlConnection con = new NpgsqlConnection(connectionString))
            {
                con.Open();
                string query = "UPDATE user_details SET  name=@val1, mobile=@val2 where email=@val3";
                using (NpgsqlCommand command = new NpgsqlCommand(query, con))
                {
                    command.Parameters.AddWithValue("@val1", user.Name);
                    command.Parameters.AddWithValue("@val2", user.MobileNo);
                    command.Parameters.AddWithValue("@val3", user.EmailId);

                    int rowsAffected = await command.ExecuteNonQueryAsync();
                }
            }
            return "User updated successfully";
        }
    }
}
