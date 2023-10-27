using Newtonsoft.Json;
using System;

namespace Azure.Training.Mvc.WebApi.Models
{
    public class ContentMetaData
    {
        [JsonProperty("userGuid")]
        public string UserGuid { get; set; }
        [JsonProperty("id")]
        public string Id { get; set; }
        [JsonProperty("fileName")]
        public string FileName { get; set; }
        [JsonProperty("uploadDateTime")]
        public DateTime UploadDateTime {  get; set; } 
    }
}
