using LibraryApp.Infrastructure.Data;
using LibraryApp.Infrastructure.Repositories;
using LibraryApp.Infrastructure.Seeders;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder();
var connectionString = builder.Configuration.GetConnectionString("PostgresConnection");

builder.Services.AddControllersWithViews();
builder.Services.AddDbContext<ApplicationDbContext>(options => options.UseNpgsql(connectionString));
builder.Services.AddScoped<IUserRepository, UserRepository>();
builder.Services.AddScoped<IBookRepository, BookRepository>();
builder.Services.AddScoped<IUserRoleRepository, UserRoleRepository>();
builder.Services.AddScoped<DatabaseSeeder>();
builder.Services.AddSession(options =>
{
    options.Cookie.HttpOnly = true;
    options.Cookie.IsEssential = true;
});

var app = builder.Build();

using (var scope = app.Services.CreateScope())
{
    var seeder = scope.ServiceProvider.GetRequiredService<DatabaseSeeder>();
    await seeder.SeedRoles();
}

if (!app.Environment.IsDevelopment())
{
    app.UseDeveloperExceptionPage();
}
else
{
    app.UseExceptionHandler("/error");
    app.UseHsts();
}

app.Use(async (context, next) =>
{
    var path = context.Request.Path.Value;

    if (!string.IsNullOrEmpty(path) && path.Any(char.IsUpper))
    {
        context.Response.Redirect(path.ToLowerInvariant());
        return;
    }

    await next();
});

app.UseSession();
app.UseStatusCodePagesWithReExecute("/error", "?statusCode={0}");
app.UseHttpsRedirection();
app.UseRouting();
app.UseAuthorization();
app.MapStaticAssets();
app.MapControllerRoute(
        "default",
        "{controller=Login}/{action=Index}")
    .WithStaticAssets();
app.MapFallbackToController("Error", "Error");

app.Run();