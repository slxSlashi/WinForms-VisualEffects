using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Drawing;
using System.Windows.Forms;

namespace VisualEffects.Animations.Effects
{
    public class ColorShiftEffect : IEffect
    {
        public EffectInteractions Interaction
        {
            get { return EffectInteractions.COLOR; }
        }

        public int GetCurrentValue( Control control )
        {
            return control.BackColor.ToArgb();
        }

        public void SetValue( Control control, int originalValue, int valueToReach, int newValue )
        {
            int actualValueChange = Math.Abs( originalValue - valueToReach );
            int currentValue = this.GetCurrentValue( control );

            double absoluteChangePerc = ( (double)( ( originalValue - newValue ) * 100 ) ) / actualValueChange;
            absoluteChangePerc = Math.Abs( absoluteChangePerc );

            if( absoluteChangePerc > 100.0f )
                return;

            Color originalColor = Color.FromArgb( originalValue );
            Color newColor = Color.FromArgb( valueToReach );

            int newA = (int)Interpolate( originalColor.A, newColor.A, absoluteChangePerc );
            int newR = (int)Interpolate( originalColor.R, newColor.R, absoluteChangePerc );
            int newG = (int)Interpolate( originalColor.G, newColor.G, absoluteChangePerc );
            int newB = (int)Interpolate( originalColor.B, newColor.B, absoluteChangePerc );

            control.BackColor = Color.FromArgb( newA, newR, newG, newB );
            Console.WriteLine( control.BackColor + " " + newColor );
        }

        public int GetMinimumValue( Control control )
        {
            return Color.Black.ToArgb();
        }

        public int GetMaximumValue( Control control )
        {
            return Color.White.ToArgb();
        }

        private int Interpolate( int val1, int val2, double changePerc )
        {
            int difference = val2 - val1;
            int distance = (int)( difference * ( changePerc / 100 ) );
            return (int)( val1 + distance );
        }
    }
}
