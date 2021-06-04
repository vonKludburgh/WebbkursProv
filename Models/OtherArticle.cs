using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace WebbkursProv.Models
{
    public class OtherArticle
    {
        [JsonPropertyName("id")]
        public long Id { get; set; }
        [JsonPropertyName("pageId")]
        public long PageId { get; set; }
        [JsonPropertyName("link")]
        public string Link { get; set; }
        [JsonPropertyName("imgLink")]
        public string ImgLink { get; set; }
        [JsonPropertyName("text")]
        public string Text { get; set; }
    }
}
