namespace HireMe.Models
{
    public class MeetingModel
    {
        public int MeetingID { get; set; }
        public int OrganizerID { get; set; }
        public int GuestID { get; set; }

        public string OrganizerName { get; set; }
        public string GuestName { get; set; }
        public string MeetingInfo { get; set; }
        public DateTime MeetingDate { get; set; }
        public string MeetingPlace { get; set; }
        public string Status { get; set; }

    }
}
