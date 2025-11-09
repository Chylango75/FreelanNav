using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using MvcFreelan.Models.Freelan;
using MvcFreelan.Models.Mypays;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MvcFreelan.Controllers
{
    public class MypaysController : Controller
    {
        private readonly AppDbContext _context;
        private readonly UserManager<IdentityUser> _userManager;

        public MypaysController(
            AppDbContext context,
            UserManager<IdentityUser> userManager
            )
        {
            _context = context;
            _userManager = userManager;
        }

        // GET: Mypays
        public async Task<IActionResult> Index()
        {
            try
            {

                var model = await _context.Mypays.ToListAsync();

                var paytypes = await _context.MypayTypes
                            .Select(p => new SelectListItem
                            {
                                Value = p.Id.ToString(),
                                Text = p.MypayName
                            })
                            .ToListAsync();

                foreach (var p in model)
                {
                    p.MypayName = paytypes
                                    .Where(t => t.Value == p.SelectedMypaytypeId.ToString())
                                    .Select(t => t.Text)
                                    .FirstOrDefault();
                }

                model[0].Items = paytypes;

                return View(model);
            }
            catch (Exception ex)
            {
                int statusCode = 400;
                string? msg = ex.Message;
                return RedirectToAction("Index", "Admin", new { statusCode, msg });
            }
        }

        // GET: Mypays/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var mypay = await _context.Mypays
                .FirstOrDefaultAsync(m => m.Id == id);
            if (mypay == null)
            {
                return NotFound();
            }

            return View(mypay);
        }

        // GET: Mypays/Create
        public async Task<IActionResult> Create()
        {
            string userId = _userManager.GetUserId(User);

            userId ??= "123";

            var paytypes = await _context.MypayTypes
                                    .Select(p => new SelectListItem
                                    {
                                        Value = p.Id.ToString(),
                                        Text = p.MypayName
                                    })
                                    .ToListAsync();
         
            var modelo = new Mypay();
            modelo.Items = paytypes;
            modelo.AspUser = userId;
            
            return View(modelo);
        }

        // POST: Mypays/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,SelectedMypaytypeId,TotalMypay,DateCovered,DateAdded,Note,AspUser,Active")] Mypay mypay)
        {
            if (ModelState.IsValid)
            {
                _context.Add(mypay);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }

            var paytypes = await _context.MypayTypes
                                   .Select(p => new SelectListItem
                                   {
                                       Value = p.Id.ToString(),
                                       Text = p.MypayName
                                   })
                                   .ToListAsync();
            mypay.Items = paytypes;

            return View(mypay);
        }

        // GET: Mypays/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var mypay = await _context.Mypays.FindAsync(id);
            if (mypay == null)
            {
                return NotFound();
            }
            return View(mypay);
        }

        // POST: Mypays/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,SelectedMypaytypeId,TotalMypay,DateCovered,DateAdded,Note,AspUser,Active")] Mypay mypay)
        {
            if (id != mypay.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(mypay);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!MypayExists(mypay.Id))
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
            return View(mypay);
        }

        // GET: Mypays/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var mypay = await _context.Mypays
                .FirstOrDefaultAsync(m => m.Id == id);
            if (mypay == null)
            {
                return NotFound();
            }

            return View(mypay);
        }

        // POST: Mypays/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var mypay = await _context.Mypays.FindAsync(id);
            if (mypay != null)
            {
                _context.Mypays.Remove(mypay);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool MypayExists(int id)
        {
            return _context.Mypays.Any(e => e.Id == id);
        }
    }
}
