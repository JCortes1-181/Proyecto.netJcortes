
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using proyectojcortes.Data;
using proyectojcortes.Models;

[Authorize]
public class RegistroController : Controller
{
    private readonly AppDbContext _context;

    public RegistroController(AppDbContext context)
    {
        _context = context;
    }

    // GET: REGISTROS
    public async Task<IActionResult> Index()    
    {
        return View(await _context.Registros.ToListAsync());
    }

    // GET: REGISTROS/Details/5
    public async Task<IActionResult> Details(int? id)
    {
        if (id == null)
        {
            return NotFound();
        }

        var registro = await _context.Registros
            .FirstOrDefaultAsync(m => m.Id == id);
        if (registro == null)
        {
            return NotFound();
        }

        return View(registro);
    }

    // GET: REGISTROS/Create
    public IActionResult Create()
    {
        return View();
    }

    // POST: REGISTROS/Create
    // To protect from overposting attacks, enable the specific properties you want to bind to.
    // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Create([Bind("Id,UsuarioId,CriaturaId,CantidadDerrotas,Notas,EstaDerrotado,FechaCreacion,Usuario,Criatura")] Registro registro)
    {
        if (ModelState.IsValid)
        {
            _context.Add(registro);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }
        return View(registro);
    }

    // GET: REGISTROS/Edit/5
    public async Task<IActionResult> Edit(int? id)
    {
        if (id == null)
        {
            return NotFound();
        }

        var registro = await _context.Registros.FindAsync(id);
        if (registro == null)
        {
            return NotFound();
        }
        return View(registro);
    }

    // POST: REGISTROS/Edit/5
    // To protect from overposting attacks, enable the specific properties you want to bind to.
    // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Edit(int? id, [Bind("Id,UsuarioId,CriaturaId,CantidadDerrotas,Notas,EstaDerrotado,FechaCreacion,Usuario,Criatura")] Registro registro)
    {
        if (id != registro.Id)
        {
            return NotFound();
        }

        if (ModelState.IsValid)
        {
            try
            {
                _context.Update(registro);
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!RegistroExists(registro.Id))
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
        return View(registro);
    }

    // GET: REGISTROS/Delete/5
    public async Task<IActionResult> Delete(int? id)
    {
        if (id == null)
        {
            return NotFound();
        }

        var registro = await _context.Registros
            .FirstOrDefaultAsync(m => m.Id == id);
        if (registro == null)
        {
            return NotFound();
        }

        return View(registro);
    }

    // POST: Registro/Delete/5
    [HttpPost, ActionName("Delete")]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> DeleteConfirmed(int id)
    {
        var registro = await _context.Registros.FindAsync(id);
        if (registro != null)
        {
            registro.IsDeleted = true; 
            _context.Registros.Update(registro); 
        }

        await _context.SaveChangesAsync();
        return RedirectToAction(nameof(Index));
    }

    private bool RegistroExists(int? id)
    {
        return _context.Registros.Any(e => e.Id == id);
    }
}
