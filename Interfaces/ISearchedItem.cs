namespace HireMe.Interfaces
{
    public interface ISearchedItem
    {
        string Industry { get; set; }
        int SalaryMin { get; set; }
        int SalaryMax { get; set;}
        string Keywords { get; set; }
    }
}
