using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using VisualEffects.Animations.Effects;

namespace VisualEffects
{
    /// <summary>
    /// By implementing this interface you define what property of your control
    /// is manipulated and the way you manipulate it.
    /// </summary>
    public interface IEffect
    {
        EffectInteractions Interaction { get; }

        int GetCurrentValue( Control control );
        void SetValue( Control control, int originalValue, int valueToReach, int newValue );

        int GetMinimumValue( Control control );
        int GetMaximumValue( Control control );
    }
}
