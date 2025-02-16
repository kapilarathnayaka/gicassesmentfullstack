using Autofac;
using Autofac.Extensions.DependencyInjection;
using MediatR;
using Microsoft.EntityFrameworkCore;
using CafeEmployee.Infrastructure.Data;
using CafeEmployee.Application.Commands;
using CafeEmployee.Application.Handlers;
using CafeEmployee.Application.Queries;
using CafeEmployee.Infrastructure.Repositories;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

// Add AutoFac for dependency injection
builder.Host.UseServiceProviderFactory(new AutofacServiceProviderFactory());
builder.Host.ConfigureContainer<ContainerBuilder>(containerBuilder =>
{
    containerBuilder.RegisterModule(new ApplicationModule());
});

// Add EF Core for database context
builder.Services.AddDbContext<CafeEmployeeDbContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));

// Add Mediator for CQRS
builder.Services.AddMediatR(typeof(CreateCafeCommand).Assembly);
builder.Services.AddMediatR(typeof(CreateCafeHandler).Assembly);
builder.Services.AddMediatR(typeof(GetCafesQuery).Assembly);
builder.Services.AddMediatR(typeof(GetCafesHandler).Assembly);

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

public class ApplicationModule : Module
{
    protected override void Load(ContainerBuilder builder)
    {
        builder.RegisterType<CafeRepository>().As<ICafeRepository>().InstancePerLifetimeScope();
        builder.RegisterType<EmployeeRepository>().As<IEmployeeRepository>().InstancePerLifetimeScope();
    }
}
