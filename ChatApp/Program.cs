using ChatApp.Data;
using ChatApp.Hubs;
using ChatApp.Services;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddSignalR();

builder.Services.AddControllers();


builder.Services.AddSingleton<IMongoDbService>();
builder.Services.AddTransient<IUserService, UserService>();
builder.Services.AddTransient<IMessegeService, MessegeService>();

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();



builder.Services.AddCors(options =>
{
    options.AddDefaultPolicy(
        builder =>
        {
              builder.WithOrigins("http://localhost:4200")
                     .AllowAnyHeader()
                     .WithMethods("GET", "POST")
                     .AllowCredentials();
        });
});

var app = builder.Build();


app.UseCors();

app.MapHub<ChatHub>("/chat");

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();

}

app.MapControllers();

app.Run();

