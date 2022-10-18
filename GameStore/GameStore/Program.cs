using Amazon;
using AWSCloudService;
using GameStore.Extensions;
using LoggingService;
using Microsoft.AspNetCore.Http.Features;
using Microsoft.AspNetCore.HttpOverrides;
using Microsoft.Extensions.FileProviders;
using NLog;
using Presentation.ActionFilters;

var builder = WebApplication.CreateBuilder(args);

// add services to the container

LogManager.LoadConfiguration(string.Concat(Directory.GetCurrentDirectory(), 
    "/nlog.config")); 
builder.Services.ConfigureCors(); 
builder.Services.ConfigureIISIntegration(); 
builder.Services.ConfigureLoggerService(); 
builder.Services.AddAuthentication(); 
builder.Services.ConfigureIdentity(); 
builder.Services.ConfigureJWT(builder.Configuration); 
builder.Services.ConfigureRepositoryManager();
builder.Services.ConfigureServiceManager(); 
builder.Services.AddAutoMapper(typeof(Program)); 
builder.Services.AddScoped<ValidationFilterAttribute>();

builder.Services.ConfigureSqlContext(builder.Configuration);



#region Aws service
builder.Services.AddSingleton<IAwsServices, AwsService>();
#endregion


#region for images 
// builder.Services.Configure<FormOptions>(o =>
// {
//     o.ValueLengthLimit = int.MaxValue;
//     o.MultipartBodyLengthLimit = int.MaxValue;
//     o.MemoryBufferThreshold = int.MaxValue;
// });
#endregion 

builder.Services.AddControllers()
    .AddApplicationPart(typeof(Presentation.AssemblyReference).Assembly);
var app = builder.Build();

//Configure the Http  request pipeine

#region Global Error handler
var logger = app.Services.GetRequiredService<ILoggerManager>(); 
app.ConfigureExceptionHandler(logger); 
 
if (app.Environment.IsProduction()) 
    app.UseHsts(); 

#endregion

app.UseHttpsRedirection();

app.UseStaticFiles(); 
app.UseForwardedHeaders(new ForwardedHeadersOptions 
{ 
    ForwardedHeaders = ForwardedHeaders.All 
}); 
 
app.UseCors("CorsPolicy");

#region  For Images

// app.UseStaticFiles();
// app.UseStaticFiles(new StaticFileOptions()
// {
//     FileProvider = new PhysicalFileProvider(Path.Combine(Directory.GetCurrentDirectory(), @"Resources")),
//     RequestPath = new PathString("/Resources")
// });

#endregion

app.UseAuthentication(); 
app.UseAuthorization();

app.MapControllers();

app.Run();