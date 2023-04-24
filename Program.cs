using Hangfire;
using Hangfire.PostgreSql;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

// docker run --rm   --name pg-docker -e POSTGRES_PASSWORD=docker -d -p 5432:5432  postgres
builder.Services
    .AddHangfire(x =>
        x.UsePostgreSqlStorage("Server=localhost;User Id=postgres;Password=docker;Database=postgres;"));
builder.Services.AddHangfireServer();



var app = builder.Build();

app.UseMiddleware<AuthenticationMiddleware>();
app.UseHangfireDashboard("/hangfire", new DashboardOptions
{
    Authorization = new[] { new HangFireAuthorizationFilter() }
});

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
