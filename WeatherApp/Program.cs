using System.Web;
using Newtonsoft.Json;
using System.Text;
using WeatherApp;
Console.OutputEncoding = Encoding.UTF8;
var API_Key = "4a64a53c024f793c57e4b57095e85459";
var client = new HttpClient();


Console.WriteLine("Введите город:");
string cityName = Console.ReadLine();
var response = await client.GetAsync($"https://api.openweathermap.org/data/2.5/weather?q={HttpUtility.UrlEncode(cityName)}&appid={API_Key}&units=metric&lang=ru");
Result weatherResult;
if (response.IsSuccessStatusCode)
{
    string res = await response.Content.ReadAsStringAsync();
    weatherResult = JsonConvert.DeserializeObject<Result>(res);
    Console.WriteLine("Результат вашего запроса: \n");
    Console.WriteLine($"На улице города {weatherResult.CityName} сейчас {weatherResult.Weather[0].WeatherDescription}");
    Console.WriteLine($"Температура равна {weatherResult.Main.Temperature} но ощущается как {weatherResult.Main.FeelsLike}");
    Console.WriteLine(@$"Скорость ветра: {weatherResult.WindInfo.WindSpeed} м\с. Направление {GetWindDirection(weatherResult.WindInfo.WindDirectionDeg)}");
    Console.WriteLine($"Давление {weatherResult.Main.Pressure} мм. ртутного столба\n");
}
else
{
    Console.WriteLine("Ошибка, вы указали неправильный город");
    Console.WriteLine("Нажмите на любую клавишу чтобы выключить приложение");
    Console.ReadKey();
    return;
}
//Отправка запроса и получение прогноза погоды на 4 дня
var response4Day = await client.GetAsync($"https://api.openweathermap.org/data/2.5/forecast?q={cityName}&appid={API_Key}&units=metric&lang=ru");
//Количество записей в list которые нужно пропустить чтобы перейти на следующий день
const int nextDayIterator = 8;
int dayIterator = nextDayIterator;
//Нужно для вывода дня недели на русском языке
var culture = new System.Globalization.CultureInfo("ru-ru");
Console.WriteLine("-------------------------------------------------");
Console.WriteLine("Прогноз на 4 дня: \n");
if (response4Day.IsSuccessStatusCode)
{
    //Конвертация ответа от сервиса в класс
    string jsonStr = await response4Day.Content.ReadAsStringAsync();
    var daysReports = JsonConvert.DeserializeObject<DaysReports>(jsonStr);
    
    for (int i = 0; i < 4; i++) 
    {
        var currDay = daysReports.list[dayIterator];
        DateTime date = DateTime.Parse(currDay.DtTxt);
        Console.WriteLine("-------------------------------------------------");
        Console.WriteLine($"{date.ToString("F")} - {culture.DateTimeFormat.GetDayName(date.DayOfWeek)}");
        Console.WriteLine($"Мин. температура: {currDay.Main.MinTemperature} Макс. температура: {currDay.Main.MaxTemperature}");
        Console.WriteLine($"Ожидаемое состояние погоды: {currDay.Weather[0].WeatherDescription}");
        dayIterator += nextDayIterator;
    }
}


Console.ReadKey();

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
