namespace SoloDevApp.Repository.Models
{
    public class NguoiDung
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
        public string Phone { get; set; }

        public string Birthday { get; set; }
        public string Avatar { get; set; }
        public bool Gender { get; set; }
        public string Role { get; set; }

    }
}