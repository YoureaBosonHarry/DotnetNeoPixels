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
        [Route("SolidPattern")]
        public void ChangePatternAsync([FromBody]Pixels pixels)
        {
            this.neoPixelService.SolidPixels(pixels.r, pixels.g, pixels.b);
        }
    }
}
