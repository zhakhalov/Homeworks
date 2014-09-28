using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityUtils.UpdateManagers.Abstract;
using UnityEngine;

namespace UnityUtils.UpdateManagers.Components
{
	public class UpdateManager : AbstractUpdateManager
	{
        void Update()
        {
            UpdateUnits(Time.deltaTime);
        }
	}
}
