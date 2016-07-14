using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Diagnostics;
using System.Threading;

namespace VisualEffects.Animators
{
    public class AnimationStatus : EventArgs
    {
        private Stopwatch _stopwatch;

        public long ElapsedMilliseconds
        {
            get { return _stopwatch.ElapsedMilliseconds; }
        }
        public CancellationTokenSource CancellationToken { get; private set; }
        public bool IsCompleted { get; set; }

        public AnimationStatus( CancellationTokenSource token, Stopwatch stopwatch )
        {
            this.CancellationToken = token;
            _stopwatch = stopwatch;
        }
        public IEffect Effect { get; set; }
    }
}
