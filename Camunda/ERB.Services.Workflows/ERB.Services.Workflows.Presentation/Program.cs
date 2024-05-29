using ERB.Services.Workflows.Application.Workers;
using System.Reflection;
using Zeebe.Client.Accelerator.Extensions;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

// Add Bootstrap Zeebe Client 
builder.Services.BootstrapZeebe(
    builder.Configuration.GetSection("ZeebeConfiguration"),
    Assembly.GetAssembly(typeof(WorkersPlaceHolder))
   );

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

app.CreateZeebeDeployment()
    .UsingDirectory(Path.Combine(AppContext.BaseDirectory, "Resources"))
    .AddResource("annual-leave-request.bpmn")
    .Deploy();

app.Run();

