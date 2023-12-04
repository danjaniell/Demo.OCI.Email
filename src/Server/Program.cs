using Autofac;
using Autofac.Extensions.DependencyInjection;
using Server.Endpoints;

var builder = WebApplication.CreateBuilder(args);
builder.Host.UseServiceProviderFactory(new AutofacServiceProviderFactory());

// Add services to the container.
builder.Host.ConfigureContainer((Action<ContainerBuilder>)(containerBuilder => { }));

// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();
app.ConfigureEmailEndpoints(builder.Configuration);

app.Run();
