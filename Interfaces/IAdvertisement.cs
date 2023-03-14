namespace HireMe.Interfaces
{
    public interface IAdvertisement
    {
        int Id { get; set; }
        int Owner { get; set; }
        string Title { get; set; }
        string Content { get; set; }
        int SalaryMin { get; set; }
        int SalaryMax { get; set; }
        string Industry { get; set; }
    }
}
