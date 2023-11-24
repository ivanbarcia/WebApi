using System.Data.SqlClient;
using WebApi.Contracts;
using WebApi.Model;

namespace WebApi.Context;

public class DataContext : IDataContext
{
    private readonly string _connectionString;

    public DataContext(string connectionString)
    {
        _connectionString = connectionString ?? throw new ArgumentNullException(nameof(connectionString));
    }

    public async Task SaveChanges()
    {
        await using (SqlConnection connection = new SqlConnection(_connectionString))
        {
            connection.Open();

            await using (SqlCommand command = new SqlCommand("COMMIT", connection))
            {
                command.ExecuteNonQuery();
            }
        }
    }

    #region Users
    public async Task AddUser(User newUser)
    {
        await using (SqlConnection connection = new SqlConnection(_connectionString))
        {
            connection.Open();

            // You should replace "Users" with the actual name of your user table
            string query = "INSERT INTO [dbo].[user] (Username, Password) VALUES (@Username, @Password)";

            await using (SqlCommand command = new SqlCommand(query, connection))
            {
                // Use parameters to prevent SQL injection
                command.Parameters.AddWithValue("@Username", newUser.UserName);
                command.Parameters.AddWithValue("@Password", newUser.Password);

                // Execute the query
                command.ExecuteNonQuery();
            }
        }
    }

    public async Task UpdateUser(User user)
    {
        await using (SqlConnection connection = new SqlConnection(_connectionString))
        {
            connection.Open();

            // You should replace "Users" with the actual name of your user table
            string query = "UPDATE [dbo].[user] (Username, Password) VALUES (@Username, @Password)";

            await using (SqlCommand command = new SqlCommand(query, connection))
            {
                // Use parameters to prevent SQL injection
                command.Parameters.AddWithValue("@Username", user.UserName);
                command.Parameters.AddWithValue("@Password", user.Password);

                // Execute the query
                command.ExecuteNonQuery();
            }
        }
    }

    public async Task RemoveUser(int userId)
    {
        await using (SqlConnection connection = new SqlConnection(_connectionString))
        {
            connection.Open();

            // You should replace "Users" with the actual name of your user table
            string query = "DELETE [dbo].[user] WHERE Id = @Id";

            await using (SqlCommand command = new SqlCommand(query, connection))
            {
                // Use parameters to prevent SQL injection
                command.Parameters.AddWithValue("@Id", userId);

                // Execute the query
                command.ExecuteNonQuery();
            }
        }
    }

    public async Task<User?> GetUser(string username)
    {
        using (SqlConnection connection = new SqlConnection(_connectionString))
        {
            connection.Open();

            using (SqlCommand command = new SqlCommand("SELECT * FROM [dbo].[user] WHERE username = @Username", connection))
            {
                // Add parameters
                command.Parameters.AddWithValue("@Username", username);

                using (SqlDataReader reader = command.ExecuteReader())
                {
                    List<User> entities = new List<User>();

                    while (reader.Read())
                    {
                        User entity = new User
                        {
                            Id = reader.GetInt32(reader.GetOrdinal("Id")),
                            UserName = reader.GetString(reader.GetOrdinal("username"))
                        };

                        entities.Add(entity);
                    }

                    return entities.SingleOrDefault();
                }
            }
        }
    }
    #endregion

    #region Auth
    public async Task<bool> CheckPassword(User user, string password)
    {
        using (SqlConnection connection = new SqlConnection(_connectionString))
        {
            connection.Open();

            using (SqlCommand command = new SqlCommand("SELECT * FROM [dbo].[user] WHERE username = @Username AND password = @Password", connection))
            {
                // Add parameters
                command.Parameters.AddWithValue("@Username", user.UserName);
                command.Parameters.AddWithValue("@Password", password);

                using (SqlDataReader reader = command.ExecuteReader())
                {
                    return reader.HasRows;
                }
            }
        }
    }
    #endregion
}