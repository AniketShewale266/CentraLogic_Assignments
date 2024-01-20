using Newtonsoft.Json;
namespace Library_Management.DTO
{
    public class UserDTO
    {
        [JsonProperty(PropertyName = "PRN", NullValueHandling = NullValueHandling.Ignore)]
        public int PRN { get; set; }

        [JsonProperty(PropertyName = "Username", NullValueHandling = NullValueHandling.Ignore)]
        public string Username { get; set; }

        [JsonProperty(PropertyName = "Password", NullValueHandling = NullValueHandling.Ignore)]
        public string Password { get; set; }

        [JsonProperty(PropertyName = "CPassword", NullValueHandling = NullValueHandling.Ignore)]
        public string CPassword { get; set; }
    }
}
