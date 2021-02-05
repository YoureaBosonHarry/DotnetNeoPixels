using DotnetNeoPixels.Models;
using DotnetNeoPixels.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Serilog;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace DotnetNeoPixels.Controllers
{
    [Route("NeoPixels")]
    [ApiController]
    public class NeoPixelController : ControllerBase
    {
        private readonly ILogger logger;
        private readonly INeoPixelService neoPixelService;
        private readonly CancellationTokenSource cancellationTokenSource;
        public NeoPixelController(INeoPixelService neoPixelService)
        {
            this.neoPixelService = neoPixelService;
            this.logger = Log.ForContext<NeoPixelController>();
            this.cancellationTokenSource = new CancellationTokenSource();
        }

        [HttpPost]
        [Route("TurnOff")]
        public async Task<IActionResult> TurnOffNeoPixels()
        {
            cancellationTokenSource.Cancel();
            await this.neoPixelService.TurnOff();
            return Ok();
        }

        [HttpPost]
        [Route("SolidPattern")]
        public async Task<IActionResult> SolidPattern([FromBody]Pixels pixels)
        {
            cancellationTokenSource.Cancel();
            await this.neoPixelService.SolidPixels(pixels);
            return Ok();
        }

        [HttpPost]
        [Route("FadePattern")]
        public async Task<IActionResult> FadePattern([FromBody] Pixels pixels)
        {
            Task.Run(async() => await this.neoPixelService.FadePixels(this.cancellationTokenSource.Token, pixels));
            return Ok();
        }
    }
}
