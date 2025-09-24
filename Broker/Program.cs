using Broker.Hubs;
using Broker.Models;
using Microsoft.AspNetCore.SignalR;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddSignalR();
builder.WebHost.UseUrls("http://localhost:5000");

var app = builder.Build();

app.MapPost("/alerts", async (Alert alert, IHubContext<HmiHub> hub) =>
{
   
  await hub.Clients.All.SendAsync("Alert", alert);
   return Results.Accepted();

  
});

app.MapHub<HmiHub>("/hmi");



app.Run();


