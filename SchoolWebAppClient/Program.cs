using SchoolWebAppClient.Controllers;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddHttpClient<SchoolClientController>(client =>
{
     client.BaseAddress = new Uri("https://localhost:7018/");
});

// Add services to the container
builder.Services.AddControllersWithViews();

var app = builder.Build();

// Configure the HTTP request pipeline
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();
app.UseRouting();
app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=SchoolClient}/{action=Index}/{id?}");

app.Run();
