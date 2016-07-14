using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace VisualEffects.Animations.Effects
{
    public class FontSizeEffect : IEffect
    {
        public EffectInteractions Interaction
        {
            get { return EffectInteractions.SIZE; }
        }

        public int GetCurrentValue( System.Windows.Forms.Control control )
        {
            return (int)control.Font.SizeInPoints;
        }

        public void SetValue( Control control, int originalValue, int valueToReach, int newValue )
        {
            control.Font = new System.Drawing.Font( control.Font.FontFamily, newValue );
        }

        public int GetMinimumValue( System.Windows.Forms.Control control )
        {
            return 0;
        }

        public int GetMaximumValue( System.Windows.Forms.Control control )
        {
            return Int32.MaxValue;
        }
    }
}
