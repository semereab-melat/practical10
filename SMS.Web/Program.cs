using SMS.Data.Repository;
using SMS.Data.Services;
using Microsoft.AspNetCore.Authentication.Cookies;
using SMS.Web;

var builder = WebApplication.CreateBuilder(args);

//Add Authontiction service using Cookie Scheme
builder.Services.AddCookieAuthentication();


// Add services to the container.
builder.Services.AddControllersWithViews();

//configuring Dependency Injection
builder.Services.AddScoped<IStudentService, StudentServiceDb>();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
} else {
    // in  development mode seed the database each time the application starts
    StudentServiceSeeder.Seed(new StudentServiceDb());
}

//app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

// ** Enable site Authentication/Authorization **
app.UseAuthentication();
app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();
