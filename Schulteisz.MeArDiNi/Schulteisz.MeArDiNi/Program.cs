using Schulteisz.MeArDiNi;
using Schulteisz.MeArDiNi.Hubs;

var builder = WebApplication.CreateBuilder(args);
builder.Services.AddSignalR();
builder.Services.AddCors(option =>
    option.AddDefaultPolicy(builder =>
    {
        builder.WithOrigins("http://localhost:3000")
        .AllowAnyHeader()
        .AllowAnyMethod()
        .AllowCredentials();
    }));
builder.Services.AddSingleton<IDictionary<string, UserConnection>>(opt => new Dictionary<string, UserConnection>());

var app = builder.Build();


app.MapHub<GameSpaceHub>("/gamespace");
app.UseCors();

app.Run();
