using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Interactivity;
using System.Windows.Markup;
using System.Windows.Media;
using System.Windows.Media.Animation;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace ModernAnimationTest
{
    public class AnimationBehavior : Behavior<FrameworkElement>
    {
        private PresentationSource source = null;
        public AnimationType InAnimationType { get; set; }
        public AnimationType OutAnimationType { get; set; }
        public AnimationDirection InAnimation { get; set; }
        public AnimationDirection OutAnimation { get; set; }
        public int AnimationTimeMS { get; set; }


        private DependencyObject _parent = null;
        private FrameworkElement _self = null;
        private Storyboard _story = null;

        public AnimationBehavior()
        {
            AnimationTimeMS = 500;
        }


        protected override void OnAttached()
        {
            AssociatedObject.Loaded += AssociatedObject_Loaded;
            AssociatedObject.Unloaded += AssociatedObject_Unloaded;
            base.OnAttached();
        }


        protected override void OnDetaching()
        {
            AssociatedObject.Loaded -= AssociatedObject_Loaded;
            _parent = null;
            _self = null;
            _story = null;
            source = null;
            base.OnDetaching();
            
        }


        void AssociatedObject_Loaded(object sender, RoutedEventArgs e)
        {
            if (_self != null)
            {
                AssociatedObject.Loaded -= AssociatedObject_Loaded;
                return;
            }
            source = PresentationSource.FromVisual(sender as Visual);
            source.ContentRendered += source_ContentRendered;


        }


        void source_ContentRendered(object sender, EventArgs e)
        {
            source.ContentRendered -= source_ContentRendered;

            _parent = VisualTreeHelper.GetParent(AssociatedObject);
            _self = AssociatedObject;
            _self.RenderTransform = new TranslateTransform();
            _self.BeginStoryboard(AnimationProducer.GetAnimation(InAnimationType, InAnimation, true, AnimationTimeMS));



        }

        void AssociatedObject_Unloaded(object sender, RoutedEventArgs e)
        {
            AssociatedObject.Unloaded -= AssociatedObject_Unloaded;

            if (source != null && source.CompositionTarget != null)
            {
                (_parent as IAddChild).AddChild(_self);
                _story = AnimationProducer.GetAnimation(OutAnimationType, OutAnimation, false, AnimationTimeMS);
                _story.Completed += story_Completed;
                AssociatedObject.BeginStoryboard(_story);
            }
            else // window is gone
            {
                Detach();
            }
        }

        void story_Completed(object sender, EventArgs e)
        {
            _story.Completed -= story_Completed;
            _story = null;

            if (_parent is Panel)
            {
                (_parent as Panel).Children.Remove((UIElement)_self);
            }

            if (_parent is ItemsControl)
            {
                (_parent as ItemsControl).Items.Remove(_self);
            }

            if (_parent is IRemoveChild)
            {
                (_parent as IRemoveChild).RemoveChild(_self);
            }

            _self = null;
            Detach();
        }

        ~AnimationBehavior()
        {
            Console.WriteLine("Behavior collected!");
        }
    }
}
