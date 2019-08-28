using Gamayun.Identity;
using Gamayun.Infrastucture;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Gamayun.Infrastucture.Command;
using Microsoft.AspNetCore.Identity;
using Gamayun.Infrastucture.Query;
using AutoMapper;
using Gamayun.Infrastucture.Query.Admin;
using Gamayun.Infrastucture.Grid.ResultModels;
using Gamayun.UI.Utilities;
using Gamayun.Infrastucture.Utilities;
using Gamayun.Infrastucture.Command.Admin;
using Gamayun.Infrastucture.Query.Teacher;
using Gamayun.Infrastucture.Command.Teacher;

namespace Gamayun.UI
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
                       
            services.Configure<CookiePolicyOptions>(options =>
            {
                // This lambda determines whether user consent for non-essential cookies is needed for a given request.
                options.CheckConsentNeeded = context => true;
                options.MinimumSameSitePolicy = SameSiteMode.None;
            });
            services.AddDbContext<GamayunDbContext>(opts => opts.UseSqlServer(Configuration["ConnectionStrings:Gamayun"]));
            services.AddIdentity<AppUser, IdentityRole>()
                .AddEntityFrameworkStores<GamayunDbContext>()
                .AddDefaultTokenProviders()
                .AddClaimsPrincipalFactory<AppUserClaimsPrincipalFactory>();
            
            services.AddSingleton<ISettings, Settings>();
            services.AddScoped<ICommandRunner, CommandRunner>();
            services.AddScoped<IGridQueryRunner, GridQueryRunner>();
            services.AddScoped<ICommandHandler<CreateUserCommandHandler.AdminCommand>, CreateUserCommandHandler>();
            services.AddScoped<ICommandHandler<CreateUserCommandHandler.StudentCommand>, CreateUserCommandHandler>();
            services.AddScoped<ICommandHandler<CreateUserCommandHandler.TeacherCommand>, CreateUserCommandHandler>();
            services.AddScoped<ICommandHandler<EditUserCommandHandler.AdminCommand>, EditUserCommandHandler>();
            services.AddScoped<ICommandHandler<EditUserCommandHandler.StudentCommand>, EditUserCommandHandler>();
            services.AddScoped<ICommandHandler<EditUserCommandHandler.TeacherCommand>, EditUserCommandHandler>();
            services.AddScoped<ICommandHandler<CreateSemesterCommandHandler.Command>, CreateSemesterCommandHandler>();
            services.AddScoped<ICommandHandler<EditSemesterCommandHandler.Command>, EditSemesterCommandHandler>();
            services.AddScoped<ICommandHandler<CreateTopicCommandHandler.Command>, CreateTopicCommandHandler>();
            services.AddScoped<ICommandHandler<EditTopicCommandHandler.Command>, EditTopicCommandHandler>();
            services.AddScoped<ICommandHandler<CreateSectionCommandHandler.Command>, CreateSectionCommandHandler>();
            services.AddScoped<ICommandHandler<EditSectionCommandHandler.Command>, EditSectionCommandHandler>();
            
            services.AddScoped<IGridQueryHandler<UserRM, TeachersQueryHandler.Query>, TeachersQueryHandler>();
            services.AddScoped<IGridQueryHandler<UserRM, AdminsQueryHandler.Query>, AdminsQueryHandler>();
            services.AddScoped<IGridQueryHandler<UserRM, StudentsQueryHandler.Query>, StudentsQueryHandler>();
            services.AddScoped<IGridQueryHandler<SemesterRM, SemestersQueryHandler.Query>, SemestersQueryHandler>();
            services.AddScoped<IGridQueryHandler<TopicRM, TopicsQueryHandler.Query>, TopicsQueryHandler>();
            services.AddScoped<IGridQueryHandler<SectionRM, SectionsQueryHandler.Query>, SectionsQueryHandler>();
            services.AddScoped<IGridQueryHandler<UserRM, StudentsForSectionQueryHandler.Query>, StudentsForSectionQueryHandler>();
            

            var autoMapperConfig = AutomapperService.Initialize();
            services.AddSingleton(autoMapperConfig);
            
            services.ConfigureApplicationCookie(opt =>
            {
                opt.LoginPath = "/Account/Login";
                opt.AccessDeniedPath = "/Account/AccessDenied";
            });

            services.AddMvc().SetCompatibilityVersion(CompatibilityVersion.Version_2_2);
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            if (env.IsDevelopment())
            {
                using (var scope = app.ApplicationServices.GetRequiredService<IServiceScopeFactory>().CreateScope())
                {
                    DataSeeder.Seed(scope.ServiceProvider);
                }
            }
            else
            {
                app.UseExceptionHandler("/Home/Error");
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
            }

            app.UseHttpsRedirection();
            app.UseStaticFiles();
            app.UseCookiePolicy();
            app.UseAuthentication();
            app.UseMvc(routes =>
            {
                routes.MapRoute(
                    "default",
                    "{controller=Account}/{action=Login}"
                    );
                routes.MapRoute(
                    "area",
                    "{area:exists}/{controller=Home}/{action=Index}"
                    );
            });
        }
    }
}
