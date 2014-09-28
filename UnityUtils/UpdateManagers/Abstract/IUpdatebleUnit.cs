using UnityUtils.Abstract;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityUtils.UpdateManagers.Concrete;

namespace UnityUtils.UpdateManagers.Abstract
{
	public interface IUpdatableUnit : IUnit
	{
        void UpdateUnit(object sender, UpdateEventArgs e);
	}
}
