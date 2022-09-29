using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using SocialSoluctionMVC.Data.Context;
using SocialSoluctionMVC.Entities;
using SocialSoluctionMVC.Entities.Enums;
using SocialSoluctionMVC.Utils;

namespace SocialSoluctionMVC.Controllers
{
    public class AnunciosController : Controller
    {
        private readonly SocialSoluctionContext _context;

        public AnunciosController(SocialSoluctionContext context)
        {
            _context = context;
        }

        // GET: Anuncios
        public async Task<IActionResult> Index()
        {
            var socialSoluctionContext = _context.Anuncios.Include(a => a.Cliente);
            ViewData["Tipos"] = new SelectList(TipoAnuncio.TiposAnuncios(), "Id", "Nome");
            return View(await socialSoluctionContext.ToListAsync());
        }

        [HttpPost]
        public async Task<IActionResult> Index(int tipo, string searchText)
        {
            var query = from a in _context.Anuncios.Include(a => a.Cliente)
                        select a;
            if (!string.IsNullOrEmpty(searchText))
            {
                query = query.Where(a => a.Cliente.Nome.ToLower().Contains(searchText.ToLower()));
            }

            if (tipo > 0)
            {
                query = query.Where(a => a.Tipo == tipo);
            }

            var tipos = new SelectList(TipoAnuncio.TiposAnuncios(), "Id", "Nome");
            foreach (var t in tipos)
            {
                if(t.Value.Equals(tipo.ToString()))
                {
                    t.Selected = true;
                }
            }
            ViewData["Tipos"] = tipos;
            ViewData["searchText"] = searchText;
            return View(await query.ToListAsync());
        }

        // GET: Anuncios/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var anuncio = await _context.Anuncios
                .Include(a => a.Cliente)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (anuncio == null)
            {
                return NotFound();
            }

            ViewData["tipo"] = TipoAnuncio.TipoNome(anuncio.Tipo);
            return View(anuncio);
        }

        // GET: Anuncios/Create
        public IActionResult Create()
        {
            ViewData["Clientes"] = new SelectList(_context.Clientes, "Id", "Nome");
            ViewData["Tipo"] = new SelectList(TipoAnuncio.TiposAnuncios(), "Id", "Nome");
            return View();
        }

        // POST: Anuncios/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Tipo,ClienteId,Valor,Publicacao,Descricao,CEP,Logradouro,Complemento,Bairro,Localidade,UF,Id")] Anuncio anuncio)
        {
            if (ModelState.IsValid)
            {
                _context.Add(anuncio);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["Clientes"] = new SelectList(_context.Clientes, "Id", "Nome");
            ViewData["Tipo"] = new SelectList(TipoAnuncio.TiposAnuncios(), "Id", "Nome");
            return View(anuncio);
        }

        // GET: Anuncios/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var anuncio = await _context.Anuncios.FindAsync(id);
            if (anuncio == null)
            {
                return NotFound();
            }
            ViewData["Clientes"] = new SelectList(_context.Clientes, "Id", "Nome");
            ViewData["Tipo"] = new SelectList(TipoAnuncio.TiposAnuncios(), "Id", "Nome");
            return View(anuncio);
        }

        // POST: Anuncios/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Tipo,ClienteId,Valor,Publicacao,Descricao,CEP,Logradouro,Complemento,Bairro,Localidade,UF,Id")] Anuncio anuncio)
        {
            if (id != anuncio.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(anuncio);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!AnuncioExists(anuncio.Id))
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
            ViewData["Clientes"] = new SelectList(_context.Clientes, "Id", "Nome");
            ViewData["Tipo"] = new SelectList(TipoAnuncio.TiposAnuncios(), "Id", "Nome");
            return View(anuncio);
        }

        // GET: Anuncios/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var anuncio = await _context.Anuncios
                .Include(a => a.Cliente)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (anuncio == null)
            {
                return NotFound();
            }
            ViewData["tipo"] = TipoAnuncio.TipoNome(anuncio.Tipo);
            return View(anuncio);
        }

        // POST: Anuncios/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var anuncio = await _context.Anuncios.FindAsync(id);
            _context.Anuncios.Remove(anuncio);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool AnuncioExists(int id)
        {
            return _context.Anuncios.Any(e => e.Id == id);
        }
    }
}
