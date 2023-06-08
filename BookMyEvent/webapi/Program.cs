using BookMyEvent.WebApi;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
Startup.StartUpConfigure(builder.Services, builder.Configuration);
var SpecificAllowOrigins = "origin_names";

// Add services to the container.
builder.Services.AddCors(options =>
{
    options.AddPolicy(SpecificAllowOrigins, policy =>
    {
        policy.WithOrigins("https://localhost:3000");
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

app.UseAuthorization();

app.MapControllers();

app.Run();
