using System.Net.Http.Json;
using TestClient.Models;

var http = new HttpClient() { BaseAddress = new Uri("http://localhost:5000") };

while (true)
{
    var alert = new Alert
    {
        TimeStamp = DateTime.Now,
        Severity ="High",
        Type ="Overheating",
        Machine = "TestConsoleApp",
        Message = "Temp: 104 °C"

    };

    var response = await http.PostAsJsonAsync<Alert>("/alerts", alert);
    Console.WriteLine($"{alert.TimeStamp}: {alert.Type} message sent. Status: {response.StatusCode}");

    await Task.Delay(TimeSpan.FromSeconds(30));
}