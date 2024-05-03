

namespace Models
{
    public class Language
    {
        public int LanguageId { get; set; }
        public string Name { get; set; }

        public ICollection<User>? Users { get; set; } = new List<User>();

    }
}