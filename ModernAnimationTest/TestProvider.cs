using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ModernAnimationTest
{
    public class TestProvider : IDataContextProvider
    {
        public T GetDataContextOfType<T>(System.Windows.DependencyObject currentObject)
        {
            throw new NotImplementedException();
        }
    }
}
