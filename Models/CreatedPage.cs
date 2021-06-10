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

        //Style Main
        [JsonPropertyName("fontMain")]
        public string FontMain { get; set; }
        [JsonPropertyName("bgColor")]
        public string BgColor { get; set; }
        [JsonPropertyName("borderColor")]
        public string BorderColor { get; set; }
        [JsonPropertyName("borderType")]
        public string BorderType { get; set; }
        //[JsonPropertyName("imgData")]
        //public byte[] ImgData { get; set; }
        [JsonPropertyName("imgBool")]
        public bool ImgBool { get; set; }

        //Style Header
        [JsonPropertyName("fontHeader")]
        public string FontHeader { get; set; }
        [JsonPropertyName("colorHeader")]
        public string ColorHeader { get; set; }
        [JsonPropertyName("borderColorHeader")]
        public string BorderColorHeader { get; set; }
        [JsonPropertyName("borderTypeHeader")]
        public string BorderTypeHeader { get; set; }
        //[JsonPropertyName("imgDataHeader")]
        //public byte[] ImgDataHeader { get; set; }
        [JsonPropertyName("imgBoolHeader")]
        public bool ImgBoolHeader { get; set; }

        //Style Left
        [JsonPropertyName("fontLeftbar")]
        public string FontLeftbar { get; set; }
        [JsonPropertyName("colorLeftbar")]
        public string ColorLeftbar { get; set; }
        [JsonPropertyName("borderColorLeftbar")]
        public string BorderColorLeftbar { get; set; }
        [JsonPropertyName("borderTypeLeftbar")]
        public string BorderTypeLeftbar { get; set; }
        //[JsonPropertyName("imgDataLeftbar")]
        //public byte[] ImgDataLeftbar { get; set; }
        [JsonPropertyName("imgBoolLeftbar")]
        public bool ImgBoolLeftbar { get; set; }

        //Style Right
        [JsonPropertyName("colorRightbar")]
        public string ColorRightbar { get; set; }
        [JsonPropertyName("borderColorRightbar")]
        public string BorderColorRightbar { get; set; }
        [JsonPropertyName("borderTypeRightbar")]
        public string BorderTypeRightbar { get; set; }
        //[JsonPropertyName("imgDataRightbar")]
        //public byte[] ImgDataRightbar { get; set; }
        [JsonPropertyName("imgBoolRightbar")]
        public bool ImgBoolRightbar { get; set; }

        //Style Footer
        [JsonPropertyName("fontFooter")]
        public string FontFooter { get; set; }
        [JsonPropertyName("colorFooter")]
        public string ColorFooter { get; set; }
        [JsonPropertyName("borderColorFooter")]
        public string BorderColorFooter { get; set; }
        [JsonPropertyName("borderTypeFooter")]
        public string BorderTypeFooter { get; set; }

        [JsonPropertyName("count")]
        public long Count { get; set; }
    }
}
