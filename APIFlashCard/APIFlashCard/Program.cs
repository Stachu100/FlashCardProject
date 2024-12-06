using Microsoft.EntityFrameworkCore;
using APIFlashCard.Data;


var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllers();

//Webio.pl Db
//builder.Services.AddDbContext<FlashCardDbContext>(options =>
//    options.UseMySql(
//        builder.Configuration.GetConnectionString("WebioDb"),
//        new MySqlServerVersion(new Version(5, 7, 32))
//    )
//);

// LocalDb
builder.Services.AddDbContext<FlashCardDbContext>(options =>
    options.UseSqlServer(
        builder.Configuration.GetConnectionString("FiszkiApp")
    )
);

// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseDeveloperExceptionPage();
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();