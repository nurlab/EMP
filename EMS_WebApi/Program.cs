using Autofac.Extensions.DependencyInjection;
using Autofac;
using AutoMapper;
using EMS.Data.DataContext;
using EMS.Services;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using System;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Host.UseServiceProviderFactory(new AutofacServiceProviderFactory()).ConfigureContainer<ContainerBuilder>(builder => { builder.RegisterModule(new AutoFacConfiguration()); });
var config = new MapperConfiguration(cfg => { cfg.AddProfile(new MappingProfile()); });
var mapper = config.CreateMapper();
builder.Services.AddSingleton(mapper);

builder.Services.AddDbContext<AppDbContext>(options =>
options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));

builder.Services.AddScoped<DataSeedInitializations>();
builder.Services.AddSingleton<IHttpContextAccessor, HttpContextAccessor>();

;

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
    app.SeedSqlServer();    

}

app.UseHttpsRedirection();

app.UseAuthorization();
app.MapControllers();

app.Run();
