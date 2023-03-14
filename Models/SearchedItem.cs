using HireMe.Interfaces;
namespace HireMe.Models
{
    public class SearchedItem : ISearchedItem
    {
        public string? Industry { get; set; }
        public int SalaryMin { get; set; }
        public int SalaryMax { get; set; }
        public string? Keywords { get; set; }
    }
}
