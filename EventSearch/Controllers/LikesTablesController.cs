﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using EventSearch.Data;
using EventSearch.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;

namespace EventSearch.Controllers
{
    public class LikesTablesController : Controller
    {
        private readonly ApplicationDbContext _context;

        public LikesTablesController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: LikesTables
        public IActionResult Index(string userid, int id)
        {
            var applicationDbContext = _context.Likes.Include(l => l.AdventurePost).Include(l => l.ApplicationUser);
            ViewModel like = new ViewModel();
            like.Likes = _context.Likes.Where(l => l.AdventurePostId == id).ToList();
            like.Users = _context.User.Where(u => like.Likes.Any(l => l.UserId == u.Id)).ToList();
            return View(like);
        }

        // GET: LikesTables/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var likesTable = await _context.Likes
                .Include(l => l.AdventurePost)
                .Include(l => l.ApplicationUser)
                .FirstOrDefaultAsync(m => m.LikeId == id);
            if (likesTable == null)
            {
                return NotFound();
            }

            return View(likesTable);
        }

        // GET: LikesTables/Create
        public async Task<IActionResult> Create(string userid, int id)
        {
            //string user = this.User.FindFirstValue(ClaimTypes.NameIdentifier);
            //ViewModel check = new ViewModel();
            //check.Likes = _context.Likes.Where(l => l.UserId == user).ToList();
            //check.Users = _context.Likes.Where(l => check.Likes.Any(u => u.AdventurePostId == id)).ToList();


            LikesTable like = new LikesTable();
            like.UserId = userid;
            like.AdventurePostId = id;
            _context.Add(like);
            await _context.SaveChangesAsync();
            return RedirectToAction("Index", "AdventurePosts");
        }

        // POST: LikesTables/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        //[HttpPost]
        //[ValidateAntiForgeryToken]
        //public async Task<IActionResult> Create([Bind("LikeId,AdventurePostId,UserId")] LikesTable likesTable)
        //{
        //    if (ModelState.IsValid)
        //    {
        //        _context.Add(likesTable);
        //        await _context.SaveChangesAsync();
        //        return RedirectToAction(nameof(Index));
        //    }
        //    ViewData["AdventurePostId"] = new SelectList(_context.AdventuresPost, "PostId", "PostId", likesTable.AdventurePostId);
        //    ViewData["UserId"] = new SelectList(_context.User, "Id", "Id", likesTable.UserId);
        //    return View(likesTable);
        //}

        // GET: LikesTables/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var likesTable = await _context.Likes.FindAsync(id);
            if (likesTable == null)
            {
                return NotFound();
            }
            ViewData["AdventurePostId"] = new SelectList(_context.AdventuresPost, "PostId", "PostId", likesTable.AdventurePostId);
            ViewData["UserId"] = new SelectList(_context.User, "Id", "Id", likesTable.UserId);
            return View(likesTable);
        }

        // POST: LikesTables/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("LikeId,AdventurePostId,UserId")] LikesTable likesTable)
        {
            if (id != likesTable.LikeId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(likesTable);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!LikesTableExists(likesTable.LikeId))
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
            ViewData["AdventurePostId"] = new SelectList(_context.AdventuresPost, "PostId", "PostId", likesTable.AdventurePostId);
            ViewData["UserId"] = new SelectList(_context.User, "Id", "Id", likesTable.UserId);
            return View(likesTable);
        }

        // GET: LikesTables/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var likesTable = await _context.Likes
                .Include(l => l.AdventurePost)
                .Include(l => l.ApplicationUser)
                .FirstOrDefaultAsync(m => m.LikeId == id);
            if (likesTable == null)
            {
                return NotFound();
            }

            return View(likesTable);
        }

        // POST: LikesTables/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var likesTable = await _context.Likes.FindAsync(id);
            _context.Likes.Remove(likesTable);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool LikesTableExists(int id)
        {
            return _context.Likes.Any(e => e.LikeId == id);
        }
    }
}