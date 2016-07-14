using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Drawing;

namespace VisualEffects.Animations.Effects
{
    public class BottomAnchoredHeightEffect : IEffect
    {
        public int GetCurrentValue( Control control )
        {
            return control.Height;
        }

        public void SetValue( Control control, int originalValue, int valueToReach, int newValue )
        {
            //changing location and size independently can cause flickering:
            //change bounds property instead.

            var size = new Size( control.Width, newValue );
            var location = new Point( control.Left, control.Top +
                ( control.Height - newValue ) );

            control.Bounds = new Rectangle( location, size );
        }

        public int GetMinimumValue( Control control )
        {
            if( control.MinimumSize.IsEmpty )
                return Int32.MinValue;

            return control.MinimumSize.Height;
        }

        public int GetMaximumValue( Control control )
        {
            if( control.MaximumSize.IsEmpty )
                return Int32.MaxValue;

            return control.MaximumSize.Height;
        }

        public EffectInteractions Interaction
        {
            get { return EffectInteractions.BOUNDS; }
        }
    }
}
