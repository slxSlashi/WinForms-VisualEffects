using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace VisualEffects.Animations.Effects
{
    public class TopAnchoredHeightEffect : IEffect
    {
        public int GetCurrentValue( Control control )
        {
            return control.Height;
        }

        public void SetValue( Control control, int originalValue, int valueToReach, int newValue )
        {
            control.Height = newValue;
        }

        public int GetMinimumValue( Control control )
        {
            return control.MinimumSize.IsEmpty ? Int32.MinValue
                : control.MinimumSize.Height;
        }

        public int GetMaximumValue( Control control )
        {
            return control.MaximumSize.IsEmpty ? Int32.MaxValue
                : control.MaximumSize.Height;
        }

        public EffectInteractions Interaction
        {
            get { return EffectInteractions.HEIGHT; }
        }
    }
}
