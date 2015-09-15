using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows;

namespace ModernAnimationTest
{
    public interface IDataContextProvider
    {
        T GetDataContextOfType<T>(DependencyObject currentObject);
    }
}
