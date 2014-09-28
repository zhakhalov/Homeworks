using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace UnityUtils.UpdateManagers.Abstract
{
	interface IDeltaTimeSetter
	{
        void SetDeltaTime(float deltaTime);
	}
}
