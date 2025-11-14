using AppointmentScheduling.Handlers;
using AppointmentScheduling.Repositories;
using AppointmentScheduling.Settings;
using Microsoft.Extensions.Options;
using MongoDB.Driver;

var builder = WebApplication.CreateBuilder(args);


builder.Services.AddControllers();

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.Configure<MongoDbSettings>(builder.Configuration.GetSection("ConnectionStrings"));
builder.Services.AddSingleton<IMongoClient>(sp =>
{
    var settings = sp.GetRequiredService<IOptions<MongoDbSettings>>().Value;
    return new MongoClient(settings.ConnectionString);
});

builder.Services.AddScoped<IAppointmentRepository, AppointmentRepository>();
builder.Services.AddScoped<CreateAppointmentHandler>();
builder.Services.AddScoped<GetAppointmentsHandler>();
builder.Services.AddScoped<UpdateAppointmentHandler>();
builder.Services.AddScoped<DeleteAppointmentHandler>();
builder.Services.AddScoped<GetAppointmentsByClientHandler>();
builder.Services.AddScoped<GetAppointmentsByDateAndServiceHandler>();

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
