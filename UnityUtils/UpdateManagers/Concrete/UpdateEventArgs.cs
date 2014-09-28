using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityUtils.UpdateManagers.Abstract;

namespace UnityUtils.UpdateManagers.Concrete
{
    public class UpdateEventArgs : EventArgs, IDeltaTimeSetter
    {
        public float DeltaTime { get; private set; }

        void IDeltaTimeSetter.SetDeltaTime(float deltaTime)
        {
            DeltaTime = deltaTime;
        }
    }

    public delegate void UpdateEventHandler(object sender, UpdateEventArgs e);
}
