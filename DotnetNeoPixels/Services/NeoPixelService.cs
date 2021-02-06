using DotnetNeoPixels.Models;
using DotnetNeoPixels.Services.Interfaces;
using MathNet.Numerics;
using Serilog;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Threading.Tasks;
using rpi_ws281x;
using System.Threading;

namespace DotnetNeoPixels.Services
{
    public class NeoPixelService : INeoPixelService
    {
        private readonly Settings settings = Settings.CreateDefaultSettings(false);
        private readonly Controller controller;
        private readonly ILogger logger;
        public NeoPixelService()
        {
            this.controller = settings.AddController(46, Pin.Gpio18, StripType.Unknown, 255, false);
            this.logger = Log.ForContext<NeoPixelService>();
        }

        public Task<bool> TurnOff()
        {
            using (var rpi = new WS281x(settings))
            {
                rpi.Reset();
                rpi.Dispose();
                return Task.FromResult<bool>(true);
            }
        }
        public Task<bool> SolidPixels(Pixels pixels)
        {
            using (var rpi = new WS281x(settings))
            {
                rpi.SetAll(Color.FromArgb(255, pixels.r, pixels.g, pixels.b));
                return Task.FromResult<bool>(true);
                //rpi.SetLed(0, Color.Blue);
                //rpi.SetLed(1, Color.Red);
                //rpi.Render();
                //var brightness = rpi.GetBrightness();
                //rpi.SetBrightness(128);
                //var ledCount = rpi.GetLedCount();
                //rpi.SetLedCount(32);
                //rpi.SetAll(Color.Green);
                //rpi.Reset();
                //rpi.Dispose();
            }
        }
        
        public Task<bool> FadePixels(CancellationToken cancellationToken, Pixels pixels)
        {
            using (var rpi = new WS281x(settings))
            {
                while (!cancellationToken.IsCancellationRequested)
                {
                    for (double x = 0.0; x <= 2.0 * Math.PI; x += (1.0/255.0))
                    {
                        rpi.SetAll(Color.FromArgb(255, pixels.r, pixels.g, pixels.b));
                        rpi.SetBrightness((int)(255.0*x));
                        Thread.Sleep(10);
                    }
                }
            }
            return Task.FromResult<bool>(true);
        }

        public Task<bool> LightingPattern(CancellationToken cancellationToken)
        {
            using (var rpi = new WS281x(settings))
            {
                while (!cancellationToken.IsCancellationRequested)
                {
                    for (double x = 0.0; x <= 1.0; x += (1.0 / 255.0))
                    {
                        rpi.SetAll(Color.FromArgb(255, pixels.r, pixels.g, pixels.b));
                        rpi.SetBrightness((int)(255.0 * x));
                        Thread.Sleep(10);
                    }
                }
            }
            return Task.FromResult<bool>(true);
        }

    }
}
