using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Drawing;
using System.Windows.Forms;

namespace VisualEffects.Animations.Effects
{
    public class ColorChannelShiftEffect : IEffect
    {
        public enum ColorChannels { A, R, G, B };
        public ColorChannels ColorChannel;

        public ColorChannelShiftEffect( ColorChannels channel )
        {
            this.ColorChannel = channel;
        }

        public EffectInteractions Interaction
        {
            get { return EffectInteractions.COLOR; }
        }

        public int GetCurrentValue( System.Windows.Forms.Control control )
        {
            if( ColorChannel == ColorChannels.A )
                return control.BackColor.A;

            if( ColorChannel == ColorChannels.R )
                return control.BackColor.R;

            if( ColorChannel == ColorChannels.G )
                return control.BackColor.G;

            return control.BackColor.B;
        }

        public void SetValue( Control control, int originalValue, int valueToReach, int newValue )
        {
            if( newValue >= 0 && newValue <= 255 )
            {
                int a = control.BackColor.A;
                int r = control.BackColor.R;
                int g = control.BackColor.G;
                int b = control.BackColor.B;

                switch( ColorChannel )
                {
                    case ColorChannels.A: a = newValue; break;
                    case ColorChannels.R: r = newValue; break;
                    case ColorChannels.G: g = newValue; break;
                    case ColorChannels.B: b = newValue; break;
                }

                control.BackColor = Color.FromArgb( a, r, g, b );
            }
        }

        public int GetMinimumValue( System.Windows.Forms.Control control )
        {
            return 0;
        }

        public int GetMaximumValue( System.Windows.Forms.Control control )
        {
            return 255;
        }
    }
}
