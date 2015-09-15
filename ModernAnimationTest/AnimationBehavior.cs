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
        private Storyboard _story = null;

        public AnimationDirection? InAnimation { get; set; }
        public AnimationDirection? OutAnimation { get; set; }
        public int AnimationTimeMS { get; set; }
        public IEasingFunction EasingFunction { get; set; }
        
        public bool TryFindDataContextWhenNested { get; set; } // default value is false
        public IDataContextProvider DataContextProvider { get; set; }
        private IOutAnimatable viewModel = null;


        public AnimationBehavior()
        {
            AnimationTimeMS = 500;
        }


        protected override void OnAttached()
        {
            if (DataContextProvider != null)
            {
                TryFindDataContextWhenNested = true;
            }
            else
            {
                DataContextProvider = new DataContextProvider();
            }


            AssociatedObject.Loaded += AssociatedObject_Loaded;
            AssociatedObject.Unloaded += AssociatedObject_Unloaded;

            if (AssociatedObject.DataContext is IOutAnimatable)
            {
                (AssociatedObject.DataContext as IOutAnimatable).RequestOutAnimation +=
                    AnimationBehavior_RequestOutAnimation;

                viewModel = (AssociatedObject.DataContext as IOutAnimatable);
            }
            else
            {
                if (TryFindDataContextWhenNested)
                {
                    IOutAnimatable dc = DataContextProvider.GetDataContextOfType<IOutAnimatable>(AssociatedObject);
                    if (dc != null)
                    {
                        dc.RequestOutAnimation += AnimationBehavior_RequestOutAnimation;
                        viewModel = dc;
                    }
                }
                
            }
                base.OnAttached();
        }




        /// <summary>
        /// Do NOT call this function directly.
        /// It is meant to be called explicitly and exclusively by <ref>story_Completed</ref>
        /// </summary>
        protected override void OnDetaching()
        {
            AssociatedObject.Loaded -= AssociatedObject_Loaded;

            if (viewModel != null)
            {
                viewModel.RequestOutAnimation -= AnimationBehavior_RequestOutAnimation;
            }
        
            _story = null;
            source = null;
            base.OnDetaching();
            
        }



        void source_ContentRendered(object sender, EventArgs e)
        {
            source.ContentRendered -= source_ContentRendered;
            var ao = AssociatedObject;

            ao.RenderTransform = new TranslateTransform();
            ao.BeginStoryboard(AnimationProducer.GetAnimation(InAnimation, true, AnimationTimeMS, EasingFunction));
        }



        void AssociatedObject_Loaded(object sender, RoutedEventArgs e)
        {
            source = PresentationSource.FromVisual(sender as Visual);
            source.ContentRendered += source_ContentRendered;
        }

        void AssociatedObject_Unloaded(object sender, RoutedEventArgs e)
        {
            viewModel = null;
            AssociatedObject.Unloaded -= AssociatedObject_Unloaded;
            Detach();
        }



        void AnimationBehavior_RequestOutAnimation()
        {

            if (source != null && source.CompositionTarget != null)
            {
                _story = AnimationProducer.GetAnimation(OutAnimation, false, AnimationTimeMS, EasingFunction);
                _story.Completed += story_Completed;
                AssociatedObject.BeginStoryboard(_story);
            }
            else // there is no window anymore, so we'll say goodbye silently
            {
                Detach();
            }
        }


        void story_Completed(object sender, EventArgs e)
        {
            _story.Completed -= story_Completed;
            _story = null;

           
            Detach();
        }




        /// <summary>
        /// Is used to test for memory leaks.
        /// </summary>
#if DEBUG
        ~AnimationBehavior()
        {
            Console.WriteLine("Behavior collected!");
        }
#endif
    }
}
