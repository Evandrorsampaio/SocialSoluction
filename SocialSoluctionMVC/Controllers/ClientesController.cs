using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using SocialSoluctionMVC.Data.Context;
using SocialSoluctionMVC.Entities;
using SocialSoluctionMVC.Utils;

namespace SocialSoluctionMVC.Controllers
{
    public class ClientesController : Controller
    {
        private readonly SocialSoluctionContext _context;

        public ClientesController(SocialSoluctionContext context)
        {
            _context = context;
        }

        // GET: Clientes
        public async Task<IActionResult> Index()
        {
            return View(await _context.Clientes.ToListAsync());
        }

        [HttpPost]
        public async Task<IActionResult> Index(string searchText)
        {
            var query = from c in _context.Clientes
                        select c;

            if (!string.IsNullOrEmpty(searchText))
            {
                query = query.Where(c => 
                    c.Nome.ToLower().Contains(searchText.ToLower())
                    || c.Email.ToLower().Contains(searchText.ToLower())
                    || c.CPFCNPJ.ToLower().Contains(searchText.ToLower())
                );
            }
            TempData["searchText"] = searchText;
            return View(await query.ToListAsync());
        }

        // GET: Clientes/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var cliente = await _context.Clientes
                .FirstOrDefaultAsync(m => m.Id == id);
            if (cliente == null)
            {
                return NotFound();
            }

            return View(cliente);
        }

        // GET: Clientes/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Clientes/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Nome,Email,CPFCNPJ,Id")] Cliente cliente)
        {
            if (ModelState.IsValid)
            {
                cliente.CPFCNPJ = cliente.CPFCNPJ.Replace(".", "").Replace("-", "").Replace("/", "");
                _context.Add(cliente);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(cliente);
        }

        // GET: Clientes/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var cliente = await _context.Clientes.FindAsync(id);
            if (cliente == null)
            {
                return NotFound();
            }
            return View(cliente);
        }

        // POST: Clientes/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Nome,Email,CPFCNPJ,Id")] Cliente cliente)
        {
            if (id != cliente.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    cliente.CPFCNPJ = cliente.CPFCNPJ.Replace(".", "").Replace("-", "").Replace("/", "");
                    _context.Update(cliente);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!ClienteExists(cliente.Id))
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
            return View(cliente);
        }

        // GET: Clientes/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var cliente = await _context.Clientes.Include(c => c.Anuncios)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (cliente == null)
            {
                return NotFound();
            }

            ViewData["TemImovel"] = (cliente.Anuncios.Count > 0);
            return View(cliente);
        }

        // POST: Clientes/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var cliente = await _context.Clientes.FindAsync(id);
            _context.Clientes.Remove(cliente);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool ClienteExists(int id)
        {
            return _context.Clientes.Any(e => e.Id == id);
        }

        public bool ValidaCPFCNPJ(string id)
        {
            if (string.IsNullOrEmpty(id)) { 
                return false; 
            }   

            var isCNPJ = Validacao.ValidaCNPJ(id);
            var isCPF = Validacao.ValidaCPF(id);

            return isCNPJ || isCPF;
        }

        [HttpPost]
        public bool ValidaCPFExistente(int id, string CPFCNPJ)
        {
            if(string.IsNullOrEmpty(CPFCNPJ))
            {
                return false;
            }

            
            var query = _context.Clientes
                        .Where(c => c.CPFCNPJ.Equals(CPFCNPJ));
            if(id > 0)
            {
                query = query.Where(c => c.Id != id);
            }
            
            var qtdClientes = query.Count();

            return qtdClientes == 0;
        }
    }
}
