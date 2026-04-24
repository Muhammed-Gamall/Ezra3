using Graduation;
using Graduation.Data;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddDependancies(builder.Configuration);

var app = builder.Build();

// Configure the HTTP request pipeline.
app.MapOpenApi();
if(app.Environment.IsDevelopment())
{
    app.MapScalarApiReference(x =>
    {
        x.WithTitle("Graduation Project").WithTheme(ScalarTheme.Mars);
    });
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
