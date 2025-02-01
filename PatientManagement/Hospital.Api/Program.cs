using FluentValidation;
using Hospital.Application;
using Hospital.Application.Middlewares;
using Hospital.Application.Patients.Queries.GetPatientById;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
builder.Services.AddApplication();

builder.Services.AddSwaggerGen();

//builder.Services.AddMediatR(config =>
//{
//    config.RegisterServicesFromAssembly(typeof(PatientCommandHandler).Assembly);
//    config.AddOpenBehavior(typeof(ValidationBehavior<,>));
//});

builder.Services.AddValidatorsFromAssemblyContaining<GetCategoryByIdQueryValidator>(ServiceLifetime.Transient);

//builder.Services.AddAutoMapper(typeof(PatientRequestProfile).Assembly);

var app = builder.Build();

// Configure the HTTP request pipeline.
app.UseMiddleware<CustomExceptionHandlerMiddleware>();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}


app.UseHttpsRedirection();

app.MapControllers();

app.Run();
