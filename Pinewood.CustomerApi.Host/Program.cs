using Pinewood.CustomerApi;
using Pinewood.Customers.EF;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddHandlers();
// builder.Services.AddInMemoryRepositories();
builder.Services.AddRepositories(builder.Configuration);
builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowCustomerApp",
        builder =>
        {
            builder.WithOrigins("https://localhost:7038")
                   .AllowAnyHeader()
                   .AllowAnyMethod()
                   .AllowCredentials();
        });
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
app.UseCors("AllowCustomerApp");
app.Run();
