using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace WebbkursProv.Models
{
    public class CreatedPage
    {
        [JsonPropertyName("id")]
        public long Id { get; set; }
        [JsonPropertyName("title")]
        public string Title { get; set; }
        [JsonPropertyName("header")]
        public bool Header { get; set; }
        [JsonPropertyName("right")]
        public bool Right { get; set; }
        [JsonPropertyName("footer")]
        public bool Footer { get; set; }
        [JsonPropertyName("order")]
        public long Order { get; set; }
        [JsonPropertyName("timeStamp")]
        public DateTime TimeStamp { get; set; }
        [JsonPropertyName("footerString")]
        public string FooterString { get; set; }
        [JsonPropertyName("bgColor")]
        public string BgColor { get; set; }
        //[JsonPropertyName("imgData")]
        //public byte[] ImageData { get; set; }
    }
}
