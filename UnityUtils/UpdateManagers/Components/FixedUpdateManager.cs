using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;
using UnityUtils.UpdateManagers.Abstract;

namespace UnityUtils.UpdateManagers.Components
{
	public class FixedUpdateManager : AbstractUpdateManager
	{
        void Update()
        {
            UpdateUnits(Time.fixedDeltaTime);
        }
	}
}
