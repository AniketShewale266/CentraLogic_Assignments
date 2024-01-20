using Newtonsoft.Json;

namespace Library_Management.DTO
{
    public class BookDTO
    {
        [JsonProperty(PropertyName = "Title", NullValueHandling = NullValueHandling.Ignore)]
        public string Title { get; set; }

        [JsonProperty(PropertyName = "Author", NullValueHandling = NullValueHandling.Ignore)]
        public string Author { get; set; }

        [JsonProperty(PropertyName = "Subject", NullValueHandling = NullValueHandling.Ignore)]
        public string Subject { get; set; }
    }
}
