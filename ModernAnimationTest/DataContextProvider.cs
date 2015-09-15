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
        public T GetDataContextOfType<T>(DependencyObject currentObject)
        {
            bool keepOn = true;
            FrameworkElement tmp = null;
            T target;
            DependencyObject CurrentObject = currentObject;


            while (keepOn)
            {
                CurrentObject = VisualTreeHelper.GetParent(CurrentObject);

                if (CurrentObject == null)
                {
                    keepOn = false;
                    continue;
                }
                else
                {
                    if (CurrentObject is FrameworkElement && (CurrentObject as FrameworkElement).DataContext is T)
                    {
                        return (T)(CurrentObject as FrameworkElement).DataContext;
                    }
                }
            }

            return default(T);
        }
    }
}
