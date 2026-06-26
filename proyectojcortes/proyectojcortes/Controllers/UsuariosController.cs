
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using proyectojcortes.Data;
using proyectojcortes.Models;
using System;
using System.Threading.Tasks;
using System.Linq;

[Authorize(Roles = "Admin")]
public class UsuariosController : Controller
{
    private readonly AppDbContext _context;

    public UsuariosController(AppDbContext context)
    {
        _context = context;
    }

    // Este método debe estar DENTRO de la clase, debajo del constructor o antes de los métodos de acción
    private async Task RegistrarAccion(string accion, int usuarioAfectadoId)
    {
        var nombreAdmin = User.Identity?.Name;
        var admin = await _context.Usuarios.FirstOrDefaultAsync(u => u.NombreUsuario == nombreAdmin);

        if (admin != null)
        {
            var log = new Registro
            {
                UsuarioId = admin.Id,
                CriaturaId = null, // Usamos null para que la base de datos no pida una criatura obligatoria
                Notas = $"Acción: {accion} sobre usuario ID: {usuarioAfectadoId}",
                EstaDerrotado = false,
                FechaCreacion = DateTime.Now
            };
            _context.Add(log);
            await _context.SaveChangesAsync();
        }
    }

    public async Task<IActionResult> Index()
    {
        return View(await _context.Usuarios.Where(u => !u.IsDeleted).ToListAsync());
    }

    public async Task<IActionResult> Details(int? id)
    {
        if (id == null) return NotFound();
        var usuario = await _context.Usuarios.FirstOrDefaultAsync(m => m.Id == id);
        return usuario == null ? NotFound() : View(usuario);
    }

    public IActionResult Create() => View();

    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Create([Bind("Id,NombreUsuario,Correo,Contrasena,FechaCreacion")] Usuario usuario)
    {
        if (ModelState.IsValid)
        {
            _context.Add(usuario);
            await _context.SaveChangesAsync();
            await RegistrarAccion("CREAR", usuario.Id);
            return RedirectToAction(nameof(Index));
        }
        return View(usuario);
    }

    [Authorize(Roles = "Admin")]
    public async Task<IActionResult> Edit(int? id)
    {
        if (id == null) return NotFound();
        var usuario = await _context.Usuarios.FindAsync(id);
        return usuario == null ? NotFound() : View(usuario);
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    [Authorize(Roles = "Admin")]
    public async Task<IActionResult> Edit(int? id, [Bind("Id,NombreUsuario,Correo,Contrasena,FechaCreacion")] Usuario usuario)
    {
        if (id != usuario.Id) return NotFound();
        if (ModelState.IsValid)
        {
            _context.Update(usuario);
            await _context.SaveChangesAsync();
            await RegistrarAccion("EDITAR", usuario.Id);
            return RedirectToAction(nameof(Index));
        }
        return View(usuario);
    }

    [Authorize(Roles = "Admin")]
    public async Task<IActionResult> Delete(int? id)
    {
        if (id == null) return NotFound();
        var usuario = await _context.Usuarios.FirstOrDefaultAsync(m => m.Id == id);
        return usuario == null ? NotFound() : View(usuario);
    }

    [HttpPost, ActionName("Delete")]
    [ValidateAntiForgeryToken]
    [Authorize(Roles = "Admin")]
    public async Task<IActionResult> DeleteConfirmed(int? id)
    {
        var usuario = await _context.Usuarios.FindAsync(id);
        if (usuario != null)
        {
            usuario.IsDeleted = true;
            _context.Usuarios.Update(usuario);
            await _context.SaveChangesAsync();
            await RegistrarAccion("BORRAR", usuario.Id);
        }
        return RedirectToAction(nameof(Index));
    }

    private bool UsuarioExists(int? id) => _context.Usuarios.Any(e => e.Id == id);
}