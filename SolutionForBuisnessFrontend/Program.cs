using Blazored.Toast;
using Blazored.Toast.Configuration;
using SolutionForBuisnessFrontend.Components;
using SolutionForBuisnessFrontend.Components.Models;
using SolutionForBuisnessFrontend.Components.Models.Commands;
using SolutionForBuisnessFrontend.Components.Service.Repository;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddRazorComponents()
    .AddInteractiveServerComponents();

builder.Services.AddHttpClient("ApiClient", client =>
{
    client.BaseAddress = new Uri("http://solutionforbuisnesstest:80/");
});

builder.Services.AddBlazoredToast();

builder.Services.AddTransient(typeof(IRepository<Resource, string, DictionaryPatchCommand>),
    typeof(DictionaryRepository<Resource>));
builder.Services.AddTransient(typeof(IRepository<Unit, string, DictionaryPatchCommand>),
    typeof(DictionaryRepository<Unit>));

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Error", createScopeForErrors: true);
}


app.UseAntiforgery();

app.MapStaticAssets();
app.MapRazorComponents<App>()
    .AddInteractiveServerRenderMode();

app.Run();
