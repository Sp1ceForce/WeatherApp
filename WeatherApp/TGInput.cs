using Newtonsoft.Json;
namespace WeatherApp
{
    public class TGInput
    {
        [JsonProperty("result")]
        public Update[] Results;
    }
    public class Update
    {
        [JsonProperty("update_id")]
        public int UpdateID;
        [JsonProperty("message")]
        public Message NewMessage;
    }
    public class Message
    {
        [JsonProperty("message_id")]
        public int MessageID { get; set; }
        [JsonProperty("from")]
        public User User { get; set; }
        [JsonProperty("chat")]
        public Chat Chat { get; set; }
        [JsonProperty("text")]
        public string Text { get; set; }
    }
    public class User
    {
        [JsonProperty("username")]
        public string UserName { get; set; }
    }
    public class Chat
    {
        [JsonProperty("id")]
        public int ChatId { get; set; }
    }
}
