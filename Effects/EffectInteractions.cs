using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Drawing;

namespace VisualEffects.Animations.Effects
{
    [Flags]
    public enum EffectInteractions
    {
        X = 1,
        Y = 2,
        WIDTH = 8,
        HEIGHT = 4,
        COLOR = 16,
        TRANSPARENCY = 32,
        LOCATION = X | Y,
        SIZE = WIDTH | HEIGHT,
        BOUNDS = X | Y | WIDTH | HEIGHT
    }
}
