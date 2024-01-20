using Newtonsoft.Json;
namespace Library_Management.Entities
{
    public class BookEntity
    {
        // Mandatory Feilds
        [JsonProperty(PropertyName = "id")]
        public string Id { get; set; } // Code : Pascal [ FirstName ] 
                                       // CosmosDb : Camalcases [ firstName ] 

        // JSON Property : For Our Code  Pascal [ FirstName ]   but DB Camelcase 

        [JsonProperty(PropertyName = "uId", NullValueHandling = NullValueHandling.Ignore)]
        public string UId { get; set; }

        [JsonProperty(PropertyName = "dType", NullValueHandling = NullValueHandling.Ignore)]
        public string DocumentType { get; set; }


        [JsonProperty(PropertyName = "createdBy", NullValueHandling = NullValueHandling.Ignore)]
        public string CreatedBy { get; set; }

        [JsonProperty(PropertyName = "createdByName", NullValueHandling = NullValueHandling.Ignore)]
        public string CreatedByName { get; set; }

        [JsonProperty(PropertyName = "createdOn", NullValueHandling = NullValueHandling.Ignore)]
        public DateTime CreatedOn { get; set; }

        [JsonProperty(PropertyName = "updatedBy", NullValueHandling = NullValueHandling.Ignore)]
        public string UpdatedBy { get; set; }

        [JsonProperty(PropertyName = "updatedByName", NullValueHandling = NullValueHandling.Ignore)]
        public string UpdatedByName { get; set; }

        [JsonProperty(PropertyName = "updatedOn", NullValueHandling = NullValueHandling.Ignore)]
        public DateTime UpdatedOn { get; set; }

        [JsonProperty(PropertyName = "version", NullValueHandling = NullValueHandling.Ignore)]
        public int Version { get; set; }

        [JsonProperty(PropertyName = "active", NullValueHandling = NullValueHandling.Ignore)]
        public bool Active { get; set; }

        [JsonProperty(PropertyName = "archieved", NullValueHandling = NullValueHandling.Ignore)]
        public bool Archieved { get; set; }

        [JsonProperty(PropertyName = "isAvailable", NullValueHandling = NullValueHandling.Ignore)]
        public bool IsAvailable { get; set; }

        [JsonProperty(PropertyName = "issuedToUserId", NullValueHandling = NullValueHandling.Ignore)]
        public string IssuedToUserId { get; set; }



        // Class  Feilds / Properties
        // [JsonProperty(PropertyName = "BookId", NullValueHandling = NullValueHandling.Ignore)]
        // public string BookId { get; set; }

        [JsonProperty(PropertyName = "Title", NullValueHandling = NullValueHandling.Ignore)]
        public string Title { get; set; }
        
        [JsonProperty(PropertyName = "Author", NullValueHandling = NullValueHandling.Ignore)]
        public string Author { get; set; }

        [JsonProperty(PropertyName = "Subject", NullValueHandling = NullValueHandling.Ignore)]
        public string Subject { get; set; }
    }
}
