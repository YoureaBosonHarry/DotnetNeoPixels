using DotnetNeoPixels.Services.Interfaces;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Threading.Tasks;
using rpi_ws281x;

namespace DotnetNeoPixels.Services
{
    public class NeoPixelService : INeoPixelService
    {
        private readonly Settings settings = Settings.CreateDefaultSettings(false);
        private readonly Controller controller;
        public NeoPixelService()
        {
            this.controller = settings.AddController(46, Pin.Gpio18, StripType.Unknown, 255, false);
        }
        public void SolidPixels(int r, int g, int b)
        {
            using (var rpi = new WS281x(settings))
            {
                rpi.SetAll(Color.FromArgb(255, r, g, b));
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
    }
}
