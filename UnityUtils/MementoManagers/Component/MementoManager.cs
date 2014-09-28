using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;
using UnityUtils.MementoManagers.Abstract;

namespace UnityUtils.MementoManagers.Component
{
    public class MementoManager : MonoBehaviour
    {
        public void Remember()
        {
            List<IRememberableUnit> pausable = ComponentFinder
                .GetComponentsInChildren(transform, typeof(IRememberableUnit), typeof(MementoManager))
                .ToList()
                .Cast<IRememberableUnit>()
                .ToList();

            foreach (var r in pausable)
            {
                if (r.IsEnabled)
                {
                    r.RememberUnit();
                }
            }
        }

        public void Recall()
        {
            List<IRememberableUnit> pausable = ComponentFinder
                .GetComponentsInChildren(transform, typeof(IRememberableUnit), typeof(MementoManager))
                .ToList()
                .Cast<IRememberableUnit>()
                .ToList();

            foreach (var r in pausable)
            {
                if (r.IsEnabled)
                {
                    r.RecallUnit();
                }
            }
        }
    }
}
