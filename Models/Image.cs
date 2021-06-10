using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace WebbkursProv.Models
{
    public class Image
    {
        [JsonPropertyName("id")]
        public long Id { get; set; }        
        [JsonPropertyName("articleId")]
        public long ArticleId { get; set; }
        [JsonPropertyName("imageData")]
        public byte[] ImageData { get; set; }
        [JsonPropertyName("title")]
        public string Title { get; set; }
        [JsonPropertyName("pageId")]
        public long PageId { get; set; }
        [JsonPropertyName("location")]
        public string Location { get; set; }
    }
}
