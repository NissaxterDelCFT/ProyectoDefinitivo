using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using ProyectoDefinitivo.Models;

namespace ProyectoDefinitivo.Controllers
{
    public class NotasController : Controller
    {
        private readonly SystemcftContext _context;

        public NotasController(SystemcftContext context)
        {
            _context = context;
        }

        // GET: Notas
        public async Task<IActionResult> Index()
        {
            var systemcftContext = _context.Nota.Include(n => n.Asignatura).Include(n => n.Estudiantes);
            return View(await systemcftContext.ToListAsync());
        }

        // GET: Notas/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.Nota == null)
            {
                return NotFound();
            }

            var nota = await _context.Nota
                .Include(n => n.Asignatura)
                .Include(n => n.Estudiantes)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (nota == null)
            {
                return NotFound();
            }

            return View(nota);
        }

        // GET: Notas/Create
        public IActionResult Create()
        {
            ViewData["Asignaturaid"] = new SelectList(_context.Asignaturas, "Id", "Id");
            ViewData["Estudiantesid"] = new SelectList(_context.Estudiantes, "Id", "Id");
            return View();
        }

        // POST: Notas/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Ponderacion,Calificacion,Fecharegistro,Estudiantesid,Asignaturaid")] Nota nota)
        {
            if (nota.Calificacion >= 1 && nota.Ponderacion >= 1 && nota.Calificacion != 0 && nota.Estudiantesid != 0 && nota.Asignaturaid != 0)
            {
                _context.Add(nota);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["Asignaturaid"] = new SelectList(_context.Asignaturas, "Id", "Id", nota.Asignaturaid);
            ViewData["Estudiantesid"] = new SelectList(_context.Estudiantes, "Id", "Id", nota.Estudiantesid);
            return View(nota);
        }

        // GET: Notas/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.Nota == null)
            {
                return NotFound();
            }

            var nota = await _context.Nota.FindAsync(id);
            if (nota == null)
            {
                return NotFound();
            }
            ViewData["Asignaturaid"] = new SelectList(_context.Asignaturas, "Id", "Id", nota.Asignaturaid);
            ViewData["Estudiantesid"] = new SelectList(_context.Estudiantes, "Id", "Id", nota.Estudiantesid);
            return View(nota);
        }

        // POST: Notas/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Ponderacion,Calificacion,Fecharegistro,Estudiantesid,Asignaturaid")] Nota nota)
        {
            if (id != nota.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(nota);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!NotaExists(nota.Id))
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
            ViewData["Asignaturaid"] = new SelectList(_context.Asignaturas, "Id", "Id", nota.Asignaturaid);
            ViewData["Estudiantesid"] = new SelectList(_context.Estudiantes, "Id", "Id", nota.Estudiantesid);
            return View(nota);
        }

        // GET: Notas/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.Nota == null)
            {
                return NotFound();
            }

            var nota = await _context.Nota
                .Include(n => n.Asignatura)
                .Include(n => n.Estudiantes)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (nota == null)
            {
                return NotFound();
            }

            return View(nota);
        }

        // POST: Notas/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.Nota == null)
            {
                return Problem("Entity set 'SystemcftContext.Nota'  is null.");
            }
            var nota = await _context.Nota.FindAsync(id);
            if (nota != null)
            {
                _context.Nota.Remove(nota);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool NotaExists(int id)
        {
          return (_context.Nota?.Any(e => e.Id == id)).GetValueOrDefault();
        }
    }
}
