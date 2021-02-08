using DotnetNeoPixels.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace DotnetNeoPixels.Services.Interfaces
{
    public interface INeoPixelService
    {
        Task<bool> TurnOff();
        Task<bool> SolidPixels(Pixels pixels);
        void FadePixels(CancellationToken token, Pixels pixels);
        void LightningPattern(CancellationToken token);
        void FlamePattern(CancellationToken token, Pixels pixels);
    }
}
