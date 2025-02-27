using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using CafeEmployeeAPI.Application.Queries.Cafe; 
using CafeEmployeeAPI.Application.Queries.Employee; 
using CafeEmployeeAPI.Application.DTOs;
using CafeEmployeeAPI.Application.Commands.Cafe;
using CafeEmployeeAPI.Application.Commands.Employee;
using CafeEmployeeAPI.Infrastructure.Middleware;
using CafeEmployeeAPI.Infrastructure;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
// Learn more about configuring OpenAPI at https://aka.ms/aspnet/openapi
builder.Services.AddOpenApi();
builder.Services.AddControllers(); // Ensure this line is present
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowSpecificOrigin", policy =>
    {
        policy.WithOrigins("http://localhost:5173","http://localhost:5174","http://gictests3.s3-website-us-east-1.amazonaws.com")  // Replace with your allowed origin
              .AllowAnyMethod()                  // Allow any HTTP method (GET, POST, etc.)
              .AllowAnyHeader();                 // Allow any headers
    });
});

builder.Services.AddMediatR(cfg => cfg.RegisterServicesFromAssembly(typeof(Program).Assembly));
builder.Services.AddTransient<IRequestHandler<GetAllCafesQuery, List<CafeDto>>, GetAllCafesQueryHandler>();
builder.Services.AddTransient<IRequestHandler<GetAllEmployeesQuery, List<EmployeeDto>>, GetAllEmployeesQueryHandler>();

builder.Services.AddTransient<IRequestHandler<GetCafeByIdQuery, CafeDto>, GetCafeByIdQueryHandler>();
builder.Services.AddTransient<IRequestHandler<GetEmployeeByIdQuery, EmployeeDto>, GetEmployeeByIdQueryHandler>();
builder.Services.AddTransient<IRequestHandler<GetEmployeesByCafeQuery, List<EmployeeDto>>, GetEmployeesByCafeQueryHandler>();

builder.Services.AddTransient<IRequestHandler<CreateCafeCommand, CafeDto>, CreateCafeCommandHandler>();
builder.Services.AddTransient<IRequestHandler<UpdateCafeCommand, CafeDto>, UpdateCafeCommandHandler>();
builder.Services.AddTransient<IRequestHandler<DeleteCafeCommand, bool>, DeleteCafeCommandHandler>();

builder.Services.AddTransient<IRequestHandler<CreateEmployeeCommand, EmployeeDto>, CreateEmployeeCommandHandler>();
builder.Services.AddTransient<IRequestHandler<UpdateEmployeeCommand, EmployeeDto>, UpdateEmployeeCommandHandler>();
builder.Services.AddTransient<IRequestHandler<DeleteEmployeeCommand, bool>, DeleteEmployeeCommandHandler>();

builder.Services.AddInfrastructure(builder.Configuration);

var app = builder.Build();

app.UseMiddleware<ExceptionHandlingMiddleware>(); 

//enable cors for aws front end and local host
app.UseCors("AllowSpecificOrigin");

app.UseSwagger();
app.UseSwaggerUI();

app.UseAuthorization();
app.MapControllers();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();
}
app.UseHttpsRedirection();
app.Run();