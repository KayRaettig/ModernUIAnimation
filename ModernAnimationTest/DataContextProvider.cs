using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows;
using System.Windows.Media;

namespace ModernAnimationTest
{
    public class DataContextProvider : IDataContextProvider
    {
        public INotifyOutAnimation GetDataContextOfType(DependencyObject currentObject)
        {
            bool keepOn = true;
            FrameworkElement tmp = null;
            DependencyObject CurrentObject = currentObject;


            while (keepOn)
            {
                CurrentObject = VisualTreeHelper.GetParent(CurrentObject);

                if (CurrentObject == null)
                {
                    keepOn = false;
                }
                else
                {
                    if (CurrentObject is FrameworkElement && (CurrentObject as FrameworkElement).DataContext is INotifyOutAnimation)
                    {
                        return (INotifyOutAnimation)(CurrentObject as FrameworkElement).DataContext;
                    }
                }
            }
            return null;
        }
    }
}
