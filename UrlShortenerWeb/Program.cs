using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.FileProviders;
using UrlShortener.Data;
using UrlShortener.Services;
using UrlShortener.Validators;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
string connection = builder.Configuration.GetConnectionString("UrlShortener");
builder.Services.AddDbContext<UrlShortenerContext>(options => options.UseNpgsql(connection));
builder.Services.AddScoped<IUrlService, UrlService>();
builder.Services.AddScoped<IUrlValidator, UrlValidator>();
builder.Services.AddScoped<IBarCodeService, BarCodeService>();

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

app.UseStaticFiles();

app.UseStaticFiles(new StaticFileOptions
{
	FileProvider = new PhysicalFileProvider(
		   Path.Combine(builder.Environment.ContentRootPath, "MyStaticFiles")),
	RequestPath = "/StaticFiles"
});

app.Run();
