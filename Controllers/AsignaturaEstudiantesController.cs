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
    public class AsignaturaEstudiantesController : Controller
    {
        private readonly SystemcftContext _context;

        public AsignaturaEstudiantesController(SystemcftContext context)
        {
            _context = context;
        }

        // GET: AsignaturaEstudiantes
        public async Task<IActionResult> Index()
        {
            var systemcftContext = _context.AsignaturaEstudiantes.Include(a => a.Asignatura).Include(a => a.Estudiantes);
            return View(await systemcftContext.ToListAsync());
        }

        // GET: AsignaturaEstudiantes/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.AsignaturaEstudiantes == null)
            {
                return NotFound();
            }

            var asignaturaEstudiante = await _context.AsignaturaEstudiantes
                .Include(a => a.Asignatura)
                .Include(a => a.Estudiantes)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (asignaturaEstudiante == null)
            {
                return NotFound();
            }

            return View(asignaturaEstudiante);
        }

        // GET: AsignaturaEstudiantes/Create
        public IActionResult Create()
        {
            ViewData["Asignaturaid"] = new SelectList(_context.Asignaturas, "Id", "Id");
            ViewData["Estudiantesid"] = new SelectList(_context.Estudiantes, "Id", "Id");
            return View();
        }

        // POST: AsignaturaEstudiantes/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Asignaturaid,Estudiantesid,Fecharegistro")] AsignaturaEstudiante asignaturaEstudiante)
        {
            if (asignaturaEstudiante.Estudiantesid != 0 && asignaturaEstudiante.Asignaturaid != 0)
            {
                _context.AsignaturaEstudiantes.Add(asignaturaEstudiante);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["Asignaturaid"] = new SelectList(_context.Asignaturas, "Id", "Id", asignaturaEstudiante.Asignaturaid);
            ViewData["Estudiantesid"] = new SelectList(_context.Estudiantes, "Id", "Id", asignaturaEstudiante.Estudiantesid);
            return View(asignaturaEstudiante);
        }

        // GET: AsignaturaEstudiantes/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.AsignaturaEstudiantes == null)
            {
                return NotFound();
            }

            var asignaturaEstudiante = await _context.AsignaturaEstudiantes.FindAsync(id);
            if (asignaturaEstudiante == null)
            {
                return NotFound();
            }
            ViewData["Asignaturaid"] = new SelectList(_context.Asignaturas, "Id", "Id", asignaturaEstudiante.Asignaturaid);
            ViewData["Estudiantesid"] = new SelectList(_context.Estudiantes, "Id", "Id", asignaturaEstudiante.Estudiantesid);
            return View(asignaturaEstudiante);
        }

        // POST: AsignaturaEstudiantes/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Asignaturaid,Estudiantesid,Fecharegistro")] AsignaturaEstudiante asignaturaEstudiante)
        {
            if (id != asignaturaEstudiante.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(asignaturaEstudiante);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!AsignaturaEstudianteExists(asignaturaEstudiante.Id))
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
            ViewData["Asignaturaid"] = new SelectList(_context.Asignaturas, "Id", "Id", asignaturaEstudiante.Asignaturaid);
            ViewData["Estudiantesid"] = new SelectList(_context.Estudiantes, "Id", "Id", asignaturaEstudiante.Estudiantesid);
            return View(asignaturaEstudiante);
        }

        // GET: AsignaturaEstudiantes/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.AsignaturaEstudiantes == null)
            {
                return NotFound();
            }

            var asignaturaEstudiante = await _context.AsignaturaEstudiantes
                .Include(a => a.Asignatura)
                .Include(a => a.Estudiantes)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (asignaturaEstudiante == null)
            {
                return NotFound();
            }

            return View(asignaturaEstudiante);
        }

        // POST: AsignaturaEstudiantes/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.AsignaturaEstudiantes == null)
            {
                return Problem("Entity set 'SystemcftContext.AsignaturaEstudiantes'  is null.");
            }
            var asignaturaEstudiante = await _context.AsignaturaEstudiantes.FindAsync(id);
            if (asignaturaEstudiante != null)
            {
                _context.AsignaturaEstudiantes.Remove(asignaturaEstudiante);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool AsignaturaEstudianteExists(int id)
        {
          return (_context.AsignaturaEstudiantes?.Any(e => e.Id == id)).GetValueOrDefault();
        }
    }
}
