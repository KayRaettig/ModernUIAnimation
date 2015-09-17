using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ModernAnimationTest
{
    public class TestProvider : IDataContextProvider
    {
        public INotifyOutAnimation GetDataContextOfType(System.Windows.DependencyObject currentObject)
        {
            throw new NotImplementedException();
        }
    }
}
