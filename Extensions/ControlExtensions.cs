using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace VisualEffects
{
    public static class ControlExtensions
    {
        public static void SelectNextControl( this Control initialControl )
        {
            if( initialControl != null )
            {
                var ctrlSelected = initialControl.SelectNextControl( initialControl, true, true, false, false );
                if( !ctrlSelected )
                    SelectNextControl( initialControl.Parent );
            }
        }

        public static Bitmap GetSnapshot( this Control control )
        {
            if( control.Width <= 0 || control.Height <= 0 )
                return null;

            Bitmap image = new Bitmap( control.Width, control.Height );
            Rectangle targetBounds = new Rectangle( 0, 0, control.Width, control.Height );

            control.DrawToBitmap( image, targetBounds );
            return image;
        }

        public static Bitmap GetFormBorderlessSnapshot( this Form window )
        {
            using( var bmp = new Bitmap( window.Width, window.Height ) )
            {
                window.DrawToBitmap( bmp, new Rectangle( 0, 0, window.Width, window.Height ) );

                Point point = window.PointToScreen( Point.Empty );

                Bitmap target = new Bitmap( window.ClientSize.Width, window.ClientSize.Height );
                using( Graphics g = Graphics.FromImage( target ) )
                {
                    var srcRect = new Rectangle( point.X - window.Location.X,
                        point.Y - window.Location.Y, target.Width, target.Height );

                    g.DrawImage( bmp, 0, 0, srcRect, GraphicsUnit.Pixel );
                }

                return target;
            }
        }
    }
}
