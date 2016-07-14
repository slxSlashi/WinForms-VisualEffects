using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading;
using System.Windows.Forms;
using VisualEffects.Animations.Effects;
using VisualEffects.Easing;
using VisualEffects.Animators;

namespace VisualEffects
{
    public static class Animator
    {
        /*I decided to add no abstraction over animators and provide bare functionality.
         *I think nothing is better than a static method here.*/

        public static event EventHandler<AnimationStatus> Animated;

        /// <summary>
        /// Animate a control property from its present value to a target one
        /// </summary>
        /// <param name="control">Target control</param>
        /// <param name="iEffect">Effect to apply</param>
        /// <param name="easing">Easing function to apply</param>
        /// <param name="valueToReach">Target value reached when animation completes</param>
        /// <param name="duration">Amount of time taken to reach the target value</param>
        /// <param name="delay">Amount of delay to apply before animation starts</param>
        /// <param name="reverse">If set to true, animation reaches target value and animates back to initial value. It takes 2*<paramref name="duration"/></param>
        /// <param name="loops">If reverse is set to true, indicates how many loops to perform. Negatives or zero mean infinite loop</param>
        /// <returns></returns>
        public static AnimationStatus Animate(Control control, IEffect iEffect, EasingDelegate easing, int valueToReach, int duration, int delay, bool reverse = false, int loops = 1)
        {
            
            try
            {

            
                //used to calculate animation frame based on how much time has effectively passed
                var stopwatch = new Stopwatch();

                //used to cancel animation
                var cancelTokenSource = new CancellationTokenSource();

                //used to access animation progress
                var animationStatus = new AnimationStatus(cancelTokenSource, stopwatch);
                animationStatus.Effect = iEffect;

                //This timer allows delayed start. Control's state checks and evaluations are delayed too.
                new System.Threading.Timer((state) =>
               {
               //is there anything to do here?
               int originalValue = iEffect.GetCurrentValue(control);
                   if (originalValue == valueToReach)
                   {
                       animationStatus.IsCompleted = true;
                       return;
                   }

               //upper bound check
               int maxVal = iEffect.GetMaximumValue(control);
                   if (valueToReach > maxVal)
                   {
                       string msg = String.Format("Value must be lesser than the maximum allowed. " +
                           "Max: {0}, provided value: {1}", maxVal, valueToReach);

                       throw new ArgumentException(msg, "valueToReach");
                   }

               //lower bound check
               int minVal = iEffect.GetMinimumValue(control);
                   if (valueToReach < iEffect.GetMinimumValue(control))
                   {
                       string msg = String.Format("Value must be greater than the minimum allowed. " +
                           "Min: {0}, provided value: {1}", minVal, valueToReach);

                       throw new ArgumentException(msg, "valueToReach");
                   }

                   bool reversed = false;
                   int performedLoops = 0;

                   int actualValueChange = Math.Abs(originalValue - valueToReach);

                   System.Timers.Timer animationTimer = new System.Timers.Timer();
               //adjust interval (naive, edge cases can mess up)
               animationTimer.Interval = (duration > actualValueChange) ?
                      (duration / actualValueChange) : actualValueChange;

               //because of naive interval calculation this is required
               if (iEffect.Interaction == EffectInteractions.COLOR)
                       animationTimer.Interval = 10;

                   if (!control.IsDisposed)
                   {


                   //main animation timer tick
                   animationTimer.Elapsed += (o, e2) =>
                      {
                          if (!control.IsDisposed)
                          {
                          //cancellation support
                          if (cancelTokenSource.Token.IsCancellationRequested)
                              {
                                  animationStatus.IsCompleted = true;
                                  animationTimer.Stop();
                                  stopwatch.Stop();

                                  return;
                              }

                          //main logic
                          bool increasing = originalValue < valueToReach;

                              int minValue = Math.Min(originalValue, valueToReach);
                              int maxValue = Math.Abs(valueToReach - originalValue);
                              int newValue = (int)easing(stopwatch.ElapsedMilliseconds, minValue, maxValue, duration);

                              if (!increasing)
                                  newValue = (originalValue + valueToReach) - newValue - 1;




                              control.BeginInvoke(new MethodInvoker(() =>
                              {
                                  if (!control.IsDisposed && control.IsHandleCreated)
                                  {
                                      iEffect.SetValue(control, originalValue, valueToReach, newValue);

                                      bool timeout = stopwatch.ElapsedMilliseconds >= duration;
                                      if (timeout)
                                      {
                                          if (reverse && (!reversed || loops <= 0 || performedLoops < loops))
                                          {
                                              reversed = !reversed;
                                              if (reversed)
                                                  performedLoops++;

                                              int initialValue = originalValue;
                                              int finalValue = valueToReach;

                                              valueToReach = valueToReach == finalValue ? initialValue : finalValue;
                                              originalValue = valueToReach == finalValue ? initialValue : finalValue;

                                              stopwatch.Restart();
                                              animationTimer.Start();
                                          }
                                          else
                                          {
                                              animationStatus.IsCompleted = true;
                                              animationTimer.Stop();
                                              stopwatch.Stop();

                                              if (Animated != null)
                                                  Animated(control, animationStatus);
                                          }
                                      }
                                  }
                                  else
                                  {
                                      if (Animated != null)
                                          Animated(control, animationStatus);
                                  }
                              }));
                          }
                      };
                   }

               //start
               stopwatch.Start();
                   animationTimer.Start();

               }, null, delay, System.Threading.Timeout.Infinite);
            

            return animationStatus;
            }
            catch (Exception e)
            {
                if( Debugger.IsAttached)
                {
                    Debug.Print(String.Format("Exception @ VisualEffects.Animator.Animate(). Message: {0}", e.Message));
                    Debug.Print(String.Format("Stack:\n{0}", e.Message));
                }
            }
            return null;
        }
    }
}
