using ERB.Services.EInvoice.Domain.Shared.Product;
using ERB.Services.EInvoice.Infrastructure.Product;
using ERP.Services.CRM.Application.Shared.Emails;
using Platform.Services.SendGrid;
using Platform.Services.SendGrid.SMTP;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

// Create SMTP Client
SmtpEmailClient SMTPClient = builder.Configuration.GetSection("EmailsService:SmtpSettings").Get<SmtpEmailClient>();
// Add SendGrid Clients and Services 
SendGridServiceExtensions.AddSendGridAndSMTP(builder.Services, builder.Configuration["EmailsService:SENDGRID_API_KEY"], SMTPClient);

builder.Services.AddScoped<IEmailsService, EmailsService>();

builder.Services.AddSingleton<IHttpContextAccessor, HttpContextAccessor>();
builder.Services.AddScoped<IProductService, ProductService>();

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
