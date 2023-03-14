namespace HireMe.Models
{
    public class KeywordModel
    {
        public List<string> KeywordSplit(string keyword)
        {
            List<string> split = new List<string>();
            split = keyword.Split(',').ToList();
            return split;
        }
    }
}
