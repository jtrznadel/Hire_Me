using HireMe.Models;
using System.Data.SqlClient;

namespace HireMe.Data
{
    public class MeetingsDAO
    {
        public List<MeetingModel> FetchAll(int userID)
        {
            List<MeetingModel> returnList = new List<MeetingModel>();

            using (SqlConnection connection = new SqlConnection(GlobalVariables.connectionString))
            {
                string sqlStatement = "SELECT CONCAT(O.FirstName, ' ', O.LastName), CONCAT(U.FirstName, ' ', U.LastName), MeetingInfo, MeetingDate, MeetingPlace, Status  FROM Meetings INNER JOIN Users AS O ON O.UserID = Meetings.OrganizerID INNER JOIN Users AS U ON U.UserID = Meetings.GuestID WHERE OrganizerID = @id OR GuestID = @id";

                SqlCommand cmd = new SqlCommand(sqlStatement, connection);
                cmd.Parameters.Add("@id", System.Data.SqlDbType.Int).Value = userID;
                try
                {
                    connection.Open();
                    SqlDataReader reader = cmd.ExecuteReader();
                    if (reader.HasRows)
                    {
                        while (reader.Read())
                        {
                            MeetingModel model = new MeetingModel();

                            model.OrganizerName = reader.GetString(0);
                            model.GuestName = reader.GetString(1);
                            model.MeetingInfo = reader.GetString(2);
                            model.MeetingDate = reader.GetDateTime(3);
                            model.MeetingPlace = reader.GetString(4);
                            model.Status = reader.GetString(5);

                            returnList.Add(model);
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

        public void CreateMeeting(MeetingModel model, int guest)
        {
            string sqlStatement = "INSERT INTO Meetings(OrganizerID, GuestID, MeetingInfo, MeetingDate, MeetingPlace, Status) " +
                "VALUES (@organizer, @guest, @info, @date, @place, 'incoming')";
            using (SqlConnection connection = new SqlConnection(GlobalVariables.connectionString))
            {
                SqlCommand cmd = new SqlCommand(sqlStatement, connection);
                cmd.Parameters.Add("@organizer", System.Data.SqlDbType.Int).Value = model.OrganizerID;
                cmd.Parameters.Add("@guest", System.Data.SqlDbType.Int).Value = guest;
                cmd.Parameters.Add("@info", System.Data.SqlDbType.NVarChar, 50).Value = model.MeetingInfo;
                cmd.Parameters.Add("@date", System.Data.SqlDbType.DateTime).Value = model.MeetingDate;
                cmd.Parameters.Add("@place", System.Data.SqlDbType.NVarChar, 50).Value = model.MeetingPlace;
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
}
