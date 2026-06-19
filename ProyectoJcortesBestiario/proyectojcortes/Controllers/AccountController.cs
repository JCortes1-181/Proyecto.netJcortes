using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using System.Security.Claims;
using Microsoft.EntityFrameworkCore;
using proyectojcortes.Data;
using proyectojcortes.Models;

namespace proyectojcortes.Controllers
{
    public class AccountController : Controller
    {
        private readonly AppDbContext _context;

        public AccountController(AppDbContext context)
        {
            _context = context;
        }

        // ==========================================
        // VISTA Y PROCESO DE REGISTRO
        // ==========================================
        [HttpGet]
        public IActionResult Register()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Register(Usuario usuario)
        {
            if (ModelState.IsValid)
            {
                // Al crearse, se asegura de que IsDeleted sea false
                usuario.IsDeleted = false;

                _context.Usuarios.Add(usuario);
                await _context.SaveChangesAsync();

                return RedirectToAction("Login");
            }
            return View(usuario);
        }

        // ==========================================
        // VISTA Y PROCESO DE LOGIN
        // ==========================================
        [HttpGet]
        public IActionResult Login()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Login(string correo, string contrasena)
        {
            // Buscamos al usuario por correo, contraseña Y que NO esté borrado lógicamente
            var user = await _context.Usuarios
                .FirstOrDefaultAsync(u => u.Correo == correo && u.Contrasena == contrasena && !u.IsDeleted);

            if (user != null)
            {
                // Creamos la identidad del usuario para la sesión
                var claims = new List<Claim>
            {
                new Claim(ClaimTypes.Name, user.NombreUsuario), // <- Usar NombreUsuario
                new Claim(ClaimTypes.Email, user.Correo)
            };

                var claimsIdentity = new ClaimsIdentity(claims, CookieAuthenticationDefaults.AuthenticationScheme);

                // Guardamos la cookie encriptada en el navegador
                await HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme, new ClaimsPrincipal(claimsIdentity));

                return RedirectToAction("Index", "Home");
            }

            // Mensaje en caso de datos inválidos o cuenta deshabilitada
            ViewBag.Error = "El correo o la contraseña son incorrectos, o la cuenta no existe.";
            return View();
        }

        // ==========================================
        // CERRAR SESIÓN
        // ==========================================
        public async Task<IActionResult> Logout()
        {
            await HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);
            return RedirectToAction("Login");
        }
    }
}
