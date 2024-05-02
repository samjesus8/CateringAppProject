namespace App.Database
{
    public class User
    {
        public string Username { get; set; }
        public string Password { get; set; }
        public long UserID { get; set; }
        public string UserType { get; set; }
        public string Address { get; set; }
        public string ProfilePictureURL { get; set; }
    }
}
