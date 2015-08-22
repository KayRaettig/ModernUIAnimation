using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows;
using System.Windows.Markup;
using System.Windows.Media;
using System.Windows.Media.Animation;

namespace ModernAnimationTest
{
    public class AnimationProducer
    {
        public static Storyboard GetAnimation(AnimationDirection? animationDirection, bool isInAnimation, int animationTimeMS, IEasingFunction easingFunction = null)
        {
            var xAxis = "RenderTransform.(TranslateTransform.X)";
            var yAxis = "RenderTransform.(TranslateTransform.Y)";
            var easing = easingFunction != null ? easingFunction : new QuadraticEase();

            var opac = GetOpac(isInAnimation, animationTimeMS);
            
            var story = new Storyboard();
            (story as IAddChild).AddChild(opac);
            

            DoubleAnimation anim = null;

            if (animationDirection != null)
            {
                if (isInAnimation)
                {
                    anim = new DoubleAnimation(animationDirection == AnimationDirection.Left || animationDirection == AnimationDirection.Top ? -30 : 30, 0, new Duration(new TimeSpan(0, 0, 0, 0, animationTimeMS))) { EasingFunction = easing };
                }
                else
                {
                    anim = new DoubleAnimation(0, animationDirection == AnimationDirection.Left || animationDirection == AnimationDirection.Top ? -30 : 30, new Duration(new TimeSpan(0, 0, 0, 0, animationTimeMS))) { EasingFunction = easing };
                }

                if (anim != null)
                {
                    anim.SetValue(Storyboard.TargetPropertyProperty, new PropertyPath(animationDirection == AnimationDirection.Left || animationDirection == AnimationDirection.Right ? xAxis : yAxis));
                    (story as IAddChild).AddChild(anim);
                }
            }

            return story;
        }

        private static DoubleAnimation GetOpac(bool isInanimation, int animationTime)
        {
            double from = isInanimation ? 0 : 1;
            double to = from == 1 ? 0 : 1;

            var opac = new DoubleAnimation(from, to, new Duration(new TimeSpan(0, 0, 0, 0, animationTime)));
            opac.SetValue(Storyboard.TargetPropertyProperty, new PropertyPath("Opacity"));


            return opac;
        }
    }
}
