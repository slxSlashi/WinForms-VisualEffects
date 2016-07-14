using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace VisualEffects.Effects.Bounds
{
    public class FoldEffect : IEffect
    {
        public Animations.Effects.EffectInteractions Interaction
        {
            get { return Animations.Effects.EffectInteractions.BOUNDS; }
        }

        public int GetCurrentValue( System.Windows.Forms.Control control )
        {
            return control.Width * control.Height;
        }

        public void SetValue( System.Windows.Forms.Control control, int originalValue, int valueToReach, int newValue )
        {
            int actualValueChange = Math.Abs( originalValue - valueToReach );
            int currentValue = this.GetCurrentValue( control );

            double absoluteChangePerc = ( (double)( ( originalValue - newValue ) * 100 ) ) / actualValueChange;
            absoluteChangePerc = Math.Abs( absoluteChangePerc );

            if( absoluteChangePerc > 100.0f )
                return;
        }

        public int GetMinimumValue( System.Windows.Forms.Control control )
        {
            if( control.MinimumSize.IsEmpty )
                return 0;

            return control.MinimumSize.Width * control.MinimumSize.Height;
        }

        public int GetMaximumValue( System.Windows.Forms.Control control )
        {
            if( control.MaximumSize.IsEmpty )
                return Int32.MaxValue;

            return control.MaximumSize.Width * control.MaximumSize.Height;
        }
    }
}
