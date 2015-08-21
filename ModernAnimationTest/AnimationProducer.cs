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
        public static Storyboard GetAnimation(AnimationType animationType, AnimationDirection animationDirection, bool? isInAnimation, int animationTimeMS = 500)
        {
            var xAxis = "RenderTransform.(TranslateTransform.X)";
            var yAxis = "RenderTransform.(TranslateTransform.Y)";

            var opac = GetOpac(animationType, animationTimeMS);
            var story = new Storyboard();
            (story as IAddChild).AddChild(opac);
            

            DoubleAnimation anim = null;

            if (isInAnimation == true)
            {
                anim = new DoubleAnimation(animationDirection == AnimationDirection.Left || animationDirection == AnimationDirection.Top ? -30 : 30, 0, new Duration(new TimeSpan(0, 0, 0, 0, animationTimeMS))) { EasingFunction = new ExponentialEase() };
            }
            else if(isInAnimation == false)
            {
                anim = new DoubleAnimation(0, animationDirection == AnimationDirection.Left || animationDirection == AnimationDirection.Top ? -30 : 30, new Duration(new TimeSpan(0, 0, 0, 0, animationTimeMS))) { EasingFunction = new ExponentialEase() };
            }

            if (anim != null)
            {
                anim.SetValue(Storyboard.TargetPropertyProperty, new PropertyPath(animationDirection == AnimationDirection.Left || animationDirection == AnimationDirection.Right ? xAxis : yAxis));
                (story as IAddChild).AddChild(anim);
            }

            return story;
        }

        private static DoubleAnimation GetOpac(AnimationType animationType, int animationTime)
        {
            double from = animationType == AnimationType.In ? 0 : 1;
            double to = from == 1 ? 0 : 1;

            var opac = new DoubleAnimation(from, to, new Duration(new TimeSpan(0, 0, 0, 0, animationTime)));
            opac.SetValue(Storyboard.TargetPropertyProperty, new PropertyPath("Opacity"));


            return opac;
        }
    }
}
