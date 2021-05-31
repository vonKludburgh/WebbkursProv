using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace WebbkursProv.Models
{
    public class Document
    {
        [JsonPropertyName("id")]
        public long Id { get; set; }
        [JsonPropertyName("articleId")]
        public long ArticleId { get; set; }
        [JsonPropertyName("docData")]
        public byte[] DocData { get; set; }
        [JsonPropertyName("title")]
        public string Title { get; set; }
    }
}
