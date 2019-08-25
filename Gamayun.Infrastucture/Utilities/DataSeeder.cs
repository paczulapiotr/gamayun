using Microsoft.AspNetCore.Identity;
using System;
using Microsoft.Extensions.DependencyInjection;
using System.Linq;
using Gamayun.Identity;
using Gamayun.Infrastucture.Entities;

namespace Gamayun.Infrastucture.Utilities
{
    public static class DataSeeder
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
            var dbContext = serviceProvider.GetService<GamayunDbContext>();
            var userManager = serviceProvider.GetService<UserManager<AppUser>>();
            var passwordHasher = userManager.PasswordHasher;
            if(userManager.Users.Any())
                return;

            var student = new AppUser { 
                UserName = "student", 
                Email = "student@local.host",
                EmailConfirmed = true,
                FirstName = "Tommie",
                LastName = "Buckner",
            };
            student.PasswordHash = passwordHasher.HashPassword(student, "Student1!");
            var studentB = new AppUser
            {
                UserName = "bstudent",
                Email = "bstudent@local.host",
                EmailConfirmed = true,
                FirstName = "Reon",
                LastName = "Sexton",
            };
            studentB.PasswordHash = passwordHasher.HashPassword(studentB, "Student1!");

            var admin = new AppUser
            {
                UserName = "admin",
                Email = "admin@local.host",
                EmailConfirmed = true,
                FirstName = "Hasan",
                LastName = "Mcloughlin",
            };
            admin.PasswordHash = passwordHasher.HashPassword(admin, "Admin1!");
            var adminB = new AppUser
            {
                UserName = "badmin",
                Email = "badmin@local.host",
                EmailConfirmed = true,
                FirstName = "Charlotte",
                LastName = "Herrera",
            };
            adminB.PasswordHash = passwordHasher.HashPassword(adminB, "Admin1!");

            var teacher = new AppUser
            {
                UserName = "teacher",
                Email = "teacher@local.host",
                EmailConfirmed = true,
                FirstName = "Mary",
                LastName = "Yoder",
            };
            teacher.PasswordHash = passwordHasher.HashPassword(teacher, "Teacher1!");

            var teacherB = new AppUser
            {
                UserName = "bteacher",
                Email = "bteacher@local.host",
                EmailConfirmed = true,
                FirstName = "Gary",
                LastName = "Golden",
            };
            teacher.PasswordHash = passwordHasher.HashPassword(teacherB, "Teacher1!");

            userManager.CreateAsync(admin).Wait();
            userManager.CreateAsync(adminB).Wait();
            userManager.CreateAsync(teacher).Wait();
            userManager.CreateAsync(teacherB).Wait();
            userManager.CreateAsync(student).Wait();
            userManager.CreateAsync(studentB).Wait();

            userManager.AddToRoleAsync(admin, AppRoles.Admin).Wait();
            userManager.AddToRoleAsync(adminB, AppRoles.Admin).Wait();
            userManager.AddToRoleAsync(teacher, AppRoles.Teacher).Wait();
            userManager.AddToRoleAsync(teacherB, AppRoles.Teacher).Wait();
            userManager.AddToRoleAsync(student, AppRoles.Student).Wait();
            userManager.AddToRoleAsync(studentB, AppRoles.Student).Wait();
            var studentEntity = new Student
            {
                AppUser = student,
            };
            var teacherEntity = new Teacher
            {
                AppUser = teacher,
            };
            var adminEntity = new Admin
            {
                AppUser = admin,
            };
            var studentBEntity = new Student
            {
                AppUser = studentB,
            };
            var teacherBEntity = new Teacher
            {
                AppUser = teacherB,
            };
            var adminBEntity = new Admin
            {
                AppUser = adminB,
            };
            dbContext.Add(studentEntity);
            dbContext.Add(studentBEntity);
            dbContext.Add(teacherEntity);
            dbContext.Add(teacherBEntity);
            dbContext.Add(adminEntity);
            dbContext.Add(adminBEntity);
            dbContext.SaveChanges();
        }

    }
}
