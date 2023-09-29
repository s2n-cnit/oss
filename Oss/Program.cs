
using System.Threading.Channels;

using Microsoft.OpenApi.Models;

using Swashbuckle.AspNetCore.Filters;

using FluentValidation;
using LanguageExt.Common;
using MediatR;

using Oss.Code;
using Oss.Services;
using Oss.Requests;
using Oss.Validators;

var builder = WebApplication.CreateBuilder(args);

builder.Logging.ClearProviders();
builder.Logging.AddConsole();

var connectionString = builder.Configuration.GetConnectionString("DefaultConnection");

builder.Services.AddControllers();

builder.Services.AddEndpointsApiExplorer();

builder.Services.ConfigureSwaggerGen(o =>
{
    o.CustomSchemaIds(x => x.FullName);
});

builder.Services.AddSwaggerGen(c =>
{
    c.ResolveConflictingActions(descriptions =>
    {
        return descriptions.First();
    });

    c.SwaggerDoc("v1", new OpenApiInfo
    {
        Title = "5G-INDUCE ApplicationAware NAO - OSS NBI",
        Version = "0.3"
    });

    c.ExampleFilters();
});

builder.Services.AddSwaggerExamplesFromAssemblyOf<IntentExample>();

builder.Services.Configure<BackgroundTaskQueueOptions>(options =>
{
    var channelMaxElements = 100;

    options.BoundedChannelOptions = new BoundedChannelOptions(channelMaxElements)
    {
        FullMode = BoundedChannelFullMode.Wait
    };
});

builder.Services.Configure<MongoDbConnectionOptions>(options =>
{
    options.ConnectionString = connectionString;
});

builder.Services.AddHttpClient();
builder.Services.AddHttpClient<RestClient>();

builder.Services.AddSingleton<IMongoDbService, MongoDbService>();
builder.Services.AddScoped<DatabaseService>();
builder.Services.AddScoped<NfvclClientService>();

builder.Services.AddSingleton<IBackgroundTaskQueue, BackgroundTaskQueue>();
builder.Services.AddHostedService<QueuedHostedService>();

builder.Services.AddValidatorsFromAssemblyContaining<Program>(ServiceLifetime.Scoped);
builder.Services.AddMediatR(c => c
    .RegisterServicesFromAssemblyContaining<Program>()
    .AddBehavior<IPipelineBehavior<CreateSliceRequest, Result<string>>, ValidationBehavior<CreateSliceRequest, string>>());

var app = builder.Build();

app.UseSwagger();
app.UseSwaggerUI();

app.MapControllers();

app.Run();
