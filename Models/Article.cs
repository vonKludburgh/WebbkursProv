using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace WebbkursProv.Models
{
    public class Article
    {
        [JsonPropertyName("id")]
        public long Id { get; set; }
        [JsonPropertyName("pageId")]
        public long PageId { get; set; }
        [JsonPropertyName("order")]
        public long Order { get; set; }
        [JsonPropertyName("header")]
        public string Header { get; set; }
        [JsonPropertyName("paragraph")]
        public string Paragraph { get; set; }
        [JsonPropertyName("timeStamp")]
        public DateTime TimeStamp { get; set; }
        [JsonPropertyName("bgColor")]
        public string BgColor { get; set; }
        [JsonPropertyName("published")]
        public bool Published { get; set; }
    }
}
