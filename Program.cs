var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
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

app.MapGet("/GenerateAltText/{imageUrl}", (string imageUrl) =>
{
    return AltTextGenAPI.AltTextGen.GetImageDescription(imageUrl);
})
    .WithName("GetGeneratedAltText")
    .WithSummary("Generates Alt Text")
    .WithDescription("Generates alt text for an image with OpenAI")
    .WithOpenApi();

app.Run();