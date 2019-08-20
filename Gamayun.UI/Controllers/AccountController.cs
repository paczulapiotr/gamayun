using Gamayun.Identity;
using Gamayun.UI.Models.Account;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System.Linq;
using System.Threading.Tasks;

namespace Gamayun.UI.Controllers
{
    [AllowAnonymous]
    public class AccountController : Controller
    {
        private readonly UserManager<AppUser> _userManager;
        private readonly RoleManager<IdentityRole> _roleManager;
        private readonly SignInManager<AppUser> _signInManager;

        public AccountController(
            UserManager<AppUser> userManager,
            RoleManager<IdentityRole> roleManager,
            SignInManager<AppUser> signInManager)
        {
            _userManager = userManager;
            _roleManager = roleManager;
            _signInManager = signInManager;
        }

        [HttpGet]
        public ViewResult AccessDenied()
        {
            return View();
        }

        [HttpGet]
        public async Task<ActionResult> Login()
        {
            if (_signInManager.IsSignedIn(User))
            {
                var user = await _userManager.GetUserAsync(User);
                var role = (await _userManager.GetRolesAsync(user)).FirstOrDefault();
                if (role != null)
                {
                    return Redirect(role);
                }
                await _signInManager.SignOutAsync();
            }
            return View();
        }

        [HttpPost]
        public async Task<ActionResult> Login(LoginVm vm)
        {
            var user = _userManager.Users.FirstOrDefault(a => a.UserName == vm.Login);
            if (user==null)
            {
                ViewBag.ErrorMessage = "Invalid login credentials";
                return View();
            }

            var result = await _signInManager.CheckPasswordSignInAsync(user, vm.Password, false);
            if(!result.Succeeded)
            {
                ViewBag.ErrorMessage = "Invalid login credentials";
                return View();
            }
            await _signInManager.SignInAsync(user, true);
            var role = (await _userManager.GetRolesAsync(user)).FirstOrDefault();
            
            switch (role)
            {
                case AppRoles.Admin:
                    return Redirect("/Admin");
                case AppRoles.Student:
                    return Redirect("/Student");
                case AppRoles.Teacher:
                    return Redirect("/Teacher");
                default:
                    ViewBag.ErrorMessage = "There has been an error with a user";
                    return View();
            }
        }


        [HttpPost]
        public async Task<ActionResult> Logout()
        {
            await _signInManager.SignOutAsync();
            return RedirectToAction(nameof(Login));
        }

        [HttpGet]
        public ViewResult Register()
        {
            return View();
        }


        [HttpPost]
        public async Task<ViewResult> Register(RegisterVm vm)
        {

            if (vm.Password != vm.RepeatPassword)
            {
                ViewBag.ErrorMessage = "Password must be the matching";
                return View();
            }
            if (vm.Email != vm.RepeatEmail)
            {
                ViewBag.ErrorMessage = "Emails must be the matching";
                return View();
            }
            if (_userManager.Users.Any(u => u.UserName == vm.Login)) 
            {
                ViewBag.ErrorMessage = "Given login is already in use";
                return View();
            }
            var user = new AppUser
            {
                UserName = vm.Login,
                Email = vm.Email,
                EmailConfirmed = true
            };

            var passwordHasher = _userManager.PasswordHasher;
            user.PasswordHash = passwordHasher.HashPassword(user, vm.Password);
            
            var result = await _userManager.CreateAsync(user);
            if (!result.Succeeded)
            {
                ViewBag.ErrorMessage = "Cannot create user with given credentials";
                return View();
            }
            result = await _userManager.AddToRoleAsync(user, AppRoles.Student);

            if(!result.Succeeded)
            {
                ViewBag.ErrorMessage = "Cannot create user with given credentials";
                return View();
            }

            ViewBag.Message = "Registration was succesfull";
            return View(nameof(Login));
        } 


    }
}
