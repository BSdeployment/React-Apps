using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Text.Json;
using System.Text.Json.Serialization;
namespace LearningApp.WinForms.RequestServices
{
    public class WebViewMessage
    {
        public class WebViewRequest
        {

            [JsonPropertyName("requestId")]
            public int RequestId { get; set; }
            [JsonPropertyName("action")]
            public string Action { get; set; }
            [JsonPropertyName("payload")]
            public object Payload { get; set; }
        }

        public class WebViewResponse
        {
            [JsonPropertyName("requestId")]
            public int RequestId { get; set; }
            [JsonPropertyName("success")]
            public bool Success { get; set; }
            [JsonPropertyName("data")]
            public object Data { get; set; }
            [JsonPropertyName("error")]
            public string Error { get; set; }
        }
    }
}
