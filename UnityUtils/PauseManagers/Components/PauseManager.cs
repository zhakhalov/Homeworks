using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;
using UnityUtils.PauseManagers.Abstract;

namespace UnityUtils.PauseManagers.Components
{
    public class PauseManager : MonoBehaviour
    {
        public void Pause()
        {
            List<IPausableUnit> pausable = ComponentFinder
                .GetComponentsInChildren(transform, typeof(IPausableUnit), typeof(PauseManager))
                .ToList()
                .Cast<IPausableUnit>()
                .ToList();

            foreach (var r in pausable)
            {
                if (r.IsEnabled && !r.IsPaused)
                {
                    r.PauseUnit();
                }
            }
        }

        public void Resume()
        {
            List<IPausableUnit> pausable = ComponentFinder
                .GetComponentsInChildren(transform, typeof(IPausableUnit), typeof(PauseManager))
                .ToList()
                .Cast<IPausableUnit>()
                .ToList();

            foreach (var r in pausable)
            {
                if (r.IsEnabled && r.IsPaused)
                {
                    r.ResumeUnit();
                }
            }
        }  
    }
}
