namespace Example1.API.Entities
{
    public class User
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public string Email { get; set; }
        public DateTime CreatedDate { get; set; }
        public Guid NumberPhone { get; set; }
        public string ImageAvt { get; set; }
        public int Role { get; set; }

    }
}
