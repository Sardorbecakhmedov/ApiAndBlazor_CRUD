using ApiProjectData.Context;
using ApiProjectData.Services;
using Microsoft.EntityFrameworkCore;
using Serilog;
using Serilog.Events;

var builder = WebApplication.CreateBuilder(args);

var logger = new LoggerConfiguration()
    .WriteTo.File(@"LoggerFolder\LogError.txt", LogEventLevel.Error)
    .CreateLogger();

builder.Logging.AddSerilog(logger);

builder.Services.AddDbContext<AppDbContext>(config =>
{
    // Server=myServerAddress;Database=myDataBase;User Id=myUsername;Password=myPassword;

/*    var path = builder.Configuration.GetValue<string>("ConnectionStringAppDbContext");
    config.UseSqlServer(path!);
*/
    config.UseInMemoryDatabase("Memory");
});


builder.Services.AddCors(policy =>
{
    policy.AddDefaultPolicy(corsPolicy =>
    {
        corsPolicy.AllowAnyOrigin().AllowAnyMethod().AllowAnyHeader();
    });

    //policy.AddPolicy("any", corsPolicy =>
    //{
    //    corsPolicy.AllowAnyOrigin.AllowAnyOrigin.AllowAnyHeader();
    //});
});


// Add services to the container.
builder.Services.AddControllers();

// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

/*app.UseCors(builderPolicy =>
{
    //builderPolicy.AllowAnyOrigin();   //// All site
    //builderPolicy.WithMethods("GET"); //// Allow only
    //builderPolicy.WithOrigins(@"https://localhost:7046");
    //builderPolicy.WithMethods("POST", "PUT");
    //builderPolicy.AllowAnyMethod();  //// Allow methods
    //builderPolicy.AllowAnyHeader();
    //builderPolicy.WithHeaders("Date", "Cookie");
});*/

//app.UseCors("any");

app.UseCors();

app.UseStaticFiles();

app.UseHttpsRedirection();

app.UseAuthorization();

app.UseMiddleware<LogErrorHandlerMiddleware>();

app.MapControllers();

app.Run();
