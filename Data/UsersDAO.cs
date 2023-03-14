using HireMe.Models;
using System.Data.SqlClient;

namespace HireMe.Services
{
    public class UsersDAO
    {
        public bool FindUser(UserModel user)
        {
            bool success = false;
            string sqlStatement = "SELECT * FROM dbo.Users WHERE Username = @username AND Password = @password";
            using (SqlConnection connection = new SqlConnection(GlobalVariables.connectionString))
            {
                SqlCommand cmd = new SqlCommand(sqlStatement, connection);
                cmd.Parameters.Add("@username", System.Data.SqlDbType.NVarChar, 50).Value = user.Username;
                cmd.Parameters.Add("@password", System.Data.SqlDbType.NVarChar, 50).Value = user.Password;
                try
                {
                    connection.Open();
                    SqlDataReader reader = cmd.ExecuteReader();
                    if (reader.HasRows)
                    {
                        success = true;
                    }
                }

                catch (Exception ex)
                {
                    Console.WriteLine(ex.Message);
                }
            }
            return success;
        }

        public bool CheckIfExists(ExtendedUserModel user)
        {
            bool success = false;
            string sqlStatement = "SELECT * FROM dbo.Users WHERE Email = @email";
            using (SqlConnection connection = new SqlConnection(GlobalVariables.connectionString))
            {
                SqlCommand cmd = new SqlCommand(sqlStatement, connection);
                cmd.Parameters.Add("@email", System.Data.SqlDbType.NVarChar, 50).Value = user.Email;
                try
                {
                    connection.Open();
                    SqlDataReader reader = cmd.ExecuteReader();
                    if (reader.HasRows)
                    {
                        success = true;
                    }
                }

                catch (Exception ex)
                {
                    Console.WriteLine(ex.Message);
                }
            }
            return success;
        }

        public bool CheckLoginExisting(ExtendedUserModel user)
        {
            bool success = false;
            string sqlStatement = "SELECT * FROM dbo.Users WHERE username = @username";
            using (SqlConnection connection = new SqlConnection(GlobalVariables.connectionString))
            {
                SqlCommand cmd = new SqlCommand(sqlStatement, connection);
                cmd.Parameters.Add("@username", System.Data.SqlDbType.NVarChar, 50).Value = user.Username;
                try
                {
                    connection.Open();
                    SqlDataReader reader = cmd.ExecuteReader();
                    if (reader.HasRows)
                    {
                        success = true;
                    }
                }

                catch (Exception ex)
                {
                    Console.WriteLine(ex.Message);
                }
            }
            return success;
        }

        public void CreateUser(ExtendedUserModel user)
        {
            string sqlStatement = "INSERT INTO Users(FirstName, LastName, Username, Email, Password) VALUES (@firstname, @lastname, @username,@email,@password)";
            using (SqlConnection connection = new SqlConnection(GlobalVariables.connectionString))
            {
                SqlCommand cmd = new SqlCommand(sqlStatement, connection);
                cmd.Parameters.Add("@firstname", System.Data.SqlDbType.NVarChar, 50).Value = user.FirstName;
                cmd.Parameters.Add("@lastname", System.Data.SqlDbType.NVarChar, 50).Value = user.LastName;
                cmd.Parameters.Add("@username", System.Data.SqlDbType.NVarChar, 50).Value = user.Username;
                cmd.Parameters.Add("@email", System.Data.SqlDbType.NVarChar, 50).Value = user.Email;
                cmd.Parameters.Add("@password", System.Data.SqlDbType.NVarChar, 50).Value = user.Password;
                try
                {
                    connection.Open();
                    cmd.ExecuteNonQuery();
                }

                catch (Exception ex)
                {
                    Console.WriteLine(ex.Message);
                }
            }
        }

        public int GetUserID(UserModel user)
        {
            string sqlStatement = "SELECT * FROM dbo.Users WHERE Username = @username";
            int userID= 0;
            using (SqlConnection connection = new SqlConnection(GlobalVariables.connectionString))
            {
                SqlCommand cmd = new SqlCommand(sqlStatement, connection);
                cmd.Parameters.Add("@username", System.Data.SqlDbType.NVarChar, 50).Value = user.Username;
                try
                {
                    connection.Open();
                    SqlDataReader reader = cmd.ExecuteReader();
                    if (reader.Read())
                    {
                        userID = reader.GetInt32(0);
                    }
                }

                catch (Exception ex)
                {
                    Console.WriteLine(ex.Message);
                }
            }
            return userID;
        }

        public string GetUserFullName(int id)
        {
            string fullname = "";
            string sqlStatement = "SELECT CONCAT(FirstName, ' ', LastName) FROM dbo.Users WHERE UserID = @id";
            using (SqlConnection connection = new SqlConnection(GlobalVariables.connectionString))
            {
                SqlCommand cmd = new SqlCommand(sqlStatement, connection);
                cmd.Parameters.Add("@id", System.Data.SqlDbType.Int).Value = id;
                try
                {
                    connection.Open();
                    SqlDataReader reader = cmd.ExecuteReader();
                    if (reader.Read())
                    {
                        fullname = reader.GetString(0);
                    }
                }

                catch (Exception ex)
                {
                    Console.WriteLine(ex.Message);
                }
            }
            return fullname;
        }        
        }
    }

