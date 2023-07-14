using BookMyEvent.WebApi;
using Microsoft.Net.Http.Headers;
using Microsoft.OpenApi.Models;
using Serilog;
using Serilog.Extensions.Logging;
var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(c =>
{
    c.SwaggerDoc("v1", new OpenApiInfo { Title = "ApiPlayground", Version = "v1" });
    c.AddSecurityDefinition("token", new OpenApiSecurityScheme
    {
        Type = SecuritySchemeType.Http,
        In = ParameterLocation.Header,
        Name = HeaderNames.Authorization,
        Scheme = "Bearer"
    });
    // dont add global security requirement
    // c.AddSecurityRequirement(/*...*/);
    c.OperationFilter<SecureEndpointAuthRequirementFilter>();
});

Startup.StartUpConfigure(builder.Services, builder.Configuration);
var SpecificAllowOrigins = "origin_names";

    Log.Logger = new LoggerConfiguration()
    .MinimumLevel.Verbose()
        .WriteTo.File("logs/logs.txt", rollingInterval: RollingInterval.Day)
        .CreateLogger();

    builder.Services.AddLogging(loggingBuilder =>
    {
        loggingBuilder.ClearProviders();
        loggingBuilder.AddConsole();
        loggingBuilder.AddSerilog(dispose: true);
    });
// Add services to the container.
builder.Services.AddCors(options =>
{
    options.AddPolicy(SpecificAllowOrigins, policy =>
    {
        policy.WithOrigins("https://localhost:3000").AllowCredentials();
        policy.WithMethods("POST", "PUT", "DELETE");
        policy.WithHeaders("*");
    });
});
var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
    
}

app.UseCors(SpecificAllowOrigins);
app.UseHttpsRedirection();
app.UseRouting();

app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();

app.Run();
