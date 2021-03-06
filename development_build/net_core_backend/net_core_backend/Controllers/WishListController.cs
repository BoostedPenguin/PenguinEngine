﻿using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using net_core_backend.Models;
using net_core_backend.Services.Interfaces;
using net_core_backend.ViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace net_core_backend.Controllers
{

    [ApiController]
    [Route("api/[controller]")]
    public class WishListController : ControllerBase
    {
        private readonly ILogger<TicketController> _logger;
        private readonly IMapper mapper;
        private readonly IWishListService _context;

        public WishListController(IWishListService context, ILogger<TicketController> logger, IMapper mapper)
        {
            _context = context;
            _logger = logger;
            this.mapper = mapper;
        }


        [HttpGet]
        [Authorize]
        public async Task<IActionResult> GetWishlist()
        {
            try
            {
                var result = await _context.GetWishlist();

                var dto = mapper.Map<WishListViewModel>(result);

                return Ok(dto);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpGet("locations")]
        [Authorize]
        public async Task<IActionResult> GetSimpleWishlistLocations()
        {
            try
            {
                var result = await _context.GetSimpleWishlistLocations();

                return Ok(result);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPatch("locations/status")]
        [Authorize]
        public async Task<IActionResult> SetOriginDestination([FromBody]dynamic body)
        {
            int locationId = body.locationId;
            string od = body.od;

            try
            {
                await _context.SetOriginDestination(locationId, od);

                return Ok();
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpGet("locations/status")]
        [Authorize]
        public async Task<IActionResult> CheckOriginDestination()
        {
            try
            {
                var result = await _context.CheckOriginDestination();

                return Ok(result);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpDelete]
        [Authorize]
        public async Task<IActionResult> ClearWishlist()
        {
            try
            {
                var result = await _context.ClearWishlist();

                var dto = mapper.Map<WishListViewModel>(result);

                return Ok(dto);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPatch("add")]
        [Authorize]
        public async Task<IActionResult> AddLocation([FromBody] Locations location)
        {
            try
            {
                var result = await _context.AddLocation(location);

                var dto = mapper.Map<WishListViewModel>(result);

                return Ok(dto);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPatch("remove/{location_id}")]
        [Authorize]
        public async Task<IActionResult> RemoveLocation([FromRoute] int location_id)
        {
            try
            {
                var result = await _context.RemoveLocation(location_id);

                var dto = mapper.Map<WishListViewModel>(result);

                return Ok(dto);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPost("create")]
        [Authorize]
        public async Task<IActionResult> CreateTrip([FromBody]dynamic body)
        {
            string name = body.name;
            string transportation = body.transportation;

            try
            {
                var result = await _context.CreateTrip(name, transportation);

                var dto = mapper.Map<UserTripsViewModel>(result);

                return Ok(dto);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpGet("waypoints")]
        [Authorize]
        public async Task<IActionResult> GetWaypointsFromWishlist()
        {
            try
            {
                var result = await _context.GetWaypointsFromWishlist();

                List<fuckoff> ea = new List<fuckoff>();
                foreach(var a in result)
                {
                    ea.Add(new fuckoff() { Location = a.End_Address });
                }
                return Ok(ea);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        public class fuckoff
        {
            public string Location { get; set; }
        }
    }
}
