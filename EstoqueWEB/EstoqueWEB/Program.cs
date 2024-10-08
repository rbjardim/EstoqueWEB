using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using EstoqueWEB.Model;
using EstoqueWEB.MySqlContext;
using Microsoft.EntityFrameworkCore;
using EstoqueWEB.Interface.Service;
using EstoqueWEB.Service;
using EstoqueWEB.Interface.Repository;
using EstoqueWEB.Repository;
using EstoqueWEB.Service.EstoqueWEB.Service;

public class Program
{
    public static void Main(string[] args)
    {
        CreateHostBuilder(args).Build().Run();
    }

    public static IHostBuilder CreateHostBuilder(string[] args) =>
        Host.CreateDefaultBuilder(args)
            .ConfigureWebHostDefaults(webBuilder =>
            {
                webBuilder.UseStartup<Startup>();
            });
}

public class Startup
{
    public Startup(IConfiguration configuration)
    {
        Configuration = configuration;
    }

    public IConfiguration Configuration { get; }

    public void ConfigureServices(IServiceCollection services)
    {
        services.AddDbContext<Context>(options =>
            options.UseMySql(Configuration.GetConnectionString("DefaultConnection"),
                new MySqlServerVersion(new Version(8, 0, 21))));

        services.AddIdentity<AplicationUser, IdentityRole>(options =>

        {
            options.Password.RequireDigit = true;
            options.Password.RequiredLength = 12;
            options.Password.RequireNonAlphanumeric = true;
            options.Password.RequireUppercase = true;
            options.Password.RequireLowercase = true;
        })
        .AddRoles<IdentityRole>()
        .AddEntityFrameworkStores<Context>()
        .AddDefaultTokenProviders();

        services.AddScoped<IRoleInitializer, RoleInitializer>();
        services.AddScoped<IEstoqueService, EstoqueService>();
        services.AddScoped<IEstoqueRepository, EstoqueRepository>();
        services.AddScoped<IDevolucaoService, DevolucaoService>();
        services.AddScoped<IDevolucaoRepository, DevolucaoRepository>();
        services.AddScoped<ICelularService, CelularService>();
        services.AddScoped<ICelularRepository, CelularRepository>();


        services.AddScoped<IUserService, UserService>();

        services.AddRazorPages();
    }

    public void Configure(IApplicationBuilder app, IWebHostEnvironment env, IRoleInitializer roleInitializer)
    {
        if (env.IsDevelopment())
        {
            app.UseDeveloperExceptionPage();
        }
        else
        {
            app.UseExceptionHandler("/Error");
            app.UseHsts();
        }

        app.UseHttpsRedirection();
        app.UseStaticFiles();

        app.UseRouting();

        app.UseAuthentication();
        app.UseAuthorization();

        roleInitializer.InitializeRoles().Wait();

        app.UseEndpoints(endpoints =>
        {
            endpoints.MapRazorPages();
        });
    }

}

