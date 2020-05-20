using LyndaSignalR.Models;
using LyndaSignalR.Services;
using Microsoft.AspNetCore.SignalR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Oldrook
{
    public class RookHub : Hub
    {
        private readonly IChatRoomService _chatRoomService;
        public RookHub(IChatRoomService chatRoomService)
        {
            _chatRoomService = chatRoomService;
        }
        public override async Task OnConnectedAsync()
        {
            var roomId = await _chatRoomService.CreateRoom(Context.ConnectionId);

            await Groups.AddToGroupAsync(Context.ConnectionId, roomId.ToString());

            await Clients.Caller.SendAsync("ReceiveMessage"
                , "Explore California"
                , DateTimeOffset.UtcNow
                , "Hello! Waht can we help you?");

            await base.OnConnectedAsync();
        }

        public override Task OnDisconnectedAsync(Exception exception)
        {
            Clients.All.SendAsync("ReceiveMessage"
                , "Buzi"
                , DateTimeOffset.UtcNow
                , "Elmentem.");

            return base.OnDisconnectedAsync(exception);
        }

        public async Task SendMessage(string name, string text)
        {
            var roomId = await _chatRoomService.GetRoomForConnectionId(Context.ConnectionId);

            var message = new ChatMessage
            {
                SenderName = name,
                Text = text,
                SentAt = DateTimeOffset.UtcNow
            };

            // Bradcast to all clients: Clients.All.SendAsync(
            //await Clients.Group(roomId.ToString()).SendAsync("ReceiveMessage"
            await Clients.All.SendAsync("ReceiveMessage"
                , message.SenderName
                , message.SentAt
                , message.Text);
        }
    }
}
