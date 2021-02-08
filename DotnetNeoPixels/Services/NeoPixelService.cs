using DotnetNeoPixels.Models;
using DotnetNeoPixels.Services.Interfaces;
using MathNet.Numerics;
using MathNet.Numerics.Random;
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
        
        public void FadePixels(CancellationToken token, Pixels pixels)
        {
            using (var rpi = new WS281x(settings))
            {
                rpi.SetAll(Color.FromArgb(255, pixels.r, pixels.g, pixels.b));
                while (true)
                {
                    for (double x = 0.0; x <= 2.0 * Math.PI; x += (5.0 / 255.0))
                    {
                        if (token.IsCancellationRequested)
                        {
                            this.logger.Information("Cancelling Fade Pattern...");
                            break;
                        }
                        rpi.SetBrightness((int)(255.0 * Math.Abs(Trig.Sin(x))));
                        Thread.Sleep(10);
                    }
                }
            }
        }

        public void LightningPattern(CancellationToken token)
        {
            var random = new Random();
            using (var rpi = new WS281x(settings))
            {
                while (true)
                {
                    if (token.IsCancellationRequested)
                    {
                        this.logger.Information("Cancelling Lightning Pattern...");
                        break;
                    }
                    var blue = random.NextDouble();
                    if (blue < 100)
                    {
                        blue = 100;
                    }
                    rpi.SetBrightness((int)(255 * random.NextDouble()));
                    rpi.SetAll(Color.FromArgb(255, 100, 100, (int)blue));
                    Thread.Sleep((int)(2000 * random.NextDouble()));
                    rpi.SetBrightness(0);
                    Thread.Sleep((int)(5000 * random.NextDouble()));
                }
            }
        }

        public void FlamePattern(CancellationToken token, Pixels pixels)
        {
            var random = new Random();
            using (var rpi = new WS281x(settings))
            {
                int r, g, b;
                if (pixels.r > pixels.g && pixels.r > pixels.b)
                {
                    r = 226; g = 121; b = 35;
                }
                else if (pixels.g > pixels.r && pixels.g > pixels.b)
                {
                    r = 74; g = 150; b = 12;
                }
                else if (pixels.b > pixels.r && pixels.b > pixels.r)
                {
                    r = 158; g = 8; b = 148;
                }
                else
                {
                    r = 226; g = 121; b = 35;
                }
                    while (true)
                {
                    if (token.IsCancellationRequested)
                    {
                        this.logger.Information("Cancelling Lightning Pattern...");
                        break;
                    }
                    for (int i = 0; i < 46; i++)
                    {
                        int flicker = (int)(55 * random.NextDouble());
                        int r1 = r - flicker, g1 = g - flicker, b1 = b - flicker;
                        if (r1 < 0) {
                            r1 = 0;
                        }
                        if (g1 < 0)
                        {
                            g1 = 0;
                        }
                        if (b1 < 0)
                        {
                            b1 = 0;
                        }
                        rpi.SetLed(i, Color.FromArgb(255, r1, g1, b1));
                    }
                    rpi.Render();
                    Thread.Sleep(random.Next(10, 133));
                }
            }
        }

    }
}
