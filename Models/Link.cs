using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace WebbkursProv.Models
{
    public class Link
    {
        [JsonPropertyName("id")]
        public long Id { get; set; }
        [JsonPropertyName("articleId")]
        public long ArticleId { get; set; }
        [JsonPropertyName("video")]
        public bool Video { get; set; }
        [JsonPropertyName("linkString")]
        public string LinkString { get; set; }
    }
}
