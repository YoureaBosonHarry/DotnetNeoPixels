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
        private static CancellationTokenSource cancellationTokenSource = new CancellationTokenSource();
        public NeoPixelController(INeoPixelService neoPixelService)
        {
            this.neoPixelService = neoPixelService;
            this.logger = Log.ForContext<NeoPixelController>();
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
        public IActionResult FadePattern([FromBody] Pixels pixels)
        {
            ResetToken();
            Task.Run(() =>
            {   
                this.neoPixelService.FadePixels(cancellationTokenSource.Token, pixels);
            });
            return Ok();
        }

        [HttpPost]
        [Route("LightningPattern")]
        public IActionResult LightningPattern()
        {
            ResetToken();
            Task.Run(() =>
            {
                this.neoPixelService.LightningPattern(cancellationTokenSource.Token);
            });
            return Ok();
        }

        [HttpPost]
        [Route("FlamePattern")]
        public IActionResult FlamePattern([FromBody] Pixels pixels)
        {
            ResetToken();
            Task.Run(() =>
            {
                this.neoPixelService.FlamePattern(cancellationTokenSource.Token, pixels);
            });
            return Ok();
        }

        private void ResetToken()
        {
            cancellationTokenSource.Cancel();
            cancellationTokenSource.Dispose();
            Thread.Sleep(500);
            cancellationTokenSource = new CancellationTokenSource();
        }
    }
}
