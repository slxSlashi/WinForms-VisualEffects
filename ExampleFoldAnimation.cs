using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using VisualEffects.Animations.Effects;
using System.Drawing;
using System.Threading;
using VisualEffects.Animations;
using System.Collections.ObjectModel;
using VisualEffects.Easing;
using VisualEffects.Animators;

namespace VisualEffects
{
    public class ExampleFoldAnimation
    {
        private List<AnimationStatus> _cancellationTokens;

        public Control Control { get; private set; }
        public Size MaxSize { get; set; }
        public Size MinSize { get; set; }
        public int Duration { get; set; }
        public int Delay { get; set; }

        public ExampleFoldAnimation( Control control )
        {
            _cancellationTokens = new List<AnimationStatus>();

            this.Control = control;
            this.MaxSize = control.Size;
            this.MinSize = control.MinimumSize;
            this.Duration = 1000;
            this.Delay = 0;
        }

        public void Show()
        {
            int duration = this.Duration;
            if( _cancellationTokens.Any( animation => !animation.IsCompleted ) )
            {
                var token = _cancellationTokens.First( animation => !animation.IsCompleted );
                duration = (int)( token.ElapsedMilliseconds );
            }

            //cancel hide animation if in progress
            this.Cancel();

            var cT1 = this.Control.Animate( new HorizontalFoldEffect(),
                 EasingFunctions.CircEaseIn, this.MaxSize.Height, duration, this.Delay );

            var cT2 = this.Control.Animate( new VerticalFoldEffect(),
                 EasingFunctions.CircEaseOut, this.MaxSize.Width, duration, this.Delay );

            _cancellationTokens.Add( cT1 );
            _cancellationTokens.Add( cT2 );
        }

        public void Hide()
        {
            int duration = this.Duration;

            if( _cancellationTokens.Any( animation => !animation.IsCompleted ) )
            {
                var token = _cancellationTokens.First( animation => !animation.IsCompleted );
                duration = (int)( token.ElapsedMilliseconds );
            }

            //cancel show animation if in progress
            this.Cancel();

            var cT1 = this.Control.Animate( new HorizontalFoldEffect(),
                EasingFunctions.CircEaseOut, this.MinSize.Height, duration, this.Delay );

            var cT2 = this.Control.Animate( new VerticalFoldEffect(),
                EasingFunctions.CircEaseIn, this.MinSize.Width, duration, this.Delay );

            _cancellationTokens.Add( cT1 );
            _cancellationTokens.Add( cT2 );
        }

        public void Cancel()
        {
            foreach( var token in _cancellationTokens )
                token.CancellationToken.Cancel();

            _cancellationTokens.Clear();
        }
    }
}
