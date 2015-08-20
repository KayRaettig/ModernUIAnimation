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

        static string opacityString = "Opacity";
        static string xAxis = "RenderTransform.(TranslateTransform.X)";
        static string yAxis = "RenderTransform.(TranslateTransform.Y)";

        static int animationTime = 500;

        public static bool isInitialized = false;

        private static DoubleAnimation OpacIn = new DoubleAnimation(0, 1, new Duration(new TimeSpan(0, 0, 0, 0, animationTime)));
        private static DoubleAnimation OpacOut = new DoubleAnimation(1, 0, new Duration(new TimeSpan(0, 0, 0, 0, animationTime)));


        private static DoubleAnimation InwardsLeft = new DoubleAnimation(-30, 0, new Duration(new TimeSpan(0, 0, 0, 0, animationTime))) { EasingFunction = new ExponentialEase() };
        private static DoubleAnimation InwardsRight = new DoubleAnimation(30, 0, new Duration(new TimeSpan(0, 0, 0, 0, animationTime)));
        private static DoubleAnimation InwardsTop = new DoubleAnimation(-30, 0, new Duration(new TimeSpan(0, 0, 0, 0, animationTime)));
        private static DoubleAnimation InwardsBottom = new DoubleAnimation(30, 0, new Duration(new TimeSpan(0, 0, 0, 0, animationTime)));


        private static DoubleAnimation OutwardsLeft = new DoubleAnimation(0, -30, new Duration(new TimeSpan(0, 0, 0, 0, animationTime)));
        private static DoubleAnimation OutwardsRight = new DoubleAnimation(0, 30, new Duration(new TimeSpan(0, 0, 0, 0, animationTime)));
        private static DoubleAnimation OutwardsTop = new DoubleAnimation(0, -30, new Duration(new TimeSpan(0, 0, 0, 0, animationTime)));
        private static DoubleAnimation OutwardBottom = new DoubleAnimation(0, 30, new Duration(new TimeSpan(0, 0, 0, 0, animationTime)));


        public static void Initialize()
        {
            OpacIn.SetValue(Storyboard.TargetPropertyProperty, new PropertyPath(opacityString));
            OpacOut.SetValue(Storyboard.TargetPropertyProperty, new PropertyPath(opacityString));


            isInitialized = true;
        }

        public static Storyboard GetAnimation(AnimationType animationType, AnimationDirection animationDirection, bool? isInAnimation)
        {
            if (!isInitialized)
                Initialize();

            var story = new Storyboard();


            if (animationType == AnimationType.In)
            {
                (story as IAddChild).AddChild(OpacIn);
            }
            else if (animationType == AnimationType.Out)
            {
                (story as IAddChild).AddChild(OpacOut);
            }
            

            DoubleAnimation anim = null;

            if (isInAnimation == true)
            {
                anim = new DoubleAnimation(animationDirection == AnimationDirection.Left || animationDirection == AnimationDirection.Top ? -30 : 30, 0, new Duration(new TimeSpan(0, 0, 0, 0, animationTime))) { EasingFunction = new ExponentialEase() };
            }
            else if(isInAnimation == false)
            {
                anim = new DoubleAnimation(0, animationDirection == AnimationDirection.Left || animationDirection == AnimationDirection.Top ? -30 : 30, new Duration(new TimeSpan(0, 0, 0, 0, animationTime))) { EasingFunction = new ExponentialEase() };
            }

            if (anim != null)
            {
                anim.SetValue(Storyboard.TargetPropertyProperty, new PropertyPath(animationDirection == AnimationDirection.Left || animationDirection == AnimationDirection.Right ? xAxis : yAxis));
                (story as IAddChild).AddChild(anim);
            }


            return story;
        }



        public static Storyboard GetInAnimation(AnimationType inAnimationType, AnimationDirection inAnimation)
        {
            if (!isInitialized)
                Initialize();

            var story = new Storyboard();

            (story as IAddChild).AddChild(inAnimationType == AnimationType.In ? OpacIn : OpacOut);


            switch (inAnimation)
            {
                case AnimationDirection.Bottom:
                    InwardsBottom.SetValue(Storyboard.TargetPropertyProperty, new PropertyPath(yAxis));
                    (story as IAddChild).AddChild(InwardsBottom);
                    break;
                case AnimationDirection.Left:
                    InwardsLeft.SetValue(Storyboard.TargetPropertyProperty, new PropertyPath(xAxis));
                    (story as IAddChild).AddChild(InwardsLeft);
                    break;
                case AnimationDirection.Right:
                    InwardsRight.SetValue(Storyboard.TargetPropertyProperty, new PropertyPath(xAxis));
                    (story as IAddChild).AddChild(InwardsRight);
                    break;
                case AnimationDirection.Top:
                    InwardsTop.SetValue(Storyboard.TargetPropertyProperty, new PropertyPath(yAxis));
                    (story as IAddChild).AddChild(InwardsTop);
                    break;
                case AnimationDirection.Unspecified:
                default:
                    break;
            }

            return story;
        }

        public static Storyboard GetOutAnimation(AnimationType outAnimationType, AnimationDirection outAnimation)
        {
            if (!isInitialized)
                Initialize();

            var story = new Storyboard();

            (story as IAddChild).AddChild(outAnimationType == AnimationType.In ? OpacIn : OpacOut);

            switch (outAnimation)
            {
                case AnimationDirection.Right:
                    OutwardsRight.SetValue(Storyboard.TargetPropertyProperty, new PropertyPath(xAxis));
                    (story as IAddChild).AddChild(OutwardsRight);
                    break;
                case AnimationDirection.Bottom:
                    OutwardBottom.SetValue(Storyboard.TargetPropertyProperty, new PropertyPath(yAxis));
                    (story as IAddChild).AddChild(OutwardBottom);
                    break;
                case AnimationDirection.Top:
                    OutwardsTop.SetValue(Storyboard.TargetPropertyProperty, new PropertyPath(yAxis));
                    (story as IAddChild).AddChild(OutwardsTop);
                    break;
                case AnimationDirection.Left:
                    OutwardsLeft.SetValue(Storyboard.TargetPropertyProperty, new PropertyPath(xAxis));
                    (story as IAddChild).AddChild(OutwardsLeft);
                    break;
            }

            return story;
        }

    }
}
