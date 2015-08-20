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

        public static bool isInitialized = false;

        private static DoubleAnimation OpacIn = new DoubleAnimation(0, 1, new Duration(new TimeSpan(0, 0, 0, 0, 500)));
        private static DoubleAnimation OpacOut = new DoubleAnimation(1, 0, new Duration(new TimeSpan(0, 0, 0, 0, 500)));


        private static DoubleAnimation InwardsLeft = new DoubleAnimation(-30, 0, new Duration(new TimeSpan(0, 0, 0, 0, 500))) {EasingFunction = new ExponentialEase()};
        // Rather experimental
        private static DoubleAnimation InwardsRight = new DoubleAnimation(60, 0, new Duration(new TimeSpan(0, 0, 0, 0, 500)));
        private static DoubleAnimation InwardsTop = new DoubleAnimation(60, 0, new Duration(new TimeSpan(0, 0, 0, 0, 500)));
        private static DoubleAnimation InwardsBottom = new DoubleAnimation(-30, 0, new Duration(new TimeSpan(0, 0, 0, 0, 500)));


        private static DoubleAnimation OutwardsRight = new DoubleAnimation(0, 60, new Duration(new TimeSpan(0, 0, 0, 0, 500)));

private static DoubleAnimation OutwardsLeft = new DoubleAnimation (0, -60, new Duration (new TimeSpan (0, 0, 0, 0, 500)));
        public static void Initialize()
        {
            OpacIn.SetValue(Storyboard.TargetPropertyProperty, new PropertyPath(opacityString));
            OpacOut.SetValue(Storyboard.TargetPropertyProperty, new PropertyPath(opacityString));


            isInitialized = true;
        }


        public static DoubleAnimation GetDoubleAnimation(AnimationType animationType)
        {
            if(animationType == AnimationType.In)
            return OpacIn;

            if (animationType == AnimationType.Out)
                return OpacOut;

            return null;

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
                    break;
                case AnimationDirection.Top:
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
            }

            return story;
        }

    }
}
