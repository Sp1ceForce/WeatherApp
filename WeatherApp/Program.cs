using System.Web;
using Newtonsoft.Json;
using System.Text;
using WeatherApp;
Console.OutputEncoding = Encoding.UTF8;
var API_Key = "4a64a53c024f793c57e4b57095e85459";
var tgKey = "6068662939:AAHVlRxsYTA5XH46ndifEGWjFuWQr9UkFfk";
var client = new HttpClient();
int offset = 0;
Console.WriteLine("Начало работы");
while (true)
{
    var tgInput = await client.GetAsync($"https://api.telegram.org/bot{tgKey}/getUpdates?offset={offset}&allowed_updates=[\"message\"]");
    
    if (tgInput.IsSuccessStatusCode)
    {
        string res = await tgInput.Content.ReadAsStringAsync();
        var updates = JsonConvert.DeserializeObject<TGInput>(res);
        foreach (var update in updates.Results)
        {
            int chatId = update.NewMessage.Chat.ChatId;
            string inputMessage = update.NewMessage.Text;
            string resText = string.Empty;
            if (inputMessage == "/commands" || inputMessage == "/start")
            {
                resText = "Доступны следующие команды:\n/getweatherinfo [Название города] - получение информации о погоде в выбранном городе";
            }
            else if (inputMessage.Contains("/getweatherinfo"))
            {
                string cityName = inputMessage.Replace("/getweatherinfo ", "");
                var response = await client.GetAsync($"https://api.openweathermap.org/data/2.5/weather?q={HttpUtility.UrlEncode(cityName)}&appid={API_Key}&units=metric&lang=ru");
                Result weatherResult;
                if (response.IsSuccessStatusCode)
                {
                    string weatherText = await response.Content.ReadAsStringAsync();
                    Console.WriteLine(cityName);
                    weatherResult = JsonConvert.DeserializeObject<Result>(weatherText);
                    resText = "Результат вашего запроса: \n";
                    resText += $"На улице города {weatherResult.CityName} сейчас {weatherResult.Weather[0].WeatherDescription}\n";
                    resText += $"Температура равна {weatherResult.Main.Temperature} но ощущается как {weatherResult.Main.FeelsLike}\n";
                    resText += @$"Скорость ветра: {weatherResult.WindInfo.WindSpeed} м\с. Направление {GetWindDirection(weatherResult.WindInfo.WindDirectionDeg)}" + "\n";
                    resText += $"Давление {weatherResult.Main.Pressure} мм. ртутного столба\n";
                }
                else
                {
                    resText = "Ошибка, вы указали неправильный город";
                }
            }
            else
            {
                resText = "Такая команда не существует, введите /commands чтобы увидеть список команд";
            }
            await client.GetAsync($"https://api.telegram.org/bot{tgKey}/sendMessage?chat_id={chatId}&text={resText}");
        }
        if (updates.Results.Length > 0)
        {
            offset = updates.Results[updates.Results.Length - 1].UpdateID+1;
        }
    }
}
//Превращение градусов направления ветра в нормальные буквы
string GetWindDirection(int degrees) =>
 degrees switch
{
        > 345 and <= 360 or >= 0 and < 15 => "С",
        >= 15 and <= 75 => "СВ",
        > 75 and < 105 => "В",
        >= 105 and <= 165 => "ЮВ",
        > 165 and < 195 => "Ю",
        >= 195 and <= 255 => "ЮЗ",
        > 255 and < 285 => "З",
        >= 285 and <= 345 => "СЗ",
        _ => "Ошибка"
};
