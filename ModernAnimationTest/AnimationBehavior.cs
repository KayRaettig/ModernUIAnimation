using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows;
using System.Windows.Interactivity;
using System.Windows.Markup;
using System.Windows.Media;

namespace ModernAnimationTest
{
    public class AnimationBehavior : Behavior<FrameworkElement>
    {
        private PresentationSource source = null;
        public AnimationType InAnimationType { get; set; }
        public AnimationType OutAnimationType { get; set; }
        public AnimationDirection InAnimation { get; set; }
        public AnimationDirection OutAnimation { get; set; }

        private DependencyObject _parent = null;
        private FrameworkElement _self = null;

        protected override void OnAttached()
        {
            AssociatedObject.Loaded += AssociatedObject_Loaded;
            AssociatedObject.Unloaded += AssociatedObject_Unloaded;
            base.OnAttached();
        }


        protected override void OnDetaching()
        {
            AssociatedObject.Loaded -= AssociatedObject_Loaded;
            AssociatedObject.Unloaded -= AssociatedObject_Unloaded;
            source.ContentRendered -= source_ContentRendered;
            _parent = null;
            _self = null;
            base.OnDetaching();
        }


        void AssociatedObject_Loaded(object sender, RoutedEventArgs e)
        {
            source = PresentationSource.FromVisual(sender as Visual);
            source.ContentRendered += source_ContentRendered;
        }

        void source_ContentRendered(object sender, EventArgs e)
        {
            _parent = VisualTreeHelper.GetParent(AssociatedObject);
            _self = AssociatedObject;
            AssociatedObject.RenderTransform = new TranslateTransform();
            AssociatedObject.BeginStoryboard(AnimationProducer.GetInAnimation(InAnimationType, InAnimation));
        }

        void AssociatedObject_Unloaded(object sender, RoutedEventArgs e)
        {
            (_parent as IAddChild).AddChild(_self);
            AssociatedObject.BeginStoryboard(AnimationProducer.GetOutAnimation(OutAnimationType, OutAnimation));


            var children = VisualTreeHelper.GetChildrenCount(_parent);
            for (int i = children; i < 0; i--)
            {
                if (VisualTreeHelper.GetChild(_parent, i) == _self)
                {
                    
                }
            }

            Detach();
        }
    }
}
