using Microsoft.AspNetCore.SignalR.Client; // To use HubConnection.
using Northwind.Chat.Models; // To use UserModel, MessageModel.

Write("Enter a username (required): ");
string? username = ReadLine();

if (string.IsNullOrEmpty(username))
{
  WriteLine("You must enter a username to register with chat!");
  return;
}

Write("Enter your groups (optional): ");
string? groups = ReadLine();

HubConnection hubConnection = new HubConnectionBuilder()
  .WithUrl("https://localhost:5111/chat")
  .Build();

hubConnection.On<MessageModel>("ReceiveMessage", message =>
{
  WriteLine($"To {message.To}, From {message.From}: {message.Body}");
});

await hubConnection.StartAsync();

WriteLine("Successfully started.");

UserModel registration = new()
{
  Name = username,
  Groups = groups
};

await hubConnection.InvokeAsync("Register", registration);

WriteLine("Successfully registered.");
WriteLine("Listening... (press ENTER to stop.)");
ReadLine();
