namespace Noticebord.Client.Models
{
    public class User : Model
    {
        public string Name { get; set; }
        public string Email { get; set; }
        public string ProfilePhotoUrl { get; set; }
    }
}