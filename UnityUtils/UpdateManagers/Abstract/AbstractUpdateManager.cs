using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;
using UnityUtils.UpdateManagers.Concrete;

namespace UnityUtils.UpdateManagers.Abstract
{
	public abstract class AbstractUpdateManager : MonoBehaviour
    {
        protected UpdateEventHandler UpdateUnit;		
		IDeltaTimeSetter _updateEventArgs;

        private List<IUpdatableUnit> _updatableUnits;
		
		void Start()
		{
			_updateEventArgs = new UpdateEventArgs();
            _updatableUnits = ComponentFinder
                .GetComponentsInChildren(transform, typeof(IUpdatableUnit), typeof(AbstractUpdateManager))
                .ToList()
                .Cast<IUpdatableUnit>()
                .ToList();
		}
		
		protected void UpdateUnits(float deltaTime)
		{
            _updateEventArgs.SetDeltaTime(deltaTime);

			foreach(var r in _updatableUnits)
			{
                if (("null" == r.ToString()) || r.IsDestroyed)
                {
                    _updatableUnits.Remove(r);
                    continue;
                }

                if (r.IsEnabled)
                {
                    r.UpdateUnit(this, (_updateEventArgs as UpdateEventArgs));
                }
			}
		}
	}
}
