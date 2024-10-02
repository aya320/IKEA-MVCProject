using IKEA.BLL.Models.Common.Services.Attachment;
using IKEA.BLL.Services.Department;
using IKEA.BLL.Services.Employees;
using IKEA.DAL.Entities.Identity;
using IKEA.DAL.Persistance.Data;
using IKEA.DAL.Persistance.UnitOfWork;
using IKEA.PL.Helpers;
using IKEA.PL.Mapping;
using IKEA.PL.Settings;
using Microsoft.AspNetCore.Authentication.Google;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace IKEA.PL
{
	public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container.
            builder.Services.AddControllersWithViews();
            builder.Services.AddDbContext<ApplicationDbContext>(options =>
            {

                options.UseLazyLoadingProxies().UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection"));
            });

            //builder.Services.AddScoped<IDepartmentRepository , DepartmentRepository>();
            builder.Services.AddScoped<IDepartmentService, DepartmentService>();

            //builder.Services.AddScoped<IEmployeeRepository, EmployeeRepository>();
            builder.Services.AddScoped<IEmployeeService, EmployeeService>();

            builder.Services.AddScoped<IUnitOfWork, UnitOfWork>();
            builder.Services.AddTransient<IAttachmentService,AttachmentService >();
			builder.Services.AddTransient<IMailSettings, EMailSettings>();

			builder.Services.AddAutoMapper(m=>m.AddProfile(new MappingProfile()));


			builder.Services.AddIdentity<ApplicationUser, IdentityRole>((options) =>

			{

				options.Password.RequiredLength = 5;

				options.Password.RequireDigit = true;

				options.Password.RequireUppercase = false;

				options.Password.RequireLowercase = true;

				options.User.RequireUniqueEmail = true;

				options.Lockout.AllowedForNewUsers = true;

				options.Lockout.MaxFailedAccessAttempts = 5;

				options.Lockout.DefaultLockoutTimeSpan = TimeSpan.FromDays(5);

 
            }).AddEntityFrameworkStores<ApplicationDbContext>().
            AddTokenProvider<DataProtectorTokenProvider<ApplicationUser>>(TokenOptions.DefaultProvider);

			builder.Services.ConfigureApplicationCookie(Options =>
            {
                Options.LoginPath = "/Account/SignIn";
                Options.LogoutPath= "/Account/SignIn";  
                Options.ExpireTimeSpan = TimeSpan.FromMinutes(5);
                Options.AccessDeniedPath = "/Home/Error";
                //Options.ForwardSignOut= "/Account/SignIn";
            });

			

			builder.Services.Configure<MailSettings>(builder.Configuration.GetSection(nameof(MailSettings)));

			builder.Services.AddAuthentication(o =>
            { 
				o.DefaultAuthenticateScheme = GoogleDefaults.AuthenticationScheme;

				o.DefaultChallengeScheme = GoogleDefaults.AuthenticationScheme;
			}
			).AddGoogle(o =>
			{
               	IConfiguration GoogleAuthSection = builder.Configuration.GetSection("Authentication:Google");
               
               	o.ClientId = GoogleAuthSection["ClientId"];
		        o.ClientSecret = GoogleAuthSection["ClientSecret"];

			});
           			//builder.Services.AddAuthentication(options =>

			//{

			//	options.DefaultAuthenticateScheme = "Identity.Application";

			//	options.DefaultChallengeScheme = "Identity.Application";

			//})

			//               .AddCookie("Both", ".AspNetCore.Both", options =>

			//               {

			//               	options.LoginPath = "/Account/Login";

			//               	options.AccessDeniedPath = "/Home/Error";

			//               	options.ExpireTimeSpan = TimeSpan.FromDays(10); 

			//               options.LogoutPath = "/Account/SignIn";

			//               });


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
            app.UseAuthentication();
            app.UseAuthorization();

            app.MapControllerRoute(
                name: "default",
                pattern: "{controller=Home}/{action=Index}/{id?}");

            app.Run();
        }
    }
}
