using Newtonsoft.Json;


namespace WeatherApp
{
    public class Result
    {
        [JsonProperty("coord")]
        public Coordinates Coordinates { get; set; }
        [JsonProperty("weather")]
        public Weather[] Weather { get; set; }
        [JsonProperty("base")]
        public string Base { get; set; }
        [JsonProperty("main")]
        public Main Main { get; set; }
        [JsonProperty("visibility")]
        public int VisibilityDistance { get; set; }
        [JsonProperty("wind")]
        public Wind WindInfo { get; set; }
        [JsonProperty("rain")]
        public Rain RainVolumes { get; set; }
        [JsonProperty("snow")]
        public Snow SnowVolumes { get; set; }
        [JsonProperty("clouds")]
        public Clouds Cloudiness { get; set; }
        [JsonProperty("dt")]
        public int DataCalculationTime { get; set; }
        [JsonProperty("sys")]
        public Sys sys { get; set; }
        [JsonProperty("timezone")]
        public int Timezone { get; set; }
        [JsonProperty("id")]
        public int CityId { get; set; }
        [JsonProperty("name")]
        public string CityName { get; set; }
        public int cod { get; set; }
        [JsonProperty("dt_txt")]
        public string DtTxt { get; set; }
    }

    public class Rain
    {
        [JsonProperty("1h")]
        float RainPer1Hour { get; set; }
        [JsonProperty("3h")]
        float RainPer3Hours { get; set; }
    }
    public class Snow
    {
        [JsonProperty("1h")]
        float SnowPer1Hour { get; set; }
        [JsonProperty("3h")]
        float SnowPer3Hours { get; set; }
    }
    public class Sys
    {
        public int type { get; set; }
        public int id { get; set; }
        public string country { get; set; }
        public int sunrise { get; set; }
        public int sunset { get; set; }
    }

    public class Clouds
    {
        [JsonProperty("all")]
        int CloudinessPercent { get; set; }
    }

    public class Wind
    {

        [JsonProperty("speed")]
        public float WindSpeed { get; set; }
        [JsonProperty("deg")]
        public int WindDirectionDeg { get; set; }
        [JsonProperty("gust")]
        public float WindGust { get; set; }
    }

    public class Main
    {

        [JsonProperty("temp")]
        public float Temperature { get; set; }
        [JsonProperty("feels_like")]
        public float FeelsLike { get; set; }
        [JsonProperty("temp_min")]
        public float MinTemperature { get; set; }
        [JsonProperty("temp_max")]
        public float MaxTemperature { get; set; }
        [JsonProperty("pressure")]
        public int Pressure { get; set; }
        [JsonProperty("humidity")]
        public int Humidity { get; set; }
        [JsonProperty("sea_level")]
        public int SeaLevel { get; set; }
        [JsonProperty("grnd_level")]
        public int GroundLevel { get; set; }
    }

    public class Coordinates
    {
        [JsonProperty("lon")]
        public float Longitude;
        [JsonProperty("lat")]
        public float Latitude;
    }
    public class Weather
    {
        [JsonProperty("id")]
        public int Id { get; set; }
        [JsonProperty("main")]
        public string WeatherParameter { get; set; }
        [JsonProperty("description")]
        public string WeatherDescription { get; set; }
        [JsonProperty("icon")]
        public string WeaterIconId { get; set; }
    }
}
