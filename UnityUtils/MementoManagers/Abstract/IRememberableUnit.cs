using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityUtils.Abstract;

namespace UnityUtils.MementoManagers.Abstract
{
    public interface IRememberableUnit : IUnit
    {
        void RememberUnit();
        void RecallUnit();
    }
}
