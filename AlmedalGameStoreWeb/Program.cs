using AlmedalGameStore.DataAccess;
using AlmedalGameStore.DataAccess.GenericRepository;
using AlmedalGameStore.DataAccess.GenericRepository.IGenericRepository;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews();
//Det säger att vi använder SQL server och hämtar connectionstring i appSettings med hjälp av DefaultConnection
//inuti ett block som heter ConnectionStrings
builder.Services.AddDbContext<ApplicationDbContext>(options => options.UseSqlServer(
    builder.Configuration.GetConnectionString("DefaultConnection")
    ));
//Whenever we request a object of IUnitOFWork, ger de oss implementation som vi definierat inuti UnitOfWork,
//bra för dependicy injections
builder.Services.AddScoped<IUnitOfWork, UnitOfWork>();
builder.Services.AddRazorPages().AddRazorRuntimeCompilation();
var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

app.UseAuthorization();
////However the Application's Startup code may require additional changes for things to work end to end.
//Add the following code to the Configure method in your Application's Startup class if not already done:
app.MapControllerRoute(
    name: "default",
    pattern: "{area=Guest}/{controller=Home}/{action=Index}/{id?}");

app.Run();
