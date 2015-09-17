using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ModernAnimationTest
{
    public delegate void OutAnimationRequested();
    public interface  INotifyOutAnimation
    {
        event OutAnimationRequested RequestOutAnimation;
    }
}
