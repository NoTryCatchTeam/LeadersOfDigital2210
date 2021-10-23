﻿using AutoMapper;
using LeadersApi.Models.Responses.Flights;
using LeadersApi.Models.Responses.Hotels;
using LeadersApi.Services.Flights;
using LeadersOfDigital.DataModels.Enums;
using LeadersOfDigital.DataModels.Responses.Flights;
using LeadersOfDigital.DataModels.Responses.Hotels;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Caching.Memory;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;

namespace LeadersApi.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class FlightsController : ControllerBase
    {
        private readonly ILogger<HotelsController> _logger;
        private readonly IMapper _mapper;
        private readonly IFlightsService _flightsService;


        public FlightsController(ILogger<HotelsController> logger, IMapper mapper, IFlightsService flightsService)
        {
            _logger = logger;
            _mapper = mapper;
            _flightsService = flightsService;
        }

        [HttpGet]
        public async Task<IActionResult> GetAsync(Iata origin, Iata destination,DateTime departure, DateTime @return)
        {
            try
            {
                var flights = _mapper.Map<IEnumerable<FlightsResponse>>(await _flightsService.GetFlights(origin, destination, departure, @return));

                if (!flights.Any())
                {
                    return NoContent();
                }

                return Ok(flights);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Ошибка поиска билетов");

                return BadRequest("Ошибка поиска билетов");
            }
        }

    }
}
