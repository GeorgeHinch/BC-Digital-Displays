using Newtonsoft.Json;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Diagnostics;
using System.Dynamic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace BC_Digital_Displays.Classes
{
    class SlackSender
    {
        //Post a message using simple strings
        public static async void slackMessageSender(string text)
        {
            Uri requestUri = new Uri("https://hooks.slack.com/services/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");

            dynamic dynamicJson = new ExpandoObject();
            dynamicJson.channel = "bcdigitaldisplays";
            dynamicJson.username = "Display Exception";
            dynamicJson.text = text;

            string json = "";
            json = JsonConvert.SerializeObject(dynamicJson);

            HttpClient client = new HttpClient();
            HttpResponseMessage respon = await client.PostAsync(requestUri, new StringContent(json, Encoding.UTF8, "application/json"));
            
            string responJsonText = await respon.Content.ReadAsStringAsync();
            Debug.WriteLine("Response: " + responJsonText);
        }

        public static async void slackExceptionSender(Exception e)
        {
            Uri requestUri = new Uri("https://hooks.slack.com/services/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");
            
            SlackPayload payload = new SlackPayload();
            payload.Channel = "bcdigitaldisplays";
            payload.Username = "Display Exception";

            SlackAttachmentsFields[] fieldsArray = new SlackAttachmentsFields[1];
            fieldsArray[0] = new SlackAttachmentsFields
            {
                Title = "Stack Trace",
                Value = e.StackTrace,
                Short = "false"
            };

            SlackAttachments[] attatchArray = new SlackAttachments[1];
            attatchArray[0] = new SlackAttachments
            {
                Fallback = "New Exception: <" + e.HelpLink + "|" + e.Message + ">",
                Pretext = "New Exception: <" + e.HelpLink + "|" + e.Message + ">",
                Color = "#D00000",
                AttachmentsFields = fieldsArray
            };
            
            payload.Attachments = attatchArray;

            string json = "";
            json = JsonConvert.SerializeObject(payload);

            Debug.WriteLine("Attatchments: " + json);

            HttpClient client = new HttpClient();
            HttpResponseMessage respon = await client.PostAsync(requestUri, new StringContent(json, Encoding.UTF8, "application/json"));
        }
    }

    //This class serializes into the Json payload required by Slack Incoming WebHooks
    public class SlackPayload
    {
        [JsonProperty("channel")]
        public string Channel { get; set; }

        [JsonProperty("username")]
        public string Username { get; set; }

        [JsonProperty("attachments")]
        public SlackAttachments[] Attachments { get; set; }
    }
    
    public class SlackAttachments
    {
        [JsonProperty("fallback")]
        public string Fallback { get; set; }

        [JsonProperty("pretext")]
        public string Pretext { get; set; }

        [JsonProperty("color")]
        public string Color { get; set; }

        [JsonProperty("fields")]
        public SlackAttachmentsFields[] AttachmentsFields { get; set; }
    }

    public class SlackAttachmentsFields
    {
        [JsonProperty("title")]
        public string Title { get; set; }

        [JsonProperty("value")]
        public string Value { get; set; }

        [JsonProperty("short")]
        public string Short { get; set; }
    }
}
