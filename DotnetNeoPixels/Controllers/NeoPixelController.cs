using DotnetNeoPixels.Models;
using DotnetNeoPixels.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Serilog;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DotnetNeoPixels.Controllers
{
    [Route("NeoPixels")]
    [ApiController]
    public class NeoPixelController : ControllerBase
    {
        private readonly ILogger logger;
        private readonly INeoPixelService neoPixelService;
        public NeoPixelController(INeoPixelService neoPixelService)
        {
            this.neoPixelService = neoPixelService;
            this.logger = Log.ForContext<NeoPixelController>();
        }

        [HttpPost]
        [Route("TurnOff")]
        public async Task<IActionResult> TurnOffNeoPixels()
        {
            await this.neoPixelService.TurnOff();
            return Ok();
        }

        [HttpPost]
        [Route("SolidPattern")]
        public async Task<IActionResult> SolidPattern([FromBody]Pixels pixels)
        {
            await this.neoPixelService.SolidPixels(pixels);
            return Ok();
        }
    }
}
