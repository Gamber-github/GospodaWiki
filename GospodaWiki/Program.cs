using GospodaWiki.Data;
using GospodaWiki.Interfaces;
using GospodaWiki.Repository;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);


// Add services to the container.

builder.Services.AddControllers()
    .AddJsonOptions(options => options.JsonSerializerOptions.DefaultIgnoreCondition = System.Text.Json.Serialization.JsonIgnoreCondition.WhenWritingNull);
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddAutoMapper(typeof(Program));
builder.Services.AddScoped<ICharacterInterface, CharacterRepository>();
builder.Services.AddScoped<IRpgSystemInterface, RpgSystemRepository>();
builder.Services.AddScoped<IEventInterface, EventRepository>();
builder.Services.AddScoped<IPlayerInterface, PlayerRepository>();
builder.Services.AddScoped<ILocationInterface, LocationRepository>();
builder.Services.AddScoped<ISeriesInterface, SeriesRepository>();
builder.Services.AddScoped<ITagInterface, TagRepository>();
builder.Services.AddScoped<IItemInterface, ItemRepository>();

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddDbContext<DataContext>(options =>
{
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection"));
});

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
