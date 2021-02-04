using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DotnetNeoPixels.Services.Interfaces
{
    public interface INeoPixelService
    {
        void SolidPixels(int r, int g, int b);
    }
}
