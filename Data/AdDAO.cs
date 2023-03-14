using HireMe.Models;
using System.Data.SqlClient;

namespace HireMe.Data
{
    public class AdDAO
    {
        public void CreateCAd(JobadModel jobad, int id)
        {
            string sqlStatement = "INSERT INTO CompanyAds(OwnerID, Industry, CompanyName, Title, SalaryMin, SalaryMax, Content) VALUES (@owner, @ind, @com, @title, @salarymin, @salarymax, @content)";
            using (SqlConnection connection = new SqlConnection(GlobalVariables.connectionString))
            {
                SqlCommand cmd = new SqlCommand(sqlStatement, connection);
                cmd.Parameters.Add("@owner", System.Data.SqlDbType.Int).Value = id;
                cmd.Parameters.Add("@ind", System.Data.SqlDbType.NVarChar, 50).Value = jobad.Industry;
                cmd.Parameters.Add("@com", System.Data.SqlDbType.NVarChar, 50).Value = jobad.CompanyName;
                cmd.Parameters.Add("@title", System.Data.SqlDbType.NVarChar, 50).Value = jobad.Title;
                cmd.Parameters.Add("@salarymin", System.Data.SqlDbType.NVarChar, 30).Value = jobad.SalaryMin;
                cmd.Parameters.Add("@salarymax", System.Data.SqlDbType.NVarChar, 30).Value = jobad.SalaryMax;
                cmd.Parameters.Add("@content", System.Data.SqlDbType.Text).Value = jobad.Content;
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

        public void CreateEAd(CandidateAdModel canad, int id)
        {
            string sqlStatement = "INSERT INTO CandidateAds(OwnerID, Industry, Title, SalaryMin, SalaryMax, Content) VALUES (@owner, @ind, @title, @salarymin, @salarymax, @content)";
            using (SqlConnection connection = new SqlConnection(GlobalVariables.connectionString))
            {
                SqlCommand cmd = new SqlCommand(sqlStatement, connection);
                cmd.Parameters.Add("@owner", System.Data.SqlDbType.Int).Value = id;
                cmd.Parameters.Add("@ind", System.Data.SqlDbType.NVarChar, 50).Value = canad.Industry;
                cmd.Parameters.Add("@title", System.Data.SqlDbType.NVarChar, 50).Value = canad.Title;
                cmd.Parameters.Add("@salarymin", System.Data.SqlDbType.NVarChar, 30).Value = canad.SalaryMin;
                cmd.Parameters.Add("@salarymax", System.Data.SqlDbType.NVarChar, 30).Value = canad.SalaryMax;
                cmd.Parameters.Add("@content", System.Data.SqlDbType.NVarChar, 10).Value = canad.Content;
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

        public void CreateEAdFromFile(List<CandidateAdModel> model)
        {
            foreach (var canad in model)
            {
                string sqlStatement = "INSERT INTO CandidateAds(OwnerID, Industry, Title, SalaryMin, SalaryMax, Content) VALUES (@owner, @ind, @title, @salarymin, @salarymax, @content)";

                using (SqlConnection connection = new SqlConnection(GlobalVariables.connectionString))
                {
                    SqlCommand cmd = new SqlCommand(sqlStatement, connection);
                    cmd.Parameters.Add("@owner", System.Data.SqlDbType.Int).Value = canad.Owner;
                    cmd.Parameters.Add("@ind", System.Data.SqlDbType.NVarChar, 50).Value = canad.Industry;
                    cmd.Parameters.Add("@title", System.Data.SqlDbType.NVarChar, 50).Value = canad.Title;
                    cmd.Parameters.Add("@salarymin", System.Data.SqlDbType.NVarChar, 30).Value = canad.SalaryMin;
                    cmd.Parameters.Add("@salarymax", System.Data.SqlDbType.NVarChar, 30).Value = canad.SalaryMax;
                    cmd.Parameters.Add("@content", System.Data.SqlDbType.NVarChar, 10).Value = canad.Content;
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
        }

        public List<SearchingResult> FetchJobOffers(SearchedItem newItem)
        {
            List<SearchingResult> returnList = new List<SearchingResult>();
            KeywordModel keyword = new KeywordModel();
            List<string> keywordsList = new List<string>();
            keywordsList = keyword.KeywordSplit(newItem.Keywords);
            
            foreach (string keywordString in keywordsList)
            {
                string keyTemp = keywordString;
                using (SqlConnection connection = new SqlConnection(GlobalVariables.connectionString))
                {
                    string sqlStatement = "SELECT OwnerID, Industry, Title, CONCAT(SalaryMin, '-', SalaryMax), Content, AdID, CompanyName  FROM CompanyAds WHERE Industry = @ind AND SalaryMin >= @smin AND SalaryMax <= @smax AND (Title LIKE '%" + keywordString + "%' OR Content LIKE '%" + keywordString + "%')";

                    SqlCommand cmd = new SqlCommand(sqlStatement, connection);
                    cmd.Parameters.Add("@ind", System.Data.SqlDbType.NVarChar, 50).Value = newItem.Industry;
                    cmd.Parameters.Add("@smin", System.Data.SqlDbType.Int).Value = newItem.SalaryMin;
                    cmd.Parameters.Add("@smax", System.Data.SqlDbType.Int).Value = newItem.SalaryMax;
                    cmd.Parameters.Add("@key", System.Data.SqlDbType.NVarChar, 10).Value = keywordString;
                    try
                    {
                        connection.Open();
                        SqlDataReader reader = cmd.ExecuteReader();
                        if (reader.HasRows)
                        {
                            while (reader.Read())
                            {
                                SearchingResult result = new SearchingResult();

                                result.OrganizerID = reader.GetInt32(0);
                                result.Industry = reader.GetString(1);
                                result.Title = reader.GetString(2);
                                result.Salary = reader.GetString(3);
                                result.Content = reader.GetString(4);
                                result.AdID = reader.GetInt32(5);
                                result.CompanyName = reader.GetString(6);
                                int index = returnList.FindIndex(item => item.AdID == result.AdID);
                                if(index >= 0 || result.OrganizerID == GlobalVariables.GlobaluserID)
                                {
                                    continue;
                                }
                                returnList.Add(result);
                            }
                        }
                    }
                    catch (Exception ex)
                    {
                        Console.WriteLine(ex.Message);
                    }
                }
            }
            return returnList;
        }

        public List<SearchingResult> FetchAllJobOffers(SearchedItem newItem)
        {     
            List<SearchingResult> returnList = new List<SearchingResult>();
                using (SqlConnection connection = new SqlConnection(GlobalVariables.connectionString))
                {
                    string sqlStatement = "SELECT OwnerID, Industry, Title, CONCAT(SalaryMin, '-', SalaryMax), Content, AdID, CompanyName  FROM CompanyAds";

                    SqlCommand cmd = new SqlCommand(sqlStatement, connection);
                    try
                    {
                        connection.Open();
                        SqlDataReader reader = cmd.ExecuteReader();
                        if (reader.HasRows)
                        {
                            while (reader.Read())
                            {
                                SearchingResult result = new SearchingResult();

                                result.OrganizerID = reader.GetInt32(0);
                                result.Industry = reader.GetString(1);
                                result.Title = reader.GetString(2);
                                result.Salary = reader.GetString(3);
                                result.Content = reader.GetString(4);
                                result.AdID = reader.GetInt32(5);
                                result.CompanyName = reader.GetString(6);
                                int index = returnList.FindIndex(item => item.AdID == result.AdID);
                                if (index >= 0 || result.OrganizerID == GlobalVariables.GlobaluserID)
                                {
                                    continue;
                                }
                                returnList.Add(result);
                            }
                        }
                    }
                    catch (Exception ex)
                    {
                        Console.WriteLine(ex.Message);
                    }
                }
            return returnList;
        }

        public List<SearchingResult> FetchCanOffers(SearchedItem newItem)
        {
            KeywordModel keyword = new KeywordModel();
            List<string> keywordsList = new List<string>();
            keywordsList = keyword.KeywordSplit(newItem.Keywords);
            List<SearchingResult> returnList = new List<SearchingResult>();
            foreach (string keywordString in keywordsList)
            {
                string keyTemp = keywordString;
                using (SqlConnection connection = new SqlConnection(GlobalVariables.connectionString))
                {
                    string sqlStatement = "SELECT OwnerID, Industry, Title, CONCAT(SalaryMin, '-', SalaryMax), Content, AdID, CONCAT(FirstName, ' ', LastName) FROM CandidateAds INNER JOIN Users ON Users.UserID = CandidateAds.OwnerID WHERE Industry = @ind AND SalaryMin >= @smin AND SalaryMax <= @smax AND (Title LIKE '%" + keywordString + "%' OR Content LIKE '%" + keywordString + "%')";

                    SqlCommand cmd = new SqlCommand(sqlStatement, connection);
                    cmd.Parameters.Add("@ind", System.Data.SqlDbType.NVarChar, 50).Value = newItem.Industry;
                    cmd.Parameters.Add("@smin", System.Data.SqlDbType.Int).Value = newItem.SalaryMin;
                    cmd.Parameters.Add("@smax", System.Data.SqlDbType.Int).Value = newItem.SalaryMax;
                    cmd.Parameters.Add("@key", System.Data.SqlDbType.NVarChar, 10).Value = keywordString;
                    try
                    {
                        connection.Open();
                        SqlDataReader reader = cmd.ExecuteReader();
                        if (reader.HasRows)
                        {
                            while (reader.Read())
                            {
                                SearchingResult result = new SearchingResult();

                                result.OrganizerID = reader.GetInt32(0);
                                result.Industry = reader.GetString(1);
                                result.Title = reader.GetString(2);
                                result.Salary = reader.GetString(3);
                                result.Content = reader.GetString(4);
                                result.AdID = reader.GetInt32(5);
                                result.CompanyName = reader.GetString(6);
                                int index = returnList.FindIndex(item => item.AdID == result.AdID);
                                if (index >= 0 || result.OrganizerID == GlobalVariables.GlobaluserID)
                                {
                                    continue;
                                }
                                returnList.Add(result);
                            }
                        }
                    }
                    catch (Exception ex)
                    {
                        Console.WriteLine(ex.Message);
                    }
                }
            }
            return returnList;
        }

        public List<SearchingResult> FetchAllCanOffers(SearchedItem newItem)
        {
            List<SearchingResult> returnList = new List<SearchingResult>();
                using (SqlConnection connection = new SqlConnection(GlobalVariables.connectionString))
                {
                    string sqlStatement = "SELECT OwnerID, Industry, Title, CONCAT(SalaryMin, '-', SalaryMax), Content, AdID, CONCAT(FirstName, ' ', LastName) FROM CandidateAds INNER JOIN Users ON Users.UserID = CandidateAds.OwnerID";

                    SqlCommand cmd = new SqlCommand(sqlStatement, connection);
                    try
                    {
                        connection.Open();
                        SqlDataReader reader = cmd.ExecuteReader();
                        if (reader.HasRows)
                        {
                            while (reader.Read())
                            {
                                SearchingResult result = new SearchingResult();

                                result.OrganizerID = reader.GetInt32(0);
                                result.Industry = reader.GetString(1);
                                result.Title = reader.GetString(2);
                                result.Salary = reader.GetString(3);
                                result.Content = reader.GetString(4);
                                result.AdID = reader.GetInt32(5);
                                result.CompanyName = reader.GetString(6);
                                int index = returnList.FindIndex(item => item.AdID == result.AdID);
                                if (index >= 0 || result.OrganizerID == GlobalVariables.GlobaluserID)
                                {
                                    continue;
                                }
                                returnList.Add(result);
                            }
                        }
                    }
                    catch (Exception ex)
                    {
                        Console.WriteLine(ex.Message);
                    }
                }
            return returnList;
        }
    }
}
