﻿using DataAccessLayer.Helpers;
using Domain.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace DataAccessLayer.Controllers;

[ApiController]
    [Route("api/Mentor")]
    public class MentorsController : ControllerBase
    {
        ApplicationContext _context;
        public MentorsController(ApplicationContext context)
        {
            _context = context;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<Mentor>>> GetMentors()
        {
            return await _context.Mentor.ToListAsync();
        }

        [HttpGet("GetMentor/{id}")]
        public async Task<ActionResult<Mentor>> GetMentor(int id)
        {
            var mentor = await _context.Mentor.FindAsync(id);
            if (mentor == null) return NotFound();
            return await mentor.WithClients(_context);
        }
        
        [HttpPost("PostMentor")] //api/client/updateMentor
        public async Task<ActionResult<Mentor>> PostMentor(Mentor mentor)
        {
            if (mentor == null) return BadRequest();
            _context.Mentor.Add(mentor);
            await _context.SaveChangesAsync();
            return Ok(mentor);
        }
        
        [HttpPut("PutMentor")]
        public async Task<ActionResult<Mentor>> Put(Mentor mentor)
        {
            if (mentor == null) return BadRequest();

            _context.Update(mentor);
            await _context.SaveChangesAsync();
            return Ok(mentor);
        }

        [HttpDelete("DeleteMentor/{Id}")]
        public async Task<ActionResult<Mentor>> Delete(int id)
        {
            var mentor = _context.Mentor.FirstOrDefaultAsync(p => p.MentorId == id);
            if (mentor == null) return NotFound();
            _context.Mentor.Remove(await mentor);
            await _context.SaveChangesAsync();
            return Ok(mentor);
        } 

    }

