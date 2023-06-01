using CarWeb.Data;
using CarWeb.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace CarWeb.Controllers
{
    public class FeedbacksController : Controller
    {
        private readonly CarWebContext _context;

        public FeedbacksController(CarWebContext context)
        {
            _context = context;
        }

        // GET: Feedbacks
        public async Task<IActionResult> Index()
        {
            return _context.Feedback != null ?
                        View(await _context.Feedback.ToListAsync()) :
                        Problem("Entity set 'CarWebContext.Feedback'  is null.");
        }

        // GET: Feedbacks/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.Feedback == null)
            {
                return NotFound();
            }

            var feedback = await _context.Feedback
                .FirstOrDefaultAsync(m => m.FeedbackId == id);
            if (feedback == null)
            {
                return NotFound();
            }

            return View(feedback);
        }

        // GET: Feedbacks/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Feedbacks/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("FeedbackId,CustomerName,CustomerEmail,FeedbackContent")] Feedback feedback)
        {
            if (ModelState.IsValid)
            {
                _context.Add(feedback);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(feedback);
        }

        // GET: Feedbacks/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.Feedback == null)
            {
                return NotFound();
            }

            var feedback = await _context.Feedback.FindAsync(id);
            if (feedback == null)
            {
                return NotFound();
            }
            return View(feedback);
        }

        // POST: Feedbacks/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("FeedbackId,CustomerName,CustomerEmail,FeedbackContent")] Feedback feedback)
        {
            if (id != feedback.FeedbackId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(feedback);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!FeedbackExists(feedback.FeedbackId))
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
            return View(feedback);
        }

        // GET: Feedbacks/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.Feedback == null)
            {
                return NotFound();
            }

            var feedback = await _context.Feedback
                .FirstOrDefaultAsync(m => m.FeedbackId == id);
            if (feedback == null)
            {
                return NotFound();
            }

            return View(feedback);
        }

        // POST: Feedbacks/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.Feedback == null)
            {
                return Problem("Entity set 'CarWebContext.Feedback'  is null.");
            }
            var feedback = await _context.Feedback.FindAsync(id);
            if (feedback != null)
            {
                _context.Feedback.Remove(feedback);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool FeedbackExists(int id)
        {
            return (_context.Feedback?.Any(e => e.FeedbackId == id)).GetValueOrDefault();
        }
    }
}
