using WhatsappNet.Api.Services.WhatsappCloud;
using WhatsappNet.Api.Utils;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddScoped<IWhasappCloudService, WhasappCloudService>();
builder.Services.AddSingleton<IUtils, Utils>();

builder.Services.AddControllers();


var app = builder.Build();

// Configure the HTTP request pipeline.

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
