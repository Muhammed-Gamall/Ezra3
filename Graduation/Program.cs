using Graduation;
using Graduation.Data;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddDependancies(builder.Configuration);

builder.Services.AddIdentityApiEndpoints<IdentityUser>().AddEntityFrameworkStores<ApplicationDbContext>();

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

app.MapIdentityApi<IdentityUser>();

app.MapControllers();

app.Run();
