using AGEX.INFRAESTRUCTURE.Extensions;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddServices(builder.Configuration);
builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

if (builder.Configuration.GetValue<bool>("ConfigurationSwagger:ShowDocumentation"))
{
    app.UseSwagger();
    app.UseSwaggerUI(options =>
    {
        string groupName = nameof(AGEX.API);

        options.SwaggerEndpoint(builder.Configuration.GetValue<string>("ConfigurationSwagger:Route"), groupName?.ToUpperInvariant());
    });
}

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
