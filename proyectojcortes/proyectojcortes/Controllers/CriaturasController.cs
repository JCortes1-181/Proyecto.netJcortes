using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using proyectojcortes.Data;
using proyectojcortes.Models;
using System;
using System.Threading.Tasks;
using System.Linq;

[Authorize]
public class CriaturasController : Controller
{
    private readonly AppDbContext _context;

    public CriaturasController(AppDbContext context)
    {
        _context = context;
    }

    // GET: CRIATURAS
    public async Task<IActionResult> Index()
    {
        return View(await _context.Criaturas.ToListAsync());
    }

    // GET: CRIATURAS/Details/5
    public async Task<IActionResult> Details(int? id)
    {
        if (id == null)
        {
            return NotFound();
        }

        var criatura = await _context.Criaturas
            .FirstOrDefaultAsync(m => m.Id == id);
        if (criatura == null)
        {
            return NotFound();
        }

        return View(criatura);
    }

    // GET: CRIATURAS/Create
    public IActionResult Create()
    {
        return View();
    }

    // POST: CRIATURAS/Create
    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Create([Bind("Id,Nombre,Categoria,Bioma,RarezaBase")] Criatura criatura)
    {
        if (ModelState.IsValid)
        {
            _context.Add(criatura);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }
        return View(criatura);
    }

    // GET: CRIATURAS/Edit/5
    public async Task<IActionResult> Edit(int? id)
    {
        if (id == null)
        {
            return NotFound();
        }

        var criatura = await _context.Criaturas.FindAsync(id);
        if (criatura == null)
        {
            return NotFound();
        }
        return View(criatura);
    }

    // POST: CRIATURAS/Edit/5
    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Edit(int? id, [Bind("Id,Nombre,Categoria,Bioma,RarezaBase")] Criatura criatura)
    {
        if (id != criatura.Id)
        {
            return NotFound();
        }

        if (ModelState.IsValid)
        {
            try
            {
                _context.Update(criatura);
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!CriaturaExists(criatura.Id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }
            return RedirectToAction(nameof(Index));
        }
        return View(criatura);
    }

    // GET: CRIATURAS/Delete/5
    public async Task<IActionResult> Delete(int? id)
    {
        if (id == null)
        {
            return NotFound();
        }

        var criatura = await _context.Criaturas
            .FirstOrDefaultAsync(m => m.Id == id);
        if (criatura == null)
        {
            return NotFound();
        }

        return View(criatura);
    }

    // POST: Criaturas/Delete/5
    [HttpPost, ActionName("Delete")]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> DeleteConfirmed(int id)
    {
        var criatura = await _context.Criaturas.FindAsync(id);
        if (criatura != null)
        {
            criatura.IsDeleted = true;
            _context.Criaturas.Update(criatura);
        }

        await _context.SaveChangesAsync();
        return RedirectToAction(nameof(Index));
    }

    // NUEVO MÉTODO PARA REGISTRAR ACCIÓN
    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> MarcarComoDerrotado(int criaturaId)
    {
        var nombreUsuario = User.Identity?.Name;
        var usuario = await _context.Usuarios.FirstOrDefaultAsync(u => u.NombreUsuario == nombreUsuario);

        if (usuario == null) return Unauthorized();

        var registro = new Registro
        {
            UsuarioId = usuario.Id,
            CriaturaId = criaturaId,
            CantidadDerrotas = 1,
            EstaDerrotado = true,
            Notas = "Derrotada automáticamente",
            FechaCreacion = DateTime.Now
        };

        _context.Add(registro);
        await _context.SaveChangesAsync();

        return RedirectToAction("Details", new { id = criaturaId });
    }

    private bool CriaturaExists(int? id)
    {
        return _context.Criaturas.Any(e => e.Id == id);
    }
}