using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace VisualEffects.Animations.Effects
{
    public class XLocationEffect : IEffect
    {
        public int GetCurrentValue( Control control )
        {
            return control.Left;
        }

        public void SetValue( Control control, int originalValue, int valueToReach, int newValue )
        {
            control.Left = newValue;
        }

        public int GetMinimumValue( Control control )
        {
            return Int32.MinValue;
        }

        public int GetMaximumValue( Control control )
        {
            return Int32.MaxValue;
        }

        public EffectInteractions Interaction
        {
            get { return EffectInteractions.X; }
        }
    }
}
