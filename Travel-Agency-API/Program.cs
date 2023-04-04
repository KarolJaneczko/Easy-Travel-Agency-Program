using Travel_Agency_API.Entities;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddDbContext<EasyTravelAgencyProgramContext>();
builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

if (app.Environment.IsDevelopment()) {
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();
app.UseAuthorization();
app.MapControllers();
app.Run();

//Scaffold-DbContext "Server=.\SQLEXPRESS;Database=Easy-Travel-Agency-Program;Encrypt=false;Trusted_Connection=true" Microsoft.EntityFrameworkCore.SqlServer -OutputDir Models