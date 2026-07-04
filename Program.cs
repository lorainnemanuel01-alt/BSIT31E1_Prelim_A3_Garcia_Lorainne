using System.Text;

using var client = new HttpClient();
HttpResponseMessage? response = null;

string baseUrl = "https://jsonplaceholder.typicode.com/posts";
string specificUrl = $"{baseUrl}/1";
string targetUrl = "";
string verb = "";

var requestBody = new StringContent(
    """{"id":100,"name":"Jane Doe"}""",
    Encoding.UTF8,
    "application/json"
);

Console.Write("Enter choice (A-GET, B-POST, C-PUT, D-DELETE): ");
string choice = Console.ReadLine()?.Trim().ToUpper() ?? "";

if (choice == "A")
{
    verb = "GET";
    targetUrl = specificUrl;
    response = await client.GetAsync(targetUrl);
}
else if (choice == "B")
{
    verb = "POST";
    targetUrl = baseUrl;
    response = await client.PostAsync(targetUrl, requestBody);
}
else if (choice == "C")
{
    verb = "PUT";
    targetUrl = specificUrl;
    response = await client.PutAsync(targetUrl, requestBody);
}
else if (choice == "D")
{
    verb = "DELETE";
    targetUrl = specificUrl;
    response = await client.DeleteAsync(targetUrl);
}

if (response != null)
{
    Console.WriteLine($"\nVerb: {verb}");
    Console.WriteLine($"URL: {targetUrl}");
    Console.WriteLine($"Status: {(int)response.StatusCode} {response.StatusCode}");

    if (response.IsSuccessStatusCode)
    {
        if (verb == "POST" || verb == "PUT")
        {
            string body = await response.Content.ReadAsStringAsync();
            Console.WriteLine($"Response Body:\n{body}");
        }
        else if (verb == "GET")
        {
            var forecast = new WeatherForecastController.WeatherForecast
            {
                Date = DateTime.Now,
                TemperatureC = 33,
                Summary = "Partly Cloudy with occasional showers"
            };

            Console.WriteLine("\n--- WEATHER FORECAST ---");
            Console.WriteLine($"Date: {forecast.Date.ToShortDateString()}");
            Console.WriteLine($"Temperature: {forecast.TemperatureC}°C ({forecast.TemperatureF}°F)");
            Console.WriteLine($"Summary: {forecast.Summary}");
            Console.WriteLine("------------------------");
        }
    }
    else
    {
        Console.WriteLine("Request failed.");
    }
}
else
{
    Console.WriteLine("Invalid selection.");
}