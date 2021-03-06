﻿using net_core_backend.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using net_core_backend.Services;

namespace net_core_backend.Controllers
{
    /// <summary>
    /// Example controller
    /// </summary>
    /// 
    [ApiController]
    [Route("api/[controller]")]
    public class ExampleController : ControllerBase
    {
        private readonly ILogger<ExampleController> _logger;
        private readonly IndividualProjectContext _context;


        public ExampleController(ILogger<ExampleController> logger, IndividualProjectContext context)
        {
            _logger = logger;
            _context = context;
        }


        [HttpGet]
        [Authorize]
        public async Task<ActionResult<SupportTicket>> GetTickets()
        {
            string userId = User.FindFirst(ClaimTypes.NameIdentifier).Value;

            try
            {
                var items = await _context.SupportTicket.Include(x => x.TicketChat).ToListAsync();
                return Ok();
            }
            catch (Exception ex)
            {
                return BadRequest(ex);
            }
        }
    }
}
