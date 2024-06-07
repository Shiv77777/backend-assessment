using backend_assignment.Interfaces;
using backend_assignment.Models;
using Microsoft.Identity.Client;
using Npgsql;

namespace backend_assignment.Repositories
{
    public class DiscussionRepository : IDiscussionRepository
    {
        private string connectionString = "Host=localhost;Port=5432;Database=postgres;Username=postgres;Password=abc;";
        public async Task<string> Create(DiscussionModel post)
        {
            using (NpgsqlConnection con = new NpgsqlConnection(connectionString))
            {
                con.Open();
                string query = "INSERT INTO discussions (text_field, image, hash_tags, created_on, creator) VALUES (@val1, @val2, @val3, @val4, @val5)";
                using (NpgsqlCommand command = new NpgsqlCommand(query, con))
                {

                    command.Parameters.AddWithValue("@val1", post.Text);
                    command.Parameters.AddWithValue("@val2", post.Image);
                    command.Parameters.AddWithValue("@val3", post.HashTags);
                    command.Parameters.AddWithValue("@val4", post.CreatedOn);
                    command.Parameters.AddWithValue("@val5", post.Creator);


                    int rowsAffected = await command.ExecuteNonQueryAsync();
                }
            }
            return "post added successfully";
        }

        public async Task<string> Update(DiscussionModel post)
        {
            using (NpgsqlConnection con = new NpgsqlConnection(connectionString))
            {
                con.Open();
                string query = "UPDATE discussions SET text_field=@val1, image=@val2, hash_tags=@val3 where id=@val4";
                using (NpgsqlCommand command = new NpgsqlCommand(query, con))
                {
                    command.Parameters.AddWithValue("@val1", post.Text);
                    command.Parameters.AddWithValue("@val2", post.Image);
                    command.Parameters.AddWithValue("@val3", post.HashTags);
                    command.Parameters.AddWithValue("@val4", post.Id);

                    int rowsAffected = await command.ExecuteNonQueryAsync();
                }
            }
            return "post updated successfully";
        }

        public async Task<List<DiscussionModel>> SearchHt(string tags)
        {
            using (NpgsqlConnection con = new NpgsqlConnection(connectionString))
            {
                con.Open();
                string query = "SELECT * from discussions where hash_tags like @val1";
                using (NpgsqlCommand command = new NpgsqlCommand(query, con))
                {
                    string param = "%" + tags + "%";
                    command.Parameters.AddWithValue("@val1", param);
                    var reader = command.ExecuteReader();
                    var result = new List<DiscussionModel>();
                    while (reader.Read())
                    {
                        var discussion = new DiscussionModel
                        {
                            Id= reader.GetInt64(0),
                            Text= reader.GetString(1),
                            Image= reader.GetString(2),
                            HashTags= reader.GetString(3),
                            CreatedOn=reader.GetDateTime(4),
                            Creator=reader.GetString(5)
                        };
                        result.Add(discussion);
                    }
                    return result;
                }
            }
        }

        public async Task<List<DiscussionModel>> SearchText(string text)
        {
            using (NpgsqlConnection con = new NpgsqlConnection(connectionString))
            {
                con.Open();
                string query = "SELECT * from discussions where text_field like @val1";
                using (NpgsqlCommand command = new NpgsqlCommand(query, con))
                {
                    string param = "%" + text + "%";
                    command.Parameters.AddWithValue("@val1", param);
                    var reader = command.ExecuteReader();
                    var result = new List<DiscussionModel>();
                    while (reader.Read())
                    {
                        var discussion = new DiscussionModel
                        {
                            Id = reader.GetInt64(0),
                            Text = reader.GetString(1),
                            Image = reader.GetString(2),
                            HashTags = reader.GetString(3),
                            CreatedOn = reader.GetDateTime(4),
                            Creator = reader.GetString(5)
                        };
                        result.Add(discussion);
                    }
                    return result;
                }
            }
        }
    }
}
