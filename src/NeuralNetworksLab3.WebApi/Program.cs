using NeuralNetworksLab3.WebApi;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowReactApp",
        policy =>
        {
            policy.WithOrigins("http://localhost:5173")
                  .AllowAnyHeader()
                  .AllowAnyMethod();
        });
});

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddSingleton<MushroomClassifier>();

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseCors("AllowReactApp");

app.UseHttpsRedirection();

app.MapPost("/api/mushroom/classify", (MushroomClassifier classifier, int[] features) =>
{
    var isPoisonous = classifier.Predict(features);
    return Results.Ok(isPoisonous == 1 ? "Отруйний гриб" : "Їстівний гриб");
});

app.Run();
