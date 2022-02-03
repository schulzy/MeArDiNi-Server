using Microsoft.AspNetCore.SignalR;

namespace Schulteisz.MeArDiNi.Hubs
{
    public class GameSpaceHub : Hub
    {
        private readonly string _botUser;
        private readonly IDictionary<string, UserConnection> _connections;
        private readonly Random _random = new Random();

        public GameSpaceHub(IDictionary<string, UserConnection> connections)
        {
            _botUser = "MyChat bot";
            _connections = connections;
        }

        public override Task OnDisconnectedAsync(Exception? exception)
        {
            return base.OnDisconnectedAsync(exception);
        }

        public async Task JoinGame(UserConnection userConnection)
        {
            await Groups.AddToGroupAsync(Context.ConnectionId, userConnection.Room);

            _connections[Context.ConnectionId] = userConnection;

            await Clients.Group(userConnection.Room).SendAsync("ReceiveMessage", _botUser, $"{userConnection.User} has joined {userConnection.Room}");

        }

        public async Task RollDice()
        {
            if (_connections.TryGetValue(Context.ConnectionId, out UserConnection userConnection))
            {
                await Clients.Group(userConnection.Room)
                    .SendAsync("ReceiveRollValue", userConnection.User, _random.Next(1,7));
            }
        }
    }
}
