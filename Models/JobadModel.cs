using HireMe.Interfaces;
namespace HireMe.Models
{
    public class JobadModel : IAdvertisement
    {
        public int Id { get; set; }
        public int Owner { get; set; }
        public string Industry { get; set; }

        public string CompanyName { get; set; }
        public string Title { get; set; }
        public int SalaryMin { get; set; }
        public int SalaryMax { get; set; }

        public string Content { get; set; }
    }
}
