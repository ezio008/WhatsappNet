namespace WhatsappNet.Api.Controllers
{
    using System;
    using System.Collections.Generic;

    using System.Globalization;
    using Newtonsoft.Json;
    using Newtonsoft.Json.Converters;

    public partial class MessageModel
    {
        [JsonProperty("messaging_product")]
        public string MessagingProduct { get; set; } = "whatsapp";

        [JsonProperty("recipient_type")]
        public string RecipientType { get; set; } = "individual";

        [JsonProperty("to")]
        public string To { get; set; } = string.Empty;

        [JsonProperty("type")]
        public string Type { get; set; } = "text";

        [JsonProperty("text")]
        public Text Text { get; set; } = new Text();
    }

    public partial class Text
    {
        [JsonProperty("preview_url")]
        public bool PreviewUrl { get; set; } = false;

        [JsonProperty("body")]
        public string Body { get; set; } = string.Empty;
    }
}
