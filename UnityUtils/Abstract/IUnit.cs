using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace UnityUtils.Abstract
{
    public interface IUnit
    {
        bool IsEnabled { get; }
        bool IsDestroyed { get; }
    }
}
