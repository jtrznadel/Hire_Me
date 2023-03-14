namespace HireMe.Models
{
    public class UserModel : ICloneable
    {
        public int Id { get; set; }
        public string Username { get; set; }

        public string Password { get; set; }

        public object Clone()
        {
            return this.MemberwiseClone();
        }
    }
}
