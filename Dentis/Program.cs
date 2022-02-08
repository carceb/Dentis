using Dentis.Core;
using static Dentis.Core.Interfaces;

var builder = WebApplication.CreateBuilder(args);

// Manage Sessions
builder.Services.AddDistributedMemoryCache();
builder.Services.AddSession(options =>
{
    options.IdleTimeout = TimeSpan.FromHours(1);
    options.Cookie.HttpOnly = true;
    options.Cookie.IsEssential = true;
});

// Add services to the container.
builder.Services.AddControllersWithViews();
builder.Services.AddTransient<IPatient, Patients>();
builder.Services.AddTransient<ISecurity, SecurityManager>();
builder.Services.AddTransient<IQueue, Queues>();
builder.Services.AddTransient<IAppointmentReason, AppointmentReasons>();
builder.Services.AddTransient<IClinic, Clinics>();
builder.Services.AddTransient<IClinicConsulting, ClinicConsultings>();
builder.Services.AddTransient<IClient, Clients>();
builder.Services.AddTransient<IBudget, Budgets>();


var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
}
app.UseStaticFiles();

app.UseRouting();

app.UseAuthorization();
app.UseSession();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Login}/{action=Index}/{id?}");

app.Run();
