using Microsoft.AspNetCore.Identity;
using System;
using Microsoft.Extensions.DependencyInjection;
using System.Text;
using System.Linq;
using System.Threading.Tasks;

namespace Gamayun.Identity
{
    public static class IdentitySeeder
    {
        public static void Seed(IServiceProvider serviceProvider)
        {
            SeedRoles(serviceProvider);
            SeedUsers(serviceProvider);
        }

        private static void SeedRoles(IServiceProvider serviceProvider)
        {
            var roleManager = serviceProvider.GetService<RoleManager<IdentityRole>>();
            if (roleManager.Roles.Any())
                return;

            roleManager.CreateAsync(new IdentityRole(AppRoles.Admin)).Wait();
            roleManager.CreateAsync(new IdentityRole(AppRoles.Student)).Wait();
            roleManager.CreateAsync(new IdentityRole(AppRoles.Teacher)).Wait();
        }

        private static void SeedUsers(IServiceProvider serviceProvider)
        {
            var userManager = serviceProvider.GetService<UserManager<AppUser>>();
            var passwordHasher = userManager.PasswordHasher;
            if(userManager.Users.Any())
                return;

            var student = new AppUser { 
                UserName = "student", 
                Email = "student@local.host",
                EmailConfirmed = true 
            };
            student.PasswordHash = passwordHasher.HashPassword(student, "Student1!");

            var admin = new AppUser
            {
                UserName = "admin",
                Email = "admin@local.host",
                EmailConfirmed = true
            };
            admin.PasswordHash = passwordHasher.HashPassword(student, "Admin!");

            var teacher = new AppUser
            {
                UserName = "teacher",
                Email = "teacher@local.host",
                EmailConfirmed = true
            };
            teacher.PasswordHash = passwordHasher.HashPassword(student, "Teacher!");

            userManager.CreateAsync(admin).Wait();
            userManager.CreateAsync(teacher).Wait();
            userManager.CreateAsync(student).Wait();

            userManager.AddToRoleAsync(admin, AppRoles.Admin).Wait();
            userManager.AddToRoleAsync(teacher, AppRoles.Teacher).Wait();
            userManager.AddToRoleAsync(student, AppRoles.Student).Wait();
        }

    }
}
