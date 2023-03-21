namespace Example01.Data
{
    public class User
    {
        public int Id { get; set; } 
        public string Name { get; set; }
        public string Email { get; set; }
        public string NumberPhone { get; set; }
        public string Role { get; set; }
        public User(int id , string name , string email, string numberphone, string role) {
        
            Id = id;
            Name = name;
            Email = email;
            NumberPhone = numberphone;
            Role = role;
        }
        public User()
        {
        }

    }
}
