using UnityUtils.Abstract;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace UnityUtils.PauseManagers.Abstract
{
    public interface IPausableUnit : IUnit
    {
        bool IsPaused { get; }
        void PauseUnit();
        void ResumeUnit();
    }
}
