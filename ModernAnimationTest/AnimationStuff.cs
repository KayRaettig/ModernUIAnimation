using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Markup;
using System.Windows.Media;
using System.Windows.Media.Animation;

namespace ModernAnimationTest
{
    public class AnimationStuff
    {

        #region Attached Properties

        private static AnimationType inAnimationType = AnimationType.Unspecified;
        private static AnimationType outAnimationType = AnimationType.Unspecified;
        private static AnimationDirection inAnim = AnimationDirection.Unspecified;
        private static AnimationDirection outAnim = AnimationDirection.Unspecified;
        private static FrameworkElement target = null;
        private static Storyboard outAnimation = null;

        private static DependencyObject parent = null;



        public static AnimationType GetInAnimationType(DependencyObject obj)
        {
            return (AnimationType)obj.GetValue(InAnimationTypeProperty);
        }

        public static void SetInAnimationType(DependencyObject obj, AnimationType value)
        {
            obj.SetValue(InAnimationTypeProperty, value);
        }

        // Using a DependencyProperty as the backing store for InAnimationType.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty InAnimationTypeProperty =
            DependencyProperty.RegisterAttached("InAnimationType", typeof(AnimationType), typeof(AnimationStuff), new PropertyMetadata(null));




        public static AnimationType GetOutAnimationType(DependencyObject obj)
        {
            return (AnimationType)obj.GetValue(OutAnimationTypeProperty);
        }

        public static void SetOutAnimationType(DependencyObject obj, AnimationType value)
        {
            obj.SetValue(OutAnimationTypeProperty, value);
        }

        // Using a DependencyProperty as the backing store for OutAnimationType.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty OutAnimationTypeProperty =
            DependencyProperty.RegisterAttached("OutAnimationType", typeof(AnimationType), typeof(AnimationStuff), new PropertyMetadata(null));




        public static AnimationDirection GetInAnimation(DependencyObject obj)
        {
            return (AnimationDirection)obj.GetValue(InAnimationProperty);
        }

        public static void SetInAnimation(DependencyObject obj, AnimationDirection value)
        {
            obj.SetValue(InAnimationProperty, value);
        }




        // Using a DependencyProperty as the backing store for InAnimation.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty InAnimationProperty =
            DependencyProperty.RegisterAttached("InAnimation", typeof(AnimationDirection), typeof(AnimationStuff), new PropertyMetadata(null));


        public static AnimationDirection GetOutAnimation(DependencyObject obj)
        {
            return (AnimationDirection)obj.GetValue(OutAnimationProperty);
        }

        public static void SetOutAnimation(DependencyObject obj, AnimationDirection value)
        {
            obj.SetValue(OutAnimationProperty, value);
        }




        // Using a DependencyProperty as the backing store for OutAnimation.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty OutAnimationProperty =
            DependencyProperty.RegisterAttached("AnimationDirection", typeof(AnimationDirection), typeof(AnimationStuff), new PropertyMetadata(null));


        public static bool GetTurnOnAnimation(DependencyObject obj)
        {
            return (bool)obj.GetValue(TurnOnAnimationProperty);
        }

        public static void SetTurnOnAnimation(DependencyObject obj, bool value)
        {
            obj.SetValue(TurnOnAnimationProperty, value);
        }

        // Using a DependencyProperty as the backing store for TurnOnAnimation.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty TurnOnAnimationProperty =
            DependencyProperty.RegisterAttached("TurnOnAnimation", typeof(bool?), typeof(AnimationStuff), new PropertyMetadata(null, Callback));




        #endregion


        private static void Callback(DependencyObject sender, DependencyPropertyChangedEventArgs e)
        {
            if (e.NewValue is bool && ((bool)e.NewValue))
            {
                if (sender is FrameworkElement)
                {
                    (sender as FrameworkElement).Loaded += AnimationStuff_Loaded;

                    inAnimationType = GetInAnimationType(sender);
                    outAnimationType = GetOutAnimationType(sender);
                    inAnim = GetInAnimation(sender);
                    outAnim = GetOutAnimation(sender);
                    target = (sender as FrameworkElement);
                    target.RenderTransform = new TranslateTransform();
                }
            }
        }





        static void AnimationStuff_Loaded(object sender, RoutedEventArgs e)
        {
            if (sender is Visual)
            {
                (sender as FrameworkElement).Unloaded += AnimationStuff_Unloaded;
                PresentationSource source = PresentationSource.FromVisual((Visual)sender);
                parent = (sender as FrameworkElement).Parent;
                source.ContentRendered += source_ContentRendered;
            }
        }

        static void AnimationStuff_Unloaded(object sender, RoutedEventArgs e)
        {
            // do something with outAnimation
            (parent as IAddChild).AddChild(sender);
            (sender as FrameworkElement).BeginStoryboard(outAnimation);

        }

        static void source_ContentRendered(object sender, EventArgs e)
        {
            ((PresentationSource) sender).ContentRendered -= source_ContentRendered;
            target.BeginStoryboard(AnimationProducer.GetInAnimation(inAnimationType, inAnim));


            outAnimation = AnimationProducer.GetOutAnimation(outAnimationType, outAnim);
        }
    }
}
