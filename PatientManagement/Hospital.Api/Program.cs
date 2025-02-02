// using System.Text.Json.Serialization;

using System.Text.Json.Serialization;
using FluentValidation;
using Hospital.Api.Mapping;
using Hospital.Application;
using Hospital.Application.Behaviors;
using Hospital.Application.Middlewares;
using Hospital.Application.Patients.Commands.CreatePatient;
using Hospital.Application.Patients.Queries.GetPatientById;
using Hospital.Infrastructure;
using Hospital.Infrastructure.Contexts;
using Microsoft.AspNetCore.Http.Json;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllers()
    .AddJsonOptions(options => options.JsonSerializerOptions.Converters.Add(new JsonStringEnumConverter()));

builder.Services.AddApplication();
builder.Services.AddInfrastructure(builder.Configuration);

builder.Services.AddSwaggerGen();

// настройка для Enum
// builder.Services.Configure<JsonOptions>(options =>
// {
//     options.SerializerOptions.Converters.Add(new JsonStringEnumConverter());
// });

builder.Services.AddMediatR(config =>
{
    config.RegisterServicesFromAssembly(typeof(CreatePatientCommandHandler).Assembly);
    config.AddOpenBehavior(typeof(ValidationBehavior<,>));
});

builder.Services.AddValidatorsFromAssemblyContaining<GetCategoryByIdQueryValidator>(ServiceLifetime.Transient);

builder.Services.AddAutoMapper(typeof(PatientRequestProfile).Assembly);

var app = builder.Build();

// Configure the HTTP request pipeline.
app.UseMiddleware<CustomExceptionHandlerMiddleware>();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
    
    using (var scope = app.Services.CreateScope())
    {
        var dbContext = scope.ServiceProvider
            .GetRequiredService<PatientDbContext>();

        dbContext.Database.Migrate();
    }
}

app.UseHttpsRedirection();

app.MapControllers();

app.Run();
